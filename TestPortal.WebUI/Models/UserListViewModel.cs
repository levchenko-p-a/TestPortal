using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestPortal.Domain.Entities;

namespace TestPortal.WebUI.Models
{
    public class UserListViewModel
    {
        //перечисление пользователей
        public IEnumerable<User> Users { get; set; }
        //информация о странице
        public PagingInfo Paginglnfo { get; set; }
        //роль пользователей в перечислении
        public String Role { get; set; }
    }
}