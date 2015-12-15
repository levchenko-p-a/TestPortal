using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPortal.Domain.Entities;

namespace TestPortal.Domain
{
    public interface IRepository
    {

        #region Attempt

        IQueryable<Attempt> Attempts { get; }

        void DeleteAttempt(Attempt instance);

        void SaveAttempt(Attempt instance);

        #endregion 

        #region Passage

        IQueryable<Passage> Passages { get; }

        void DeletePassage(Passage instance);

        void SavePassage(Passage instance);

        #endregion 

        #region Question

        IQueryable<Question> Questions { get; }

        void DeleteQuestion(Question instance);

        void SaveQuestion(Question instance);

        #endregion 

        #region Reply

        IQueryable<Reply> Replies { get; }

        void DeleteReply(Reply instance);

        void SaveReply(Reply instance);

        #endregion 

        #region Role

        IQueryable<Role> Roles { get; }

        void DeleteRole(Role instance);

        void SaveRole(Role instance);

        #endregion 
        
        #region Group

        IQueryable<Group> Groups { get; }

        void DeleteGroup(Group instance);

        void SaveGroup(Group instance);

        #endregion 

        #region Test

        IQueryable<Test> Tests { get; }

        void DeleteTest(Test instance);

        void SaveTest(Test instance);

        #endregion 

        #region User

        IQueryable<User> Users { get; }

        void DeleteUser(User instance);

        void SaveUser(User instance);

        #endregion 
    }
}
