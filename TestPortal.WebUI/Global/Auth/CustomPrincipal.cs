using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TestPortal.Domain.Entities;

namespace TestPortal.WebUI.Global.Auth
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Name)
        {
            this.Identity = new GenericIdentity(Name);
        }
        public CustomPrincipal(User user)
        {
            UserId = user.UserId;
            Salt = user.Salt;
            Name = user.Name;
            Surname = user.Surname;
            Roles=user.Roles.Select(m => m.Title).ToArray();
            this.Identity = new GenericIdentity(Name);
        }

        public int UserId { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string[] Roles { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public CustomPrincipalSerializeModel()
        {

        }
        public CustomPrincipalSerializeModel(User user) 
        {
            UserId = user.UserId;
            Salt = user.Salt;
            Name = user.Name;
            Surname = user.Surname;
            Roles = user.Roles.Select(m => m.Title).ToArray();
        }
        public CustomPrincipalSerializeModel(CustomPrincipal user)
        {
            UserId = user.UserId;
            Salt = user.Salt;
            Name = user.Name;
            Surname = user.Surname;
            Roles = user.Roles;
        }
        public int UserId { get; set; }
        public string Salt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string[] Roles { get; set; }
    }
}