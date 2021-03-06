﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyQuantityTypeController : Controller
    {
        // GET: ModifyQuantityType
        public ActionResult Index(string quantityTypeID)
        {
            Models.QuantityType myModel = new Models.QuantityType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (quantityTypeID != null)
            {
                var intTypeID = Int32.Parse(quantityTypeID);
                var myType = db.Quantity_Type.Where(i => i.QuantityTypeID == intTypeID).FirstOrDefault();

                myModel.QuantityTypeID = myType.QuantityTypeID;
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
                var myType = db.Quantity_Type.Where(i => i.QuantityTypeID == model.QuantityTypeID).FirstOrDefault();

                model.QuantityTypeID = myType.QuantityTypeID;
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
                var type = db.Quantity_Type.Where(v => v.QuantityTypeID == model.QuantityTypeID).SingleOrDefault();

                if (type != null)
                {
                    type.QuantityTypeID = model.QuantityTypeID;
                    type.Description = model.Description;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "QuantityType");
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyQuantityTypeDetails(string quantityTypeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyQuantityType", new { quantityTypeID = quantityTypeID });
            return Json(new { Url = redirectUrl });
        }
    }
}