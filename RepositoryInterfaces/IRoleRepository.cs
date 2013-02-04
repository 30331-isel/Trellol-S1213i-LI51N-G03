
using System.Collections.Generic;
namespace RepositoryInterfaces
{
    public interface IRoleRepository : IRepository<string>
    {
        IEnumerable<string> GetRolesForUser(string username);
        void AddUsersToRoles(string[] usernames, string[] roleNames);
        bool IsUserInRole(string username, string roleName);
    }
}
