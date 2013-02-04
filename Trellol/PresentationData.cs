using System.Collections.Generic;
using System.Linq;
using Trellol;
using EntitiesLogic.Entities;
using Logic;

namespace Trellol
{
    class PresentationData
    {
        internal static IEnumerable<Card> OrderCardsFromList(List l)
        {
            return AppServices.GetCardsFromList(l)
                .Where(c => c.isArchived == false)
                .OrderBy(c => c.Position);
        }

        public static IEnumerable<Board> GetBoardResults(string search, int number)
        {
            return AppServices.GetAllBoards().Where(b => b.Name.Contains(search)).Take(number);
        } 

        public static IEnumerable<Card> GetCardResults(string search, int number)
        {
            IEnumerable<Board> boards = AppServices.GetAllBoards();
            return boards.SelectMany(board => AppServices.GetAllCardsFromBoard(board.Name).Where(c => c.Description.Contains(search))).Take(number);
        }

        public static IEnumerable<KeyValuePair<List, IEnumerable<Card>>> GetListsWithCardsOrderly(Board board)
        {
            var lists = AppServices.GetAllListsFromBoard(board.Name);
            if (lists == null) yield break;

            lists.OrderBy(l => l.Position);
            foreach (List list in lists)
                yield return new KeyValuePair<List, IEnumerable<Card>>(list, OrderCardsFromList(list));
        }

        internal static IEnumerable<int> GetAllCardsPositions(List list)
        {
            var cards = AppServices.GetCardsFromList(list);
            int pos = 1;
            int count = cards.Count();
            while (pos <= count)
            {
                yield return pos;
                pos += 1;
            }
        }
    }
}

