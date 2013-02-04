using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLogic.Entities;
using DAL.Exceptions;
using RepositoryInterfaces;
using System.Linq.Expressions;

namespace DAL
{
    public class ListMemoryRepository : IListRepository
    {
        private readonly IDictionary<string, IDictionary<string, List>> _repo = new Dictionary<string, IDictionary<string, List>>();

        public IEnumerable<List> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<List> GetSome(params string[] ids)
        {
            var repoL = _repo[ids[0]];
            return ids.Select(id => repoL[id]);
        }

        public List GetById(params string[] id)
        {
           return  _repo[id[0]][id[1]];
        }


        private void AdjustListPosition(List l, Action<List> incOrDec)
        {
            DALUtils.AdjustPosition<List>(
                    _repo[l.BoardName].Values,
                    list => list.Position >= l.Position,
                    incOrDec
                    );
        }

        public void Add(List l)
        {
            if (_repo.ContainsKey(l.BoardName) && _repo[l.BoardName].ContainsKey(l.Name))
                throw new DuplicateKeyException();
            if (!_repo.ContainsKey(l.BoardName)) _repo[l.BoardName] = new Dictionary<string, List>();
            AdjustListPosition(l, item => item.Position++);
            _repo[l.BoardName][l.Name] = l;
        }

        public void Edit(List l)
        {



            //if(listForBoards.SingleOrDefault(list =>list.Position==l.Position)!=null)
            //{
            //    foreach (List list in listForBoards.Where(list => list.Position >= l.Position))
            //        list.Position++;
            //}

            AdjustListPosition(l, item => item.Position++);

            _repo[l.BoardName][l.Name] = l;
        }



        public void Remove(List l)
        {
            _repo[l.BoardName].Remove(l.Name);
            AdjustListPosition(l, item => item.Position--);
        }

        public IEnumerable<List> GetListsFromBoard(string id)
        {
            if(!_repo.ContainsKey(id)) return null;
            return _repo[id].Values.OrderBy(l=>l.Position);
        }


        public bool Exists(params string[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
