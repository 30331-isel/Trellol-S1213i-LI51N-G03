using RepositoryFactory;
using RepositoryInterfaces;
using System.Collections.Generic;
namespace Security
{
    public class TrellolRoleProvider
    {

        public static readonly string UnconfirmedUser = "UnconfirmedUser";
        public static readonly string ConfirmedUser = "ConfirmedUser";
        public static readonly string Admin = "Admin";

        private static readonly IRoleRepository _repoRole = RoleRepositoryLocator.Get();

        public static void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            _repoRole.AddUsersToRoles(usernames, roleNames);
        }

        public static void CreateRole(string roleName)
        {
            _repoRole.Add(roleName);
        }

        public static void DeleteRole(string roleName)
        {
            _repoRole.Remove(roleName);
        }

        public static IEnumerable<string> GetAllRoles()
        {
            return _repoRole.GetAll();
        }

        public static IEnumerable<string> GetRolesForUser(string username)
        {
            return _repoRole.GetRolesForUser(username);
        }

        //public override string[] GetUsersInRole(string roleName)
        //{
        //    throw new System.NotImplementedException();
        //}

        public static bool IsUserInRole(string username, string roleName)
        {
            return _repoRole.IsUserInRole(username, roleName);
        }

        //public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        //{
        //    throw new System.NotImplementedException();
        //}

        public static bool RoleExists(string roleName)
        {
            return _repoRole.Exists(roleName);
        }
    }
}
