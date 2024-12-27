using emlak.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emlak.Models.user
{
    public class Users
    {
        [Key]
        public int user_id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public RoleType role { get; set; }


        public virtual ICollection<Properties> Properties { get; set; }
        public virtual ICollection<ContactRequests> ContactRequests { get; set; }
        public virtual ICollection<PropertyImages> PropertyImages { get; set; }

    }
}
