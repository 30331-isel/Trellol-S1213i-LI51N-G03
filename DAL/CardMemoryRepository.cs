using System;
using System.Collections.Generic;
using System.Linq;
using EntitiesLogic.Entities;
using DAL.Exceptions;
using RepositoryInterfaces;

namespace DAL
{
    public class CardMemoryRepository : ICardRepository
    {
        private readonly IDictionary<string, IDictionary<int, Card>> _repo = new Dictionary<string, IDictionary<int, Card>>();

        public IEnumerable<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Card> GetSome(params string[] ids)
        {
            var repoL = _repo[ids[0]];
            return ids.Select(id => repoL[int.Parse(ids[1])]);
        }

        public Card GetById(params string[] id)
        {
            return _repo[id[0]][int.Parse(id[1])];
        }

        private void AdjustCardPosition(Card c)
        {
            DALUtils.AdjustPosition<Card>(
                _repo[c.BoardName].Values.Where(card => card.ListName == c.ListName),
                card => card.Position >= c.Position,
                item => item.Position++
            );
        }

        public void Add(Card c)
        {
            if (_repo.ContainsKey(c.BoardName) && _repo[c.BoardName].ContainsKey(c.Id))
                throw new DuplicateKeyException();
            if (!_repo.ContainsKey(c.BoardName)) _repo[c.BoardName] = new Dictionary<int, Card>();


            AdjustCardPosition(c);

            _repo[c.BoardName][c.Id] = c;
        }

        public void Edit(Card c)
        {

            AdjustCardPosition(c);

            _repo[c.BoardName][c.Id] = c;
        }

        public void Remove(Card t)
        {
            _repo[t.BoardName].Remove(t.Id);
        }


        public IEnumerable<Card> GetCardsFromBoard(string id)
        {
            if (!_repo.ContainsKey(id)) return null;
            return _repo[id].Values;
        }

        public int Count(string board)
        {
            IEnumerable<Card> cards = GetCardsFromBoard(board);
            if (cards == null) return 0;
            return cards.Count();
        }



        public bool Exists(params string[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
