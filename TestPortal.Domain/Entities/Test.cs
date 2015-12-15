using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.ComponentModel;

namespace TestPortal.Domain.Entities
{
    public class Test
    {
        [HiddenInput(DisplayValue = false)]
        public int TestId { get; set; }
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название теста")]
        public string Name { get; set; }
        [Display(Name = "Количество вопросов")]
        [ReadOnly(true)]
        public int QuestionCount
        {
            get
            {
                return Questions==null?0:Questions.Count();
            }
        }
        [Display(Name = "Вес теста")]
        [ReadOnly(true)]
        public int Score
        {
            get
            {
                return Questions==null?0:Questions.Sum(s=>s.Weight);
            }
        }
        [Display(Name = "Порог прохождения")]
        public int Threshold { get; set; }
        [Display(Name = "Время на прохождение")]
        public DateTimeOffset TestTime { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
