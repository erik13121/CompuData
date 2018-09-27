using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class FunderPersonDetailsController : Controller
    {
        // GET: FunderPersonDetails
        public ActionResult Index(string funderPersonID)
        {
            Models.Funder_Person myModel = new Models.Funder_Person();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (funderPersonID != null)
            {
                var intFunderPersonID = Int32.Parse(funderPersonID);
                var myFunderPerson = db.Funder_Person.Where(i => i.FunderPersonID == intFunderPersonID).FirstOrDefault();
                var mytypeID = db.Funder_Type.Where(i => i.TypeID == myFunderPerson.TypeID).FirstOrDefault();
                var myProjectID = db.Projects.Where(i => i.ProjectID == myFunderPerson.ProjectID).FirstOrDefault();

                myModel.FunderPersonID = myFunderPerson.FunderPersonID;
                myModel.FirstName = myFunderPerson.FirstName;
                myModel.MiddleName = myFunderPerson.MiddleName;
                myModel.LastName = myFunderPerson.LastName;
                myModel.Initials = myFunderPerson.Initials;
                myModel.CellNum = myFunderPerson.CellNum;
                myModel.PersonalEmail = myFunderPerson.PersonalEmail;
                myModel.Bank = myFunderPerson.Bank;
                myModel.AccountNumber = myFunderPerson.AccountNumber;
                myModel.BranchCode = myFunderPerson.BranchCode;
                myModel.StreetAddress = myFunderPerson.StreetAddress;
                myModel.City = myFunderPerson.City;
                myModel.AreaCode = myFunderPerson.AreaCode;
                myModel.Thanked = myFunderPerson.Thanked;
                myModel.ProjectName = myProjectID != null ? myProjectID.ProjectName : "None";
                myModel.Name = mytypeID.Name;

            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToFunderPersonDetails(string funderPersonID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "FunderPersonDetails", new { funderPersonID = funderPersonID });
            return Json(new { Url = redirectUrl });
        }
    }
}