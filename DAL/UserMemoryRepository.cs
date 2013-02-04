using DAL.Exceptions;
using EntitiesLogic.Entities;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class UserMemoryRepository : IUserRepository
    {
        private readonly IDictionary<string, User> _repo = new Dictionary<string, User>();

        public IEnumerable<User> GetAll()
        {
            return _repo.Values;
        }

        public IEnumerable<User> GetSome(params string[] ids)
        {
            throw new NotImplementedException();
        }

        public User GetById(params string[] id)
        {
            return _repo[id[0]];
        }

        public void Add(User u)
        {
            if (_repo.ContainsKey(u.Username))
                throw new DuplicateKeyException();
            _repo[u.Username] = u;
        }

        public void Edit(User u)
        {
            _repo[u.Username] = u;
        }

        public void Remove(User u)
        {
            _repo.Remove(u.Username);
        }


        public bool Exists(params string[] ids)
        {
            return _repo.ContainsKey(ids[0]);
        }

        public IEnumerable<Board> GetBoardsFromUser(string username)
        {
            return GetById(username).Boards;
        }
    }
}
