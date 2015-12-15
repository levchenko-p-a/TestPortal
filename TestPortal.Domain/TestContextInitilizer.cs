using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.Domain.Entities;

namespace TestPortal.Domain
{
    class TestContextInitilizer : DropCreateDatabaseIfModelChanges<TestContext>
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //функция для заполнения базы данных
        protected override void Seed(TestContext context)
        {
            //может добавлять и удалять пользователей и роли + все что ниже
            Role role1 = new Role { Title = "Admin" };
            //может давать попытки на тесты студентам + все что ниже
            Role role2 = new Role { Title = "Teacher" };
            //может редактировать тесты + все что ниже
            Role role3 = new Role { Title = "Editor" };
            //может проходить тесты
            Role role4 = new Role { Title = "Student" };
            //главный админ
            Role role5 = new Role { Title = "root" };
            context.Roles.Add(role1);
            context.Roles.Add(role2);
            context.Roles.Add(role3);
            context.Roles.Add(role4);
            User user1 = new User { Name = "Павел",Surname="Левченко",Patronymic="Алексеевич", Email = "admin@ymail.com", Login = "Admin", Password = "123", IsActive = true, CreateDate = DateTime.UtcNow, Roles = new List<Role>(),Attempts=null };
            User user2 = new User { Name = "Виктория", Surname = "Морнева", Patronymic = "Сергеевна", Email = "user1@ymail.com", Login = "User1", Password = "123", IsActive = true, CreateDate = DateTime.UtcNow, Roles = new List<Role>(), Attempts = null };
            user1.Roles.Add(role1);
            user2.Roles.Add(role4);
            context.Users.Add(user1);
            context.Users.Add(user2);
            try
                {
            context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        logger.Error("Object: " + validationError.Entry.Entity.ToString());
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            logger.Error(err.ErrorMessage + " ");
                        }
                    }
                }
        }
    }
}
