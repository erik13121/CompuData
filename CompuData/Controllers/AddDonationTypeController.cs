using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddDonationTypeController : Controller
    {
        // GET: AddDonationType
        public ActionResult Index()
        {
            var type = new Models.DonationType();
            return View(type);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.DonationType model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                if (db.Donation_Type.Count() > 0)
                {
                    var item = db.Donation_Type.OrderByDescending(a => a.TypeID).FirstOrDefault();

                    db.Donation_Type.Add(new CodeFirst.Donation_Type
                    {
                        TypeID = item.TypeID + 1,
                        TypeName = model.TypeName
                    });
                }
                else
                {
                    db.Donation_Type.Add(new CodeFirst.Donation_Type
                    {
                        TypeID = 1,
                        TypeName = model.TypeName
                    });
                }
                
                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "DonationType");
            }

            return View("Index", model);
        }
    }
}