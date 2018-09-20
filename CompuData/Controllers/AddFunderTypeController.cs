using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddFunderTypeController : Controller
    {
        // GET: AddFunderType
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var FunderType = new Models.FunderType();
            return View(FunderType);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.FunderType model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Funder_Type.Count() > 0)
                {
                    var item = db.Funder_Type.OrderByDescending(a => a.TypeID).FirstOrDefault();

                    db.Funder_Type.Add(new CodeFirst.Funder_Type
                    {
                        TypeID = item.TypeID + 1,
                        Name = model.Name,
                        Description = model.Description
                    });
                }
                else
                {
                    db.Funder_Type.Add(new CodeFirst.Funder_Type
                    {
                        TypeID = 1,
                        Name = model.Name,
                        Description = model.Description
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "FunderType");
            }

            return View("Index", model);
        }
    }
}