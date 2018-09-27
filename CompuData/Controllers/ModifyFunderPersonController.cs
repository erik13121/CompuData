using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyFunderPersonController : Controller
    {
        // GET: ModifyFunderPerson
        public ActionResult Index(string funderPersonID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (funderPersonID != null)
            {
                Models.Funder_Person myModel = new Models.Funder_Person();

                var intFundOrgID = Int32.Parse(funderPersonID);
                var myFunderPerson = db.Funder_Person.Where(i => i.FunderPersonID == intFundOrgID).FirstOrDefault();

                myModel.FunderPersonID = myFunderPerson.FunderPersonID;
                myModel.FirstName = myFunderPerson.FirstName;
                myModel.MiddleName = myFunderPerson.MiddleName;
                myModel.LastName = myFunderPerson.LastName;
                myModel.PersonalEmail = myFunderPerson.PersonalEmail;
                myModel.CellNum = myFunderPerson.CellNum;
                myModel.Bank = myFunderPerson.Bank;
                myModel.AccountNumber = myFunderPerson.AccountNumber;
                myModel.BranchCode = myFunderPerson.BranchCode;
                myModel.StreetAddress = myFunderPerson.StreetAddress;
                myModel.City = myFunderPerson.City;
                myModel.AreaCode = myFunderPerson.AreaCode;
                myModel.Thanked = myFunderPerson.Thanked;
                myModel.TypeID = myFunderPerson.TypeID;
                myModel.ProjectID = myFunderPerson.ProjectID;

                myModel.FunderTypes = db.Funder_Type.ToList();
                myModel.Project = db.Projects.ToList();
                return View(myModel);
            }

            Models.Funder_Person model = new Models.Funder_Person();
            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyFunderPersonDetails(string funderPersonID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyFunderPerson", new { funderPersonID = funderPersonID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Funder_Person model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {

                var myFunderPerson = db.Funder_Person.Where(v => v.FunderPersonID == model.FunderPersonID).SingleOrDefault();

                if (myFunderPerson != null)
                {
                    myFunderPerson.FunderPersonID = model.FunderPersonID;
                    myFunderPerson.FirstName = model.FirstName;
                    myFunderPerson.MiddleName = model.MiddleName;
                    myFunderPerson.LastName = model.LastName;
                    myFunderPerson.PersonalEmail = model.PersonalEmail;
                    myFunderPerson.CellNum = model.CellNum;
                    myFunderPerson.Bank = model.Bank;
                    myFunderPerson.AccountNumber = model.AccountNumber;
                    myFunderPerson.BranchCode = model.BranchCode;
                    myFunderPerson.StreetAddress = model.StreetAddress;
                    myFunderPerson.City = model.City;
                    myFunderPerson.AreaCode = model.AreaCode;
                    myFunderPerson.Thanked = model.Thanked;
                    myFunderPerson.TypeID = model.TypeID;
                    myFunderPerson.ProjectID = model.ProjectID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "FunderPerson");
            }

            model.FunderTypes = db.Funder_Type.ToList();
            model.Project = db.Projects.ToList();
            return View("Index", model);
        }
    }
}