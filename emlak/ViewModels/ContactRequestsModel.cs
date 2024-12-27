using emlak.Models.user;
using System.ComponentModel.DataAnnotations;

namespace emlak.Models
{
    public class SubmissionModel
    {
        public int submission_id { get; set; }



        [Display(Name = "Odev No")]
        [Required(ErrorMessage = "Odev No Giriniz!")]
        public int assignment_ID { get; set; }
        public virtual Properties Assignment{ get; set; } // User tablosu ile ilişkilendirme



        [Display(Name = "Ogrenci No")]
        [Required(ErrorMessage = "Ogrenci No Giriniz!")]
        public int user_ID { get; set; }
        public virtual Users User { get; set; } // User tablosu ile ilişkilendirme



        [Display(Name = "Odev Tarih")]
        [Required(ErrorMessage = "Odev Tarih Giriniz!")]
        public string submission_date { get; set; }


        public int FileUploadId { get; set; }
        public PropertyImages file_url { get; set; }

        [Required(ErrorMessage = "Dosya yüklenmesi gereklidir.")]
        public IFormFile UploadedFile { get; set; } // Formdan gelen dosya


    }
}
