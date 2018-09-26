using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyDonorOrgController : Controller
    {
        // GET: ModifyDonorOrg
        public ActionResult Index(string DonorOrgID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.Donor_Org myModel = new Models.Donor_Org();
            if (DonorOrgID != null)
            {
                var intDonorOrgID = Int32.Parse(DonorOrgID);
                var myDonorOrg = db.Donor_Org.Where(i => i.DonorOrgID == intDonorOrgID).FirstOrDefault();

                myModel.DonorOrgID = myDonorOrg.DonorOrgID;
                myModel.OrgName = myDonorOrg.OrgName;
                myModel.ContactEmail = myDonorOrg.ContactEmail;
                myModel.ContactNum = myDonorOrg.ContactNum;
                myModel.Thanked = myDonorOrg.Thanked;
                myModel.StreetAddress = myDonorOrg.StreetAddress;
                myModel.City = myDonorOrg.City;
                myModel.AreaCode = myDonorOrg.AreaCode;

                return View(myModel);
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyDonorOrgDetails(string DonorOrgID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyDonorOrg", new { DonorOrgID = DonorOrgID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Donor_Org model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var DonorOrg = db.Donor_Org.Where(v => v.DonorOrgID == model.DonorOrgID).SingleOrDefault();

                if (DonorOrg != null)
                {
                    DonorOrg.DonorOrgID = model.DonorOrgID;
                    DonorOrg.OrgName = model.OrgName;
                    DonorOrg.ContactNum = model.ContactNum;
                    DonorOrg.ContactEmail = model.ContactEmail;
                    DonorOrg.Thanked = model.Thanked;
                    DonorOrg.StreetAddress = model.StreetAddress;
                    DonorOrg.City = model.City;
                    DonorOrg.AreaCode = model.AreaCode;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "DonorOrg");
            }

            return View("Index", model);
        }
    }
}