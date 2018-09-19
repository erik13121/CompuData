using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SoftwareLicensesDetailsController : Controller
    {
        // GET: SoftwareLicensesDetails
        public ActionResult Index(string LicenseID)
        {
            Models.SoftwareLicenses myModel = new Models.SoftwareLicenses();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (LicenseID != null)
            {
                var intLicID = Int32.Parse(LicenseID);
                var myLicence = db.Software_Licenses.Where(i => i.LicenceID == intLicID).FirstOrDefault();

                myModel.LicenceID = myLicence.LicenceID;
                myModel.SoftwareName = myLicence.SoftwareName;
                myModel.ActivationPeriodInMonths = myLicence.ActivationPeriodInMonths;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToEquipTypeDetails(string LicenceID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "SoftwareLicensesDetails", new { LicenceID = LicenceID });
            return Json(new { Url = redirectUrl });
        }
    }
}