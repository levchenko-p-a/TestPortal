using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.ComponentModel;

namespace TestPortal.Domain.Entities
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
        [Display(Name = "е-мейл")]
        [Required(ErrorMessage = "Пожалуйста, введите е-мейл")]
        public string Email { get; set; }
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "Пожалуйста, введите логин")]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "Пожалуйста, введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Пожалуйста, введите фамилию")]
        public string Surname { get; set; }
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Пожалуйста, введите имя")]
        public string Name { get; set; }
        [Display(Name = "Отчество")]
        [Required(ErrorMessage = "Пожалуйста, введите отчество")]
        public string Patronymic { get; set; }
        [ScaffoldColumn(false)]
        public Boolean IsActive { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }
        [ScaffoldColumn(false)]
        public string Salt { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
