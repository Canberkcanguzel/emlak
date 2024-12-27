using emlak.Models;
using emlak.Models.user;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace emlak.ViewModels
{
    public class PropertiesModel
    {
        public int pro_id { get; set; }


        [Display(Name = "Emlak Basligi")]
        [Required(ErrorMessage = "Lutfen Emlak Basligini Giriniz!")]
        public string title { get; set; }


        [Display(Name = "Emlak Aciklama")]
        [Required(ErrorMessage = "Lutfen Emlak Detaylarini Giriniz!")]
        public string description { get; set; }


        [Display(Name = "Emlak Fiyati")]
        [Required(ErrorMessage = "Lutfen Emlak Fiyatini Giriniz!")]
        public string price { get; set; }


        [Display(Name = "Emlak Konumu")]
        [Required(ErrorMessage = "Lutfen Emlak Konumunu Giriniz!")]
        public string location { get; set; }
        
        
        [Display(Name = "Emlak Turu")]
        [Required(ErrorMessage = "Lutfen Emlak Turunu Giriniz!")]
        public string propertyType { get; set; }


        [Display(Name = "Emlak Oda")]
        [Required(ErrorMessage = "Lutfen Oda Sayisini Giriniz!")]
        public string bedrooms { get; set; }


        [Display(Name = "Emlak Durumu")]
        [Required(ErrorMessage = "Lutfen Emlakin Satilip Satilmadigini Giriniz!")]
        public string status { get; set; }


        [Display(Name = "Emlak Metre")]
        [Required(ErrorMessage = "Lutfen Emlakin Kac Metre Kare Oldugunu Giriniz!")]
        public decimal area { get; set; }


        [Display(Name = "Ilani Veren")]
        [Required(ErrorMessage = "Lutfen Odevle Ilgili Akademisyeni Giriniz!")]
        public int user_ID { get; set; }
        public virtual Users User { get; set; } // User tablosu ile ilişkilendirme

        [Display(Name = "Yuklenmis Resimler")]
        public IList<string> UploadedImages { get; set; } // For Base64-encoded strings

        [Display(Name = "Yeni Resim Yükleme")]
        public ICollection<IFormFile> PropertyImages { get; set; } // For handling image uploads
    }
}
