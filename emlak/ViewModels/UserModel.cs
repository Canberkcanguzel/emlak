using emlak.Models.user;
using System.ComponentModel.DataAnnotations;

namespace emlak.ViewModels
{
    public class UserModel
    {
        public int user_id { get; set; }



        [Display(Name = "Kullanici Ad")]
        [Required(ErrorMessage = "Kullanici Adi Giriniz!")]
        public string userName { get; set; }

        [Display(Name = "Kullanici email")]
        [Required(ErrorMessage = "Kullanici Emailini Giriniz!")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Kullanici Sifresi")]
        [Required(ErrorMessage = "Kullanici Sifresi Giriniz!")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Display(Name = "Yetki Giriniz")]
        [Required(ErrorMessage = "Kullanici Yetkisi Giriniz!")]
        public RoleType role { get; set; }


    }
}
