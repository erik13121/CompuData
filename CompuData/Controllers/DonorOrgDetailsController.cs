using DataTables.AspNet.Core;
using CompuData.CodeFirst;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonorOrgDetailsController : Controller
    {
        // GET: DonorOrgDetails
        public ActionResult Index(string donorOrgID)
        {
            Models.Donor_Org myModel = new Models.Donor_Org();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (donorOrgID != null)
            {
                var intDonoOrgID = Int32.Parse(donorOrgID);
                var myDonorOrg = db.Donor_Org.Where(i => i.DonorOrgID == intDonoOrgID).FirstOrDefault();

                myModel.DonorOrgID = myDonorOrg.DonorOrgID;
                myModel.OrgName = myDonorOrg.OrgName;
                myModel.ContactEmail = myDonorOrg.ContactEmail;
                myModel.ContactNum = myDonorOrg.ContactNum;
                myModel.Thanked = myDonorOrg.Thanked;
                myModel.StreetAddress = myDonorOrg.StreetAddress;
                myModel.City = myDonorOrg.City;
                myModel.AreaCode = myDonorOrg.AreaCode;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToDonorOrgDetails(string donorOrgID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonorOrgDetails", new { donorOrgID = donorOrgID });
            return Json(new { Url = redirectUrl });
        }
    }
}