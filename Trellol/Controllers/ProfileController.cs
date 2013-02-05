using Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntitiesLogic.Entities;
using System.IO;


namespace Trellol.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/Details/5

        public ActionResult Details(string id)
        {
            return View(TrellolUserProvider.GetUser(id));
        }

        //
        // GET: /Profile/Edit/5

        public ActionResult Edit(string id)
        {
            return View(TrellolUserProvider.GetUser(id));
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, User user, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User u = TrellolUserProvider.GetUser(user.Username);
                    //if (image != null)
                    //{
                    //    string fileName = String.Format("{0}{1}", user.Username, Path.GetExtension(image.FileName));

                    //    string filePath = Path.Combine(HttpContext.Server.MapPath("../../Uploads/ProfileImages"),
                    //                                    fileName);
                    //    image.SaveAs(filePath);

                    //    u.ImageProfile = filePath;
                    //    u.ImageMimeType = image.ContentType;
                    //}
                    u.ImageProfile = PresentationUtils.UploadProfileImage(image);


                    TrellolUserProvider.UpdateUser(u);

                    return RedirectToAction("Details", "Profile", new { id = user.Username });
                }
                catch
                {
                    return View(user);
                }
            }
            return View(user);
        }

        //public FilePathResult GetImage(string id)
        //{
        //    User user = TrellolUserProvider.GetUser(id);
        //    if (user != null)
        //    {
        //        return File(user.ImageProfile, user.ImageMimeType, "ProfileImage.jpeg");
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
