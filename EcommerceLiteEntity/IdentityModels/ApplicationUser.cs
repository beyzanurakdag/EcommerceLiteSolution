using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceLiteEntity.Models;

namespace EcommerceLiteEntity.IdentityModels
{
    public class ApplicationUser:IdentityUser
    {
        [StringLength(maximumLength:25, MinimumLength =2, ErrorMessage ="İsminizin uzunluğu 2 ile 25 karakter arasında olmalıdır!")]
        [Display(Name="Ad")]
        [Required]
        public string Name { get; set; }
        [StringLength(maximumLength: 25, MinimumLength = 2, ErrorMessage = "Soyisminizin uzunluğu 2 ile 25 karakter arasında olmalıdır!")]
        [Display(Name = "Soyad")]
        [Required]
        public string Surname { get; set; }
        [Display(Name = "Kayıt Tarihi")]
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime RegisterDate { get; set; } = DateTime.Now;
       //TODO:!
        public string ActivationCode { get; set; }
        public virtual List<Customer> CustomerList { get; set; }
        public virtual List<Admin> AdminList { get; set; }
        public virtual List<PassiveUser> PassiveUserList { get; set; }
    }
}
