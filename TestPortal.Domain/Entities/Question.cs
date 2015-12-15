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
    public class Question
    {
        [HiddenInput(DisplayValue = false)]
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите вопрос")]
        [Display(Name = "Текст")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        [Display(Name = "Мульти")]
        public bool Multi { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение")]
        [Display(Name = "Вес")]
        public int Weight { get; set; }
        [Required(ErrorMessage = "Пожалуйста, введите категорию")]
        [Display(Name = "Категория")]
        public string Category { get; set; }
        public byte[] ImageData { get; set; }
        [ScaffoldColumn(false)]
        public string ImageMimeType { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
    }
}
