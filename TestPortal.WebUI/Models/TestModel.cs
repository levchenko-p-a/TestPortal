using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestPortal.Domain;
using TestPortal.Domain.Entities;

namespace TestPortal.WebUI.Models
{
    public class TestModel
    {
        public IEnumerable<Reply> Replies { get; set; }
        public IEnumerable<Test> Tests { get; set; }
        public Question Question { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}