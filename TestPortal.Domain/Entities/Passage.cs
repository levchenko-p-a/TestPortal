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
    public class Passage
    {
        [HiddenInput(DisplayValue = false)]
        public int PassageId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int ReplyId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int AttemptId { get; set; }
        [Display(Name = "Баллы прохождения")]
        [ReadOnly(true)]
        public int Score
        {
            get
            {
                return Reply.Correct ? Reply.Question.Weight : 0;
            }
        }
        public virtual Attempt Attempt { get; set; }
        public virtual Reply Reply { get; set; }
    }
}
