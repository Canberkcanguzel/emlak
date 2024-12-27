using emlak.Models.user;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emlak.Models
{
    public class PropertyImages
    {
        [Key]
        public int image_id { get; set; } 
        public byte[] imageURL { get; set; }  
        public Boolean IsPrimary { get; set; } 

        public int pro_ID { get; set; }

        [ForeignKey("pro_ID")]
        public virtual Properties Properties { get; set; }


    }
}
