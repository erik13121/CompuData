using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddRefreshmentController : Controller
    {
        // GET: AddRefreshment
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var refreshment = new Models.Refreshment();
            return View(refreshment);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Refreshment model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Refreshments.Count() > 0)
                {
                    var item = db.Refreshments.OrderByDescending(a => a.RefreshmentID).FirstOrDefault();

                    db.Refreshments.Add(new CodeFirst.Refreshment
                    {
                        RefreshmentID = item.RefreshmentID + 1,
                        Name = model.Name,
                        Description = model.Description,
                        UnitPrice = model.UnitPrice,

                    });
                }
                else
                {
                    db.Refreshments.Add(new CodeFirst.Refreshment
                    {
                        RefreshmentID = 1,
                        Name = model.Name,
                        Description = model.Description,
                        UnitPrice = model.UnitPrice,

                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "Refreshment");
            }

            return View("Index", model);
        }
    }
}