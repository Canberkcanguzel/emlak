using AspNetCoreHero.ToastNotification.Abstractions;
using emlak.Repository;
using emlak.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace emlak.Controllers
{
    public class GuestHomePageController : Controller
    {

        private readonly PropertiesRepository _propertiesRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserRepository _userRepository;
        private readonly INotyfService _notyf;

        public GuestHomePageController(UserRepository userRepository, PropertiesRepository propertiesRepository, ApplicationDbContext applicationDbContext, INotyfService notyf)
        {
            _propertiesRepository = propertiesRepository;
            _applicationDbContext = applicationDbContext;
            _userRepository = userRepository;
            _notyf = notyf;

        }

        public async Task<IActionResult> Index()
        {
            // Tüm ilanları ve kullanıcıları çek
            var properties = await _propertiesRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();

            // İlanları ViewModel'e dönüştür
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

            // View'a gönder
            return View(propertiesViewModels);
        }
    }
}
