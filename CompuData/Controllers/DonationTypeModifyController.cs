using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonationTypeModifyController : Controller
    {
        // GET: DonationTypeModify
        public ActionResult Index(string typeID)
        {
            Models.DonationType myModel = new Models.DonationType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (typeID != null)
            {
                var intTypeID = Int32.Parse(typeID);
                var myType = db.Donation_Type.Where(i => i.TypeID == intTypeID).FirstOrDefault();

                myModel.TypeID = myType.TypeID;
                myModel.TypeName = myType.TypeName;

                db.SaveChanges();
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.DonationType model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var myType = db.Donation_Type.Where(i => i.TypeID == model.TypeID).FirstOrDefault();

                model.TypeID = myType.TypeID;
                model.TypeName = myType.TypeName;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Update([Bind(Prefix = "")]Models.DonationType model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                var type = db.Donation_Type.Where(v => v.TypeID == model.TypeID).SingleOrDefault();

                if (type != null)
                {
                    type.TypeID = model.TypeID;
                    type.TypeName = model.TypeName;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "DonationType");
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyDonationTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonationTypeModify", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }
    }
}