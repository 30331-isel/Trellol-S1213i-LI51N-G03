using System;
using System.Collections.Generic;
using System.Linq;
using EntitiesLogic.Entities;
using RepositoryInterfaces;
using RepositoryFactory;

namespace Logic
{
    public class AppServices
    {
        private static readonly ICardRepository _repoCard = CardRepositoryLocator.Get();
        private static readonly IListRepository _repoList = ListRepositoryLocator.Get();
        private static readonly IBoardRepository _repoBoard = BoardRepositoryLocator.Get();
        private static readonly IUserRepository _repoUser = UserRepositoryLocator.Get();

        public static IEnumerable<Card> GetAllCardsFromBoard(string boardName)
        {
            return _repoCard.GetCardsFromBoard(boardName);
        }

        public static Card GetCard(string boardName, int cardId)
        {
            return _repoCard.GetById(boardName, cardId.ToString());
        }

        public static void EditCard(Card card)
        {
            _repoCard.Edit(card);
        }

        public static IEnumerable<Card> GetCardsFromList(List t)
        {
            var cards = GetAllCardsFromBoard(t.BoardName);
            if (cards == null) return null;
            return cards.Where(card => card.ListName == t.Name);
        }

        public static void AddCard(Card card)
        {
            _repoCard.Add(card);
        }

        public static void RemoveCard(Card card)
        {
            _repoCard.Remove(card);
        }

        public static IEnumerable<List> GetAllListsFromBoard(string boardName)
        {
            return _repoList.GetListsFromBoard(boardName);
        }

        public static List GetList(string boardName, string listName)
        {
            return _repoList.GetById(boardName, listName);
        }

        public static void EditList(List list)
        {
            _repoList.Edit(list);
        }

        public static void AddList(List list)
        {
            _repoList.Add(list);
        }

        public static void RemoveList(List list)
        {
            _repoList.Remove(list);
        }

        public static IEnumerable<Board> GetAllBoards()
        {
            return _repoBoard.GetAll();
        }

        public static Board GetBoard(string boardName)
        {
            return _repoBoard.GetById(boardName);
        }

        public static void EditBoard(Board board)
        {
            _repoBoard.Edit(board);
        }

        public static void AddBoard(Board board)
        {
            _repoBoard.Add(board);
        }

        public static void RemoveBoard(Board board)
        {
            _repoBoard.Remove(board);
        }

        public static int GetNumberOfCardsInBoard(string boardName)
        {
            return _repoCard.Count(boardName);
        }

        public static bool ExistBoard(string b)
        {
            return _repoBoard.Exists(b);
        }

    }
}
