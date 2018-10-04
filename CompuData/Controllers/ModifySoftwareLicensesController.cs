using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifySoftwareLicensesController : Controller
    {
        // GET: ModifySoftwareLicenses
        public ActionResult Index(string LicenceID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.SoftwareLicenses myModel = new Models.SoftwareLicenses();
            if (LicenceID != null)
            {
                var intLicID = Int32.Parse(LicenceID);
                var mylicence = db.Software_Licenses.Where(i => i.LicenceID == intLicID).FirstOrDefault();

                myModel.LicenceID = mylicence.LicenceID;
                myModel.SoftwareName = mylicence.SoftwareName;
                myModel.ActivationPeriodInMonths = mylicence.ActivationPeriodInMonths;

                return View(myModel);
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.SoftwareLicenses model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();

            if (ModelState.IsValid)
            {
                Models.SoftwareLicenses myModel = new Models.SoftwareLicenses();

                var myLicence = db.Software_Licenses.Where(i => i.LicenceID == model.LicenceID).FirstOrDefault();

                myModel.LicenceID = myLicence.LicenceID;
                myModel.SoftwareName = myLicence.SoftwareName;
                myModel.ActivationPeriodInMonths = myLicence.ActivationPeriodInMonths;


                return View(myModel);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifySoftwareLicensesDetails(string licenceID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifySoftwareLicenses", new { licenceID = licenceID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.SoftwareLicenses model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var Licence = db.Software_Licenses.Where(v => v.LicenceID == model.LicenceID).SingleOrDefault();

                if (Licence != null)
                {
                    Licence.LicenceID = model.LicenceID;
                    Licence.SoftwareName = model.SoftwareName;
                    Licence.ActivationPeriodInMonths = model.ActivationPeriodInMonths;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "SoftwareLicenses");
            }

            return View("Index", model);
        }
    }
}