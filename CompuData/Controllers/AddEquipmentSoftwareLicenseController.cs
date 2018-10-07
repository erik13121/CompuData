using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddEquipmentSoftwareLicenseController : Controller
    {
        // GET: AddEquipmentSoftwareLicense
        public ActionResult Index(string equipmentID, string licenceID)
        {
            Models.SoftwareLicensesLine line = new Models.SoftwareLicensesLine();

            line.EquipmentID = Int32.Parse(equipmentID);
            line.LicenceID = Int32.Parse(licenceID);

            return View(line);
        }

        [HttpPost]
        public ActionResult RedirectToAdd(string equipmentID, string licenceID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AddEquipmentSoftwareLicense", new { equipmentID = equipmentID, licenceID = licenceID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.SoftwareLicensesLine model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var item = db.Software_Licenses_Line.OrderByDescending(a => a.LicenceID).FirstOrDefault();

                db.Software_Licenses_Line.Add(new CodeFirst.Software_Licenses_Line
                {
                    EquipmentID = model.EquipmentID,
                    LicenceID = model.LicenceID,
                    LastActivatedDate = DateTime.Parse(model.LastActivatedDate.Value.ToString("yyyy-MM-dd")),
                    Activated = model.Activated
                });

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "EquipmentSoftwareLicenses");

            }

            return View("Index", model);
        }
    }
}