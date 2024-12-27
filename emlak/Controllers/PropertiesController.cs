using AspNetCoreHero.ToastNotification.Abstractions;
using emlak.Models;
using emlak.Models.user;
using emlak.Repository;
using emlak.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace emlak.Controllers
{
    public class PropertiesController : Controller
    {
        public class PropertiesPersonController : Controller
        {
            private readonly PropertiesRepository _propertiesRepository;
            private readonly PropertiesImagesRepository _propertiesImagesRepository;
            private readonly ApplicationDbContext _applicationDbContext;
            private readonly UserRepository _userRepository;
            private readonly INotyfService _notyf;

            public PropertiesPersonController(PropertiesImagesRepository propertiesImagesRepository, UserRepository userRepository, PropertiesRepository propertiesRepository, ApplicationDbContext applicationDbContext, INotyfService notyf)
            {
                _propertiesRepository = propertiesRepository;
                _applicationDbContext = applicationDbContext;
                _propertiesImagesRepository = propertiesImagesRepository;
                _userRepository = userRepository;
                _notyf = notyf;

            }
            public async Task<IActionResult> Index()
            {
                var properties = await _propertiesRepository.GetAllAsync();
                var users = await _userRepository.GetAllAsync();

                var propertiesViewModels = properties.Select(p => new PropertiesModel
                {
                    pro_id = p.pro_id,
                    title = p.title,
                    price = p.price,
                    description = p.description,
                    location = p.location,
                    propertyType = p.propertyType,
                    bedrooms = p.bedrooms,
                    status = p.status,
                    area = p.area,
                    user_ID = p.user_ID,
                    User = users.FirstOrDefault(u => u.user_id == p.user_ID)
                });

                return View(propertiesViewModels);
            }


            public async Task<IActionResult> Add()
            {
                // Kullanıcılar için gerekli verilerin hazırlanması
                await PopulateUsersToViewBag();
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Add(PropertiesModel model)
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "Formda hata var!" });
                }

                // Find the user by user_ID
                var user = await _userRepository.GetByIdAsync(model.user_ID);
                if (user == null)
                {
                    return Json(new { success = false, message = "Geçerli bir kullanıcı seçilmedi." });
                }

                // Create the property object and save it to the database
                var property = new Properties
                {
                    title = model.title,
                    description = model.description,
                    price = model.price,
                    location = model.location,
                    propertyType = model.propertyType,
                    bedrooms = model.bedrooms,
                    status = model.status,
                    area = model.area,
                    user_ID = model.user_ID,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                };

                using (var transaction = await _applicationDbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Save the property first
                        await _propertiesRepository.AddAsync(property);

                        // Handle image uploads (multiple images)
                        if (model.PropertyImages != null && model.PropertyImages.Any())
                        {
                            var primaryImageSet = false;
                            foreach (var file in model.PropertyImages)
                            {
                                if (file.Length > 0)
                                {
                                    var fileExtension = Path.GetExtension(file.FileName).ToLower();
                                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

                                    if (!allowedExtensions.Contains(fileExtension))
                                    {
                                        return Json(new { success = false, message = "Geçersiz dosya türü. Lütfen geçerli bir resim dosyası yükleyin." });
                                    }

                                    using (var memoryStream = new MemoryStream())
                                    {
                                        await file.CopyToAsync(memoryStream);
                                        var imageBytes = memoryStream.ToArray();

                                        var propertyImage = new PropertyImages
                                        {
                                            imageURL = imageBytes,
                                            pro_ID = property.pro_id,
                                            IsPrimary = !primaryImageSet // Set the first image as the primary
                                        };

                                        if (!primaryImageSet)
                                        {
                                            primaryImageSet = true;
                                        }

                                        // Save each image to the database
                                        await _propertiesImagesRepository.AddAsync(propertyImage);
                                    }
                                }
                            }
                        }

                        // Commit transaction
                        await transaction.CommitAsync();
                        return Json(new { success = true, message = "Emlak ilanı başarıyla eklendi!" });
                    }
                    catch (Exception)
                    {
                        // Rollback transaction on error
                        await transaction.RollbackAsync();
                        return Json(new { success = false, message = "Bir hata oluştu. Lütfen tekrar deneyin." });
                    }
                }
            }

            private async Task PopulateUsersToViewBag()
            {
                var users = await _userRepository
                    .Where(u => u.role == RoleType.PERSON || u.role == RoleType.ADMIN)
                    .Select(u => new
                    {
                        u.user_id,
                        user_display = $"{u.userName} ({u.role})"
                    })
                    .ToListAsync();

                ViewBag.Users = new SelectList(users, "user_id", "user_display");
            }

            public async Task<IActionResult> Update(int id)
            {
                var property = await _propertiesRepository.GetByIdAsync(id);
                if (property == null)
                {
                    return NotFound("Emlak ilanı bulunamadı.");
                }

                // Kullanıcılar için gerekli verilerin hazırlanması
                await PopulateUsersToViewBag();

                // Ödevin mevcut user_ID'sini seçili olarak View'a aktar
                ViewBag.user_ID = new SelectList(ViewBag.Users as IEnumerable<dynamic>, "user_id", "user_name", property.user_ID);

                return View(property);
            }



            [HttpPost]
            public async Task<IActionResult> Update(PropertiesModel model)
            {
                if (!ModelState.IsValid)
                {
                    await PopulateUsersToViewBag();
                    return Json(new { success = false, message = "Model validation failed." });
                }

                var property = await _propertiesRepository.GetByIdAsync(model.pro_id);
                if (property == null)
                {
                    return Json(new { success = false, message = "Property not found." });
                }

                property.title = model.title;
                property.description = model.description;
                property.price = model.price;
                property.location = model.location;
                property.propertyType = model.propertyType;
                property.bedrooms = model.bedrooms;
                property.area = model.area;
                property.status = model.status;
                property.user_ID = model.user_ID;

                await _propertiesRepository.UpdateAsync(property);

                _notyf.Success("Emlak ilanı başarıyla güncellendi!");
                return Json(new { success = true, message = "Property successfully updated." });
            }

            public async Task<IActionResult> Delete(int id)
            {
                var property = await _propertiesRepository.GetByIdAsync(id);
                if (property == null)
                {
                    TempData["ErrorMessage"] = "Emlak ilanı bulunamadı.";
                    return RedirectToAction("Index");
                }

                return View(property);
            }

            [HttpPost]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                try
                {
                    await _propertiesRepository.DeleteAsync(id);
                    _notyf.Success("Emlak ilanı başarıyla silindi!");
                    return Json(new { success = true, message = "Property successfully deleted." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"Error: {ex.Message}" });
                }
            }
        }
    }
}