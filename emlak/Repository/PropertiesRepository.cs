using Microsoft.EntityFrameworkCore;
using emlak.Models;
using emlak.Models.user;
using emlak.ViewModels;
using System.Data;

namespace emlak.Repository
{
    public class PropertiesRepository: GenericRepository<Properties>
    {

        public PropertiesRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
