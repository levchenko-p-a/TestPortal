using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.Domain.Entities;

namespace TestPortal.Domain
{
    public class TestRepository:IRepository
    {
        private TestContext context = new TestContext();
        private void CopyProperty(object from, object to)
        {
            foreach (System.Reflection.PropertyInfo info in from.GetType().GetProperties())
            {
                try
                {
                    if (info.CanWrite)
                    {
                        to.GetType().GetProperty(info.Name).SetValue(to, info.GetValue(from, null), null);
                    }
                }
                catch
                { }
            }
        }

        public IQueryable<Attempt> Attempts
        {
            get
            {
                return context.Attempts;
            }
        }

        public void SaveAttempt(Attempt instance)
        {
            if (instance.AttemptId == 0)
            {
                context.Attempts.Add(instance);
            }
            else
            {
                Attempt cache = context.Attempts.Where(p => p.AttemptId == instance.AttemptId).FirstOrDefault();
                CopyProperty(instance, cache);
            }
            context.SaveChanges();
        }

        public void DeleteAttempt(Attempt instance)
        {
            context.Attempts.Remove(instance);
            context.SaveChanges();
        }

        public IQueryable<Passage> Passages
        {
            get
            {
                return context.Passages;
            }
        }

        public void SavePassage(Passage instance)
        {
            if (instance.PassageId == 0)
            {
                context.Passages.Add(instance);
            }
            else
            {
                Passage cache = context.Passages.Where(p => p.PassageId == instance.PassageId).FirstOrDefault();
                CopyProperty(instance, cache);
            }
            context.SaveChanges();
        }

        public void DeletePassage(Passage instance)
        {
            context.Passages.Remove(instance);
            context.SaveChanges();
        }


        public IQueryable<Question> Questions
        {
            get
            {
                return context.Questions;
            }
        }

        public void SaveQuestion(Question instance)
        {
            if (instance.QuestionId == 0)
            {
                context.Questions.Add(instance);
            }
            else
            {
                Question cache = context.Questions.Where(p => p.QuestionId == instance.QuestionId).FirstOrDefault();
                CopyProperty(instance, cache);
            }
            context.SaveChanges();
        }

        public void DeleteQuestion(Question instance)
        {
            context.Questions.Remove(instance);
            context.SaveChanges();
        }

        public IQueryable<Reply> Replies
        {
            get
            {
                return context.Replies;
            }
        }

        public void SaveReply(Reply instance)
        {
            if (instance.ReplyId == 0)
            {
                context.Replies.Add(instance);
            }
            else
            {
                Reply cache = context.Replies.Where(p => p.ReplyId == instance.ReplyId).FirstOrDefault();
                CopyProperty(instance, cache);
            }
            context.SaveChanges();
        }

        public void DeleteReply(Reply instance)
        {
            context.Replies.Remove(instance);
            context.SaveChanges();
        }

        public IQueryable<Role> Roles
        {
            get
            {
                return context.Roles;
            }
        }

        public void SaveRole(Role instance)
        {
            if (instance.RoleId == 0)
            {
                context.Roles.Add(instance);
            }
            else
            {
                Role cache = context.Roles.Where(p => p.RoleId == instance.RoleId).FirstOrDefault();
                CopyProperty(instance, cache);
            }
            context.SaveChanges();
        }

        public void DeleteRole(Role instance)
        {
            context.Roles.Remove(instance);
            context.SaveChanges();
        }
        public IQueryable<Group> Groups
        {
            get
            {
                return context.Groups;
            }
        }

        public void SaveGroup(Group instance)
        {
            if (instance.GroupId == 0)
            {
                context.Groups.Add(instance);
            }
            else
            {
                Group cache = context.Groups.Where(p => p.GroupId == instance.GroupId).FirstOrDefault();
                CopyProperty(instance, cache);
            }
            context.SaveChanges();
        }

        public void DeleteGroup(Group instance)
        {
            context.Groups.Remove(instance);
            context.SaveChanges();
        }
        public IQueryable<Test> Tests
        {
            get
            {
                return context.Tests;
            }
        }

        public void SaveTest(Test instance)
        {
            if (instance.TestId == 0)
            {
                context.Tests.Add(instance);
            }
            else
            {
                Test cache = context.Tests.Where(p => p.TestId == instance.TestId).FirstOrDefault();
                CopyProperty(instance, cache);
            }
            context.SaveChanges();
        }

        public void DeleteTest(Test instance)
        {
            context.Tests.Remove(instance);
            context.SaveChanges();
        }

        public IQueryable<User> Users
        {
            get
            {
                return context.Users;
            }
        }

        public void SaveUser(User instance)
        {
            if (instance.UserId == 0)
            {
                context.Users.Add(instance);
            }
            else
            {
                User cache = context.Users.Where(p => p.UserId == instance.UserId).FirstOrDefault();
                CopyProperty(instance, cache);
            }
            context.SaveChanges();
        }

        public void DeleteUser(User instance)
        {
            context.Users.Remove(instance);
            context.SaveChanges();
        }
    }
}
