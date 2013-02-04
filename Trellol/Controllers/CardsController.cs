using EntitiesLogic.Entities;
using Logic;
using System;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace Trellol.Controllers
{
    public class CardsController : Controller
    {

        public JsonResult Results(string search)
        {
            return Json(PresentationUtils.GetCardUrlInfoResults(search), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Cards/Details/5

        public ActionResult Details(string boardId, string listId, int cardId)
        {
            return View(AppServices.GetCard(boardId, cardId));
        }

        //
        // GET: /Cards/Create

        public ActionResult Create(string boardId, string listId)
        {
            ViewBag.Title = "Create";
            ViewBag.SubTitle = "A New Card";
            ViewBag.ButtonText = "Create";
            Card c = new Card { BoardName = boardId, ListName = listId, isArchived = false };
            return View("Form", c);
        }

        //
        // POST: /Cards/Create

        [HttpPost]
        public ActionResult Create(Card card)
        {
            try
            {
                card.Id = AppServices.GetNumberOfCardsInBoard(card.BoardName) + 1;
                AppServices.AddCard(card);

                return RedirectToAction("Details", "Boards");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cards/Edit/5

        public ActionResult Edit(string boardId, string listId, int cardId)
        {
            Card c = AppServices.GetCard(boardId, cardId);
            ViewBag.Title = "Edit";
            ViewBag.SubTitle = String.Format("\"{0}\" Board", c.Name);
            ViewBag.ButtonText = "Edit";
            ViewBag.Positions = PresentationData.GetAllCardsPositions(AppServices.GetList(boardId, listId));
            return View("Form", c);
        }

        //
        // POST: /Cards/Edit/5

        [HttpPost]
        public ActionResult Edit(string boardId, string listId, string cardId, Card card)
        {
            try
            {
                card.BoardName = boardId;
                AppServices.EditCard(card);

                return RedirectToAction("Index", "Boards");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public void SaveCardOrder(String board, String list, String[] cards)
        {
            if (cards == null) return; //caso em que a lista está vazia, não há cards para fazer update.

            for (int i = 0; i < cards.Length; i++)
            {
                Card c = AppServices.GetCard(board, Convert.ToInt32(cards[i]));
                c.ListName = list;
                c.Position = i + 1;
                AppServices.EditCard(c);

            }
        }

        //
        // GET: /Cards/Archive/5

        public ActionResult Archive(string boardId, string listId, string cardId)
        {
            return View();
        }

        //
        // POST: /Cards/Archive/5

        [HttpPost]
        public ActionResult Archive(string cardId, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult ValidationInfo()
        {
            return Json(PresentationUtils.getValidationInfo<Card>(), JsonRequestBehavior.AllowGet);
        }

    }
}
