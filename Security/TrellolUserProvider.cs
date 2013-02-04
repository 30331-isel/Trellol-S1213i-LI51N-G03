using EntitiesLogic.Entities;
using RepositoryFactory;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace Security
{
    public class TrellolUserProvider
    {
        private static readonly IUserRepository _repoUser = UserRepositoryLocator.Get();

        public static bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                User user = _repoUser.GetById(username);
                if (user.Password != oldPassword) return false;
                user.Password = newPassword;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CreateUser(User user)
        {
            try
            {
                _repoUser.Add(user);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public static void DeleteUser(User user)
        {
            _repoUser.Remove(user);
        }

        //public MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        //{
        //    throw new NotImplementedException();
        //}

        public static IEnumerable<User> GetAllUsers()
        {
            return _repoUser.GetAll();
        }

        public static string GetPassword(string username)
        {
            return _repoUser.GetById(username).Password;
        }

        public static User GetUser(string username)
        {
            return _repoUser.GetById(username);
        }

        public static IEnumerable<Board> GetBoardsFromUser(string username)
        {
            return GetUser(username).Boards;
        }

        public static void AddBoardForUser(Board board, User user)
        {
            user.Boards.Add(board);
        }

        //public MembershipUser GetUser(object providerUserKey)
        //{
        //    throw new NotImplementedException();
        //}

        //public string ResetPassword(string username, string answer)
        //{
        //    throw new NotImplementedException();
        //}

        public static void UnlockUser(string userName)
        {
            _repoUser.GetById(userName).isConfirmed = true;
        }

        public static void UpdateUser(User user)
        {
            _repoUser.Edit(user);
        }

        public virtual string GetCurrentUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public static bool ValidateUser(string username, string password)
        {
            if(!_repoUser.Exists(username)) return false;
            return _repoUser.GetById(username).Password == password;
        }
    }
}
