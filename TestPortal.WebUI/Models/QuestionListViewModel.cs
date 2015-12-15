using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestPortal.Domain.Entities;

namespace TestPortal.WebUI.Models
{
    public class QuestionListViewModel
    {
        public IEnumerable<Question> Questions { get; set; }
        public PagingInfo Paginglnfo { get; set; }
        public string QuestionCategory { get; set; }
    }
}