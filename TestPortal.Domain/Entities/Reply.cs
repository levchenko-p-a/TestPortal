using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace TestPortal.Domain.Entities
{
    public class Reply
    {
        [HiddenInput(DisplayValue = false)]
        public int ReplyId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int QuestionId { get; set; }
        
        [Required(ErrorMessage = "Пожалуйста, введите ответ")]
        [Display(Name = "Текст")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите правильность ответа")]
        [Display(Name = "Правильный?")]
        public bool Correct { get; set; }
        public virtual Question Question { get; set; }
    }
}
