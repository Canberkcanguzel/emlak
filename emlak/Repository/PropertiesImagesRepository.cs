using Microsoft.EntityFrameworkCore;
using emlak.Models;
using emlak.Models.user;
using emlak.ViewModels;
using System.Data;

namespace emlak.Repository
{
    public class PropertiesImagesRepository : GenericRepository<PropertyImages>
    {

        public PropertiesImagesRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
