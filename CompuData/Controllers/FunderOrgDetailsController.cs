using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class FunderOrgDetailsController : Controller
    {
        // GET: FunderOrgDetails
        public ActionResult Index(string funderOrgID)
        {
            Models.Funder_Org myModel = new Models.Funder_Org();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (funderOrgID != null)
            {
                var intFunderOrgID = Int32.Parse(funderOrgID);
                var myFunderOrg = db.Funder_Org.Where(i => i.FunderOrgID == intFunderOrgID).FirstOrDefault();
                var mytypeID = db.Funder_Type.Where(i => i.TypeID == myFunderOrg.TypeID).FirstOrDefault();
                var myProject = db.Projects.Where(i => i.ProjectID == myFunderOrg.ProjectID).FirstOrDefault();

                myModel.FunderOrgID = myFunderOrg.FunderOrgID;
                myModel.OrgName = myFunderOrg.OrgName;
                myModel.ContactNumber = myFunderOrg.ContactNumber;
                myModel.EmailAddress = myFunderOrg.EmailAddress;
                myModel.Bank = myFunderOrg.Bank;
                myModel.AccountNumber= myFunderOrg.AccountNumber;
                myModel.BranchCode = myFunderOrg.BranchCode;
                myModel.StreetAddress = myFunderOrg.StreetAddress;
                myModel.City = myFunderOrg.City;
                myModel.AreaCode = myFunderOrg.AreaCode;
                myModel.Thanked = myFunderOrg.Thanked;
                myModel.TypeID = mytypeID.TypeID;
                myModel.ProjectID = myFunderOrg.ProjectID;

                myModel.ProjectName = myProject != null ? myProject.ProjectName : "Not linked to Project";
                myModel.Name = db.Funder_Type.Where(i => i.TypeID == mytypeID.TypeID).FirstOrDefault().Name;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToFunderOrgDetails(string funderOrgID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "FunderOrgDetails", new { funderOrgID = funderOrgID });
            return Json(new { Url = redirectUrl });
        }
    }
}