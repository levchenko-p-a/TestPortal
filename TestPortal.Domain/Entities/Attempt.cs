using System;
using System.Collections.Generic;
using System.Data.Linq;
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
    public class Attempt
    {
        [HiddenInput(DisplayValue = false)]
        public int AttemptId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int TestId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int UserId { get; set; }
        [Display(Name = "Баллы попытки")]
        [ReadOnly(true)]
        public int Score
        {
            get
            {
                return Passages == null ? 0 : Passages.Sum(s => s.Score);
            }
        } 
        [Display(Name = "Завершено?")]
        [ReadOnly(true)]
        public bool Complete { get; set; }
        [Display(Name = "Время начала")]
        [ReadOnly(true)]
        public DateTime BeginTime { get; set; }
        [Display(Name = "Время конца")]
        [ReadOnly(true)]
        public DateTime EndTime { get; set; }
        public virtual ICollection<Passage> Passages { get; set; }
        public virtual User User { get; set; }
        public virtual Test Test { get; set; }
    }
}
