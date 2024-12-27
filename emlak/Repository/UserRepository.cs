using emlak.Models.user;

namespace emlak.Repository
{
    public class UserRepository : GenericRepository<Users>
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
