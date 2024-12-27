using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using emlak.Repository;
using emlak.ViewModels;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using emlak.Models.user;
using emlak.Models;

namespace emlak.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyf;
        private readonly PropertiesRepository _propertiesRepository;
        private readonly UserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, INotyfService notyf, PropertiesRepository propertiesRepository, UserRepository userRepository)
        {
            _logger = logger;
            _propertiesRepository = propertiesRepository;
            _notyf = notyf;
            _userRepository = userRepository;
        }



        public async Task<IActionResult> Index()
        {
            // Properties ve Users verilerini alıyoruz
            var properties = await _propertiesRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();  // Kullanıcıları almak için kullanıcı repository'sini kullanıyoruz.

            // Properties'i ViewModel'e dönüştürme
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
                User = users.FirstOrDefault(u => u.user_id == p.user_ID), // Kullanıcı eşleştirme
            }).ToList();

            // ViewModel'i View'a gönderiyoruz
            return View(propertiesViewModels);  // Burada `propertiesViewModels` kullanmalıyız, `productModels` değil.
        }

    }
}
