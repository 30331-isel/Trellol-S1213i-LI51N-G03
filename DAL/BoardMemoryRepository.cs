namespace DAL
{
    using System.Collections.Generic;
    using System.Linq;
    using EntitiesLogic.Entities;
    using DAL.Exceptions;
    using RepositoryInterfaces;

    public class BoardMemoryRepository : IBoardRepository
    {
        private readonly IDictionary<string, Board> _repo = new Dictionary<string, Board>();

        public IEnumerable<Board> GetAll()
        {
            return _repo.Values;
        }

        public IEnumerable<Board> GetSome(params string[] ids)
        {
            return ids.Select(id => _repo[id]);
        }

        public Board GetById(params string[] id)
        {
            return _repo[id[0]];
        }
         
        public void Add(Board t)
        {
            if (_repo.ContainsKey(t.Name))
                throw new DuplicateKeyException();
            _repo[t.Name]= t;
        }

        public void Edit(Board t)
        {
            _repo[t.Name] = t;
        }

        public void Remove(Board t)
        {
            _repo.Remove(t.Name);
        }



        public bool Exists(params string[] ids)
        {
            if (_repo.ContainsKey(ids[0])) 
                return true;

            return false;
        }
    }
}