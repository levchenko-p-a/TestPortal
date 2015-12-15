using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TestPortal.Domain.Entities
{
    public class Role
    {
        [HiddenInput(DisplayValue = false)]
        public int RoleId { get; set; }
        [Display(Name = "Роль")]
        [Required(ErrorMessage = "Пожалуйста, выберите роль")]
        public string Title { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
