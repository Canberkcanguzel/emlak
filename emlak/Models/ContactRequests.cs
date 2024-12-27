using emlak.Models;
using emlak.Models.user;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emlak.Models
{
    public class ContactRequests
    {
        [Key]
        public int request_id { get; set; }
        public string message { get; set; }
        public string contactPhone { get; set; }
        public string contactEmail { get; set; }
        public string createdAt { get; set; }


        public int user_ID { get; set; }
        [ForeignKey("user_ID")]
        public virtual Users Users { get; set; }

        public int pro_ID { get; set; }
        [ForeignKey("pro_ID")]
        public virtual Properties Properties { get; set; }

    }
}
