using emlak.Models.user;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace emlak.Models
{
    public class Properties
    {
        [Key]
        public int pro_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string price { get; set; }
        public string location { get; set; }
        public string propertyType { get; set; }
        public string bedrooms { get; set; }
        public string status { get; set; }
        public decimal area { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }

        public int user_ID { get; set; }
        [ForeignKey("user_ID")]
        public virtual Users Users { get; set; }

        public virtual ICollection<PropertyImages> PropertyImages { get; set; }

    }
}
