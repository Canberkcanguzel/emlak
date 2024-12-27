using AspNetCoreHero.ToastNotification.Abstractions;
using emlak.Models.user;
using emlak.Repository;
using emlak.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace emlak.Controllers
{
    public class UserController : Controller
    {

        private readonly UserRepository _userRepository;
        private readonly INotyfService _notyf;

        public UserController(UserRepository userRepository, INotyfService notyf)
        {
            _userRepository = userRepository;
            _notyf = notyf;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userRepository.GetAllAsync();
            var userModels = user.Select(u => new UserModel
            {
                user_id = u.user_id,
                userName = u.userName,
                email = u.email,
                password = u.password,
                role = u.role
            }).ToList();

            return View(userModels);
        }

        public IActionResult Add()
        {
            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(RoleType)));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(Enum.GetValues(typeof(RoleType)), model.role);
                return View(model);
            }

            var user = new Users
            {
                userName = model.userName,
                email = model.email,
                password = model.password,
                role = model.role
            };

            await _userRepository.AddAsync(user);

            _notyf.Success("Kullanıcı Eklendi...");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            // Kullanıcıyı UserModel'e dönüştürme
            var model = new UserModel
            {
                userName = user.userName,
                email = user.email,
                password = user.password,
                role = user.role,
                user_id = user.user_id// id'yi de eklemeyi unutmayın
            };

            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(RoleType)), model.role);

            return View(model);  // UserModel'i View'a gönderiyoruz
        }


        [HttpPost]
        public async Task<IActionResult> Update(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(Enum.GetValues(typeof(RoleType)), model.role);
                return View(model);  // Hata varsa, model ile View'a geri dönüyoruz
            }

            var user = await _userRepository.GetByIdAsync(model.user_id);


            // Kullanıcıyı güncelliyoruz
            user.userName = model.userName;
            user.email = model.email;
            user.password = model.password;
            user.role = model.role;

            // Veritabanına güncellenmiş kullanıcıyı kaydediyoruz
            await _userRepository.UpdateAsync(user);

            _notyf.Success("Kullanıcı başarıyla güncellendi.");
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
          
            var userModel = new UserModel
            {
                user_id = user.user_id,
                userName = user.userName,
                email = user.email,
                role = user.role
            };

            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UserModel model)
        {
            // Kullanıcı bilgisi geçerli mi kontrol et
            if (model == null || model.user_id == 0)
            {
                TempData["ErrorMessage"] = "Geçersiz kullanıcı bilgisi.";
                return RedirectToAction("Index");
            }

            await _userRepository.DeleteAsync(model.user_id);

            _notyf.Success("Kullanıcı başarıyla silindi.");
            return RedirectToAction("Index");
        }


    }
}
