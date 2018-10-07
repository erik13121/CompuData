using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyEquipmentSoftwareLicenseController : Controller
    {
        // GET: ModifyEquipmentSoftwareLicense
        public ActionResult Index(string lineID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.SoftwareLicensesLine myModel = new Models.SoftwareLicensesLine();
            if (lineID != null)
            {
                var intLineID = Int32.Parse(lineID);
                var myLine = db.Software_Licenses_Line.Where(i => i.LineID == intLineID).FirstOrDefault();

                myModel.LineID = myLine.LineID;
                myModel.LicenceID = myLine.LicenceID;
                myModel.EquipmentID = myLine.EquipmentID;
                myModel.LastActivatedDate = myLine.LastActivatedDate;
                myModel.Activated = myLine.Activated;

                myModel.Softwares = db.Software_Licenses.ToList();
                myModel.Equipments = db.Equipments.AsEnumerable().Select(e => new SelectListItem
                {
                    Value = e.EquipmentID.ToString(),
                    Text = e.ManufacturerName + " " + e.ModelNumber
                }).ToList();
                return View(myModel);
            }

            myModel.Softwares = db.Software_Licenses.ToList();
            myModel.Equipments = db.Equipments.AsEnumerable().Select(e => new SelectListItem
            {
                Value = e.EquipmentID.ToString(),
                Text = e.ManufacturerName + " " + e.ModelNumber
            }).ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyEquipmentSoftwareLicensesDetails(string lineID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyEquipmentSoftwareLicense", new { lineID = lineID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.SoftwareLicensesLine model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var line = db.Software_Licenses_Line.Where(v => v.LineID == model.LineID).SingleOrDefault();

                if (line != null)
                {
                    line.EquipmentID = model.EquipmentID;
                    line.LicenceID = model.LicenceID;
                    line.LastActivatedDate = model.LastActivatedDate;
                    line.Activated = model.Activated;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "EquipmentSoftwareLicenses");
            }

            model.Softwares = db.Software_Licenses.ToList();
            model.Equipments = db.Equipments.AsEnumerable().Select(e => new SelectListItem
            {
                Value = e.EquipmentID.ToString(),
                Text = e.ManufacturerName + " " + e.ModelNumber
            }).ToList();
            return View("Index", model);
        }
    }
}