using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyQuantityTypeController : Controller
    {
        // GET: ModifyQuantityType
        public ActionResult Index(string quatityTypeID)
        {
            Models.QuantityType myModel = new Models.QuantityType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (quatityTypeID != null)
            {
                var intTypeID = Int32.Parse(quatityTypeID);
                var myType = db.Quantity_Type.Where(i => i.QuatityTypeID == intTypeID).FirstOrDefault();

                myModel.QuatityTypeID = myType.QuatityTypeID;
                myModel.Description = myType.Description;

                db.SaveChanges();
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.QuantityType model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var myType = db.Quantity_Type.Where(i => i.QuatityTypeID == model.QuatityTypeID).FirstOrDefault();

                model.QuatityTypeID = myType.QuatityTypeID;
                model.Description = myType.Description;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Update([Bind(Prefix = "")]Models.QuantityType model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                var type = db.Quantity_Type.Where(v => v.QuatityTypeID == model.QuatityTypeID).SingleOrDefault();

                if (type != null)
                {
                    type.QuatityTypeID = model.QuatityTypeID;
                    type.Description = model.Description;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "QuantityType");
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyQuantityTypeDetails(string quatityTypeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyQuantityType", new { quatityTypeID = quatityTypeID });
            return Json(new { Url = redirectUrl });
        }
    }
}