using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddQuantityTypeController : Controller
    {
        // GET: AddQuantityType
        public ActionResult Index()
        {
            var type = new Models.QuantityType();
            return View(type);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.QuantityType model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                if (db.Quantity_Type.Count() > 0)
                {
                    var item = db.Quantity_Type.OrderByDescending(a => a.QuantityTypeID).FirstOrDefault();

                    db.Quantity_Type.Add(new CodeFirst.Quantity_Type
                    {
                        QuantityTypeID = item.QuantityTypeID + 1,
                        Description = model.Description
                    });
                }
                else
                {
                    db.Quantity_Type.Add(new CodeFirst.Quantity_Type
                    {
                        QuantityTypeID = 1,
                        Description = model.Description
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "QuantityType");
            }

            return View("Index", model);
        }
    }
}