using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddSoftwareLicensesController : Controller
    {
        // GET: AddSoftwareLicenses
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var SoftwareLic = new Models.SoftwareLicenses();
            return View(SoftwareLic);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.SoftwareLicenses model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Software_Licenses.Count() > 0)
                {
                    var item = db.Software_Licenses.OrderByDescending(a => a.LicenceID).FirstOrDefault();

                    db.Software_Licenses.Add(new CodeFirst.Software_Licenses
                    {
                        LicenceID = item.LicenceID + 1,
                        SoftwareName = model.SoftwareName,
                        ActivationPeriodInMonths = model.ActivationPeriodInMonths
                    });
                }
                else
                {
                    db.Software_Licenses.Add(new CodeFirst.Software_Licenses
                    {
                        LicenceID = 1,
                        SoftwareName = model.SoftwareName,
                        ActivationPeriodInMonths = model.ActivationPeriodInMonths
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "SoftwareLicenses");
            }

            return View("Index", model);
        }
    }
}