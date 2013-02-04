using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class RoleMemoryRepository : IRoleRepository
    {
        private Dictionary<string, string[]> _userRoles = new Dictionary<string, string[]>();
        private IList<string> _roles = new List<string>();
        

        public IEnumerable<string> GetAll()
        {
            return _roles;
        }

        public IEnumerable<string> GetSome(params string[] ids)
        {
            throw new NotImplementedException();
        }

        public string GetById(params string[] id)
        {
            throw new NotImplementedException();
        }

        public void Add(string roleName)
        {
            if (Exists(roleName)) return;

            _roles.Add(roleName);
        }

        public void Edit(string roleName)
        {
            throw new NotImplementedException();
        }

        public void Remove(string roleName)
        {
            _roles.Remove(roleName);
        }

        public bool Exists(params string[] ids)
        {
            return _roles.Contains(ids[0], StringComparer.InvariantCultureIgnoreCase);
        }

        public IEnumerable<string> GetRolesForUser(string username)
        {
            string[] roles;
            _userRoles.TryGetValue(username, out roles);
            return roles == null ? new string[0] : roles;
        }
        
        public void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            foreach (string username in usernames)
            {
                _userRoles[username] = roleNames;
            }
        }

        public bool IsUserInRole(string username, string roleName)
        {
            return GetRolesForUser(username).Contains(roleName, StringComparer.InvariantCultureIgnoreCase);
        }

    }
}
