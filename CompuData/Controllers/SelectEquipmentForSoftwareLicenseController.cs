using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SelectEquipmentForSoftwareLicenseController : Controller
    {
        // GET: SelectEquipmentForSoftwareLicense
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RedirectToSelectEquipment(string licenceID)
        {
            TempData["licenceID"] = licenceID;
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "SelectEquipmentForSoftwareLicense");
            return Json(new { Url = redirectUrl });
        }
    }
}