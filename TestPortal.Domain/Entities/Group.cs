using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestPortal.Domain.Entities
{
    public class Group
    {
        [HiddenInput(DisplayValue = false)]
        public int GroupId { get; set; }
        [Display(Name = "Группа")]
        [Required(ErrorMessage = "Пожалуйста, выберите группу")]
        public string Title { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
