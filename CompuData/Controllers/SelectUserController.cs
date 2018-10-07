using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SelectUserController : Controller
    {
        // GET: SelectUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RedirectToSelectUser (string equipmentID)
        {
            TempData["equipmentID"] = equipmentID;
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "SelectUser", new { equipmentID = equipmentID });
            return Json(new { Url = redirectUrl });
        }
    }
}