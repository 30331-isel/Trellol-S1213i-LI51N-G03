﻿using EntitiesLogic.Entities;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Trellol.Infrastructure.Filters;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace Trellol.Controllers
{

    public class BoardsController : Controller
    {
        //
        // GET: /Boards/
        [Authentication]
        public ActionResult Index()
        {
            return View(AppServices.GetAllBoards());
        }

        //
        // GET: /Boards/{id}

        public ActionResult Details(string id)
        {
            try
            {
                var board = AppServices.GetBoard(id);
                var lists = PresentationData.GetListsWithCardsOrderly(board);
                ViewBag.Lists = lists;
                return View(board);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        public JsonResult Result(String search)
        {
            return Json(PresentationData.GetBoardResults(search, 5), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Boards/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Boards/Create
        [HttpPost]
        public ActionResult Create(Board board)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (AppServices.ExistBoard(board.Name))
                    {
 
                        ModelState.AddModelError("Name", "There is already a Board with that name");
                        return View();
                    }

                    AppServices.AddBoard(board);
                    return RedirectToAction("Index", "Boards");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        //
        // GET: /Boards/{id}/Edit
        //[Authentication(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            return View(AppServices.GetBoard(id));
        }

        //
        // POST: /Boards/{id}/Edit/
        [HttpPost]
        public ActionResult Edit(Board board)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AppServices.EditBoard(board);

                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    return View();
                }
            }
            return View(AppServices.GetBoard(board.Name));
        }

        public JsonResult ValidationInfo()
        {
            return Json(PresentationUtils.getValidationInfo<Board>(
                AppServices.GetAllBoards(),
                n => n.Name), JsonRequestBehavior.AllowGet);
        }

    }
}
