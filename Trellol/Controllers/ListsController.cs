using EntitiesLogic.Entities;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Trellol.Controllers
{
    public class ListsController : Controller
    {
        //
        // GET: /Lists/Create

        public ActionResult Create(string boardId)
        {
            ViewBag.Title = "Create";
            ViewBag.SubTitle = "A New List";
            ViewBag.ButtonText = "Create";
            return View("Form", new List { BoardName = boardId });
        }

        //
        // POST: /Lists/Create

        [HttpPost]
        public ActionResult Create(List list)
        {
            try
            {
                AppServices.AddList(list);

                return RedirectToAction("Index", "Boards");
            }
            catch
            {
                return View("Form");
            }
        }

        //
        // GET: /Lists/Edit/5

        public ActionResult Edit(string boardId, string listId)
        {
            ViewBag.Title = "Edit";
            ViewBag.SubTitle = String.Format("\"{0}\" Board", listId);
            ViewBag.ButtonText = "Edit";
            return View("Form", AppServices.GetList(boardId, listId));
        }

        //
        // POST: /Lists/Edit/5

        [HttpPost]
        public ActionResult Edit(string listId, List list)
        {
            try
            {
                AppServices.EditList(list);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Lists/Delete/5

        public ActionResult Delete(string boardId, string listId)
        {
            List l = AppServices.GetList(boardId, listId);
            if (AppServices.GetCardsFromList(l).FirstOrDefault(c => c.isArchived) == null)
            {
                ViewBag.Action = "Delete";
                ViewBag.ArchivedCards = false;
            }
            else
            {
                ViewBag.Action = "DeleteListWithArchivedCards";
                ViewBag.ArchivedCards = true;
            }
            return View(l);
            
        }

        //
        // POST: /Lists/Delete/5

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmptyList(string boardId, string listId)
        {
            try
            {
                List list = AppServices.GetList(boardId, listId);
                AppServices.RemoveList(list);

                return RedirectToAction("Index", "Boards");
            }
            catch
            {
                return View("Index");
            }
        }


        [HttpPost]
        public ActionResult DeleteListWithArchivedCards(string boardId, string listId)
        {
            try
            {
                List list = AppServices.GetList(boardId, listId);
                List<Card> cards = AppServices.GetCardsFromList(list).Where(c => c.isArchived).ToList();
                foreach(Card cardArchived in cards)
                {
                    AppServices.RemoveCard(cardArchived);
                }

                AppServices.RemoveList(list);

                return RedirectToAction("Index", "Boards");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult ValidationInfo(string boardId)
        {
            return Json(PresentationUtils.getValidationInfo<List>(
                AppServices.GetAllListsFromBoard(boardId), 
                n => n.Name ), JsonRequestBehavior.AllowGet);
        }

    }
}
