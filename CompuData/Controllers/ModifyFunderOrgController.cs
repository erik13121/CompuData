using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyFunderOrgController : Controller
    {
        // GET: ModifyFunderOrg
        public ActionResult Index(string funderOrgID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (funderOrgID != null)
            {
                Models.Funder_Org myModel = new Models.Funder_Org();                

                var intFundOrgID = Int32.Parse(funderOrgID);
                var myFunderOrg = db.Funder_Org.Where(i => i.FunderOrgID == intFundOrgID).FirstOrDefault();

                myModel.FunderOrgID = myFunderOrg.FunderOrgID;
                myModel.OrgName = myFunderOrg.OrgName;
                myModel.ContactNumber = myFunderOrg.ContactNumber;
                myModel.EmailAddress = myFunderOrg.EmailAddress;
                myModel.Bank = myFunderOrg.Bank;
                myModel.AccountNumber = myFunderOrg.AccountNumber;
                myModel.BranchCode = myFunderOrg.BranchCode;
                myModel.StreetAddress = myFunderOrg.StreetAddress;
                myModel.City = myFunderOrg.City;
                myModel.AreaCode = myFunderOrg.AreaCode;
                myModel.Thanked = myFunderOrg.Thanked;
                myModel.TypeID = myFunderOrg.TypeID;
                myModel.ProjectID = myFunderOrg.ProjectID;

                myModel.FunderTypes = db.Funder_Type.ToList();
                myModel.Project = db.Projects.ToList();
                return View(myModel);
            }

            Models.Funder_Org model = new Models.Funder_Org();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Models.Funder_Org model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                Models.Funder_Org myModel = new Models.Funder_Org();
                var myFunderOrg = db.Funder_Org.Where(i => i.FunderOrgID == model.FunderOrgID).FirstOrDefault();

                myModel.FunderOrgID = myFunderOrg.FunderOrgID;
                myModel.OrgName = myFunderOrg.OrgName;
                myModel.ContactNumber = myFunderOrg.ContactNumber;
                myModel.EmailAddress = myFunderOrg.EmailAddress;
                myModel.Bank = myFunderOrg.Bank;
                myModel.AccountNumber = myFunderOrg.AccountNumber;
                myModel.BranchCode = myFunderOrg.BranchCode;
                myModel.StreetAddress = myFunderOrg.StreetAddress;
                myModel.City = myFunderOrg.City;
                myModel.AreaCode = myFunderOrg.AreaCode;
                myModel.Thanked = myFunderOrg.Thanked;
                myModel.TypeID = myFunderOrg.TypeID;
                myModel.ProjectID = myFunderOrg.ProjectID;

                return View(myModel);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyFunderOrgDetails(string funderOrgID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyFunderOrg", new { funderOrgID = funderOrgID });
            return Json(new { Url = redirectUrl });
        }
        
        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Funder_Org model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var myFunderOrg = db.Funder_Org.Where(v => v.FunderOrgID == model.ProjectID).SingleOrDefault();

                if (myFunderOrg != null)
                {
                    myFunderOrg.FunderOrgID = model.FunderOrgID;
                    myFunderOrg.OrgName = model.OrgName;
                    myFunderOrg.ContactNumber = model.ContactNumber;
                    myFunderOrg.EmailAddress = model.EmailAddress;
                    myFunderOrg.Bank = model.Bank;
                    myFunderOrg.AccountNumber = model.AccountNumber;
                    myFunderOrg.BranchCode = model.BranchCode;
                    myFunderOrg.StreetAddress = model.StreetAddress;
                    myFunderOrg.City = model.City;
                    myFunderOrg.AreaCode = model.AreaCode;
                    myFunderOrg.Thanked = model.Thanked;
                    myFunderOrg.TypeID = model.TypeID;
                    myFunderOrg.ProjectID = model.ProjectID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "FunderOrg");
            }

            return View("Index", model);
        }
    }
}