using emlak.Models;
using emlak.Models.user;
using System.Diagnostics;

namespace emlak.Repository
{
    public class ContactRequestsRepository : GenericRepository<Users>
    {
        public ContactRequestsRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
