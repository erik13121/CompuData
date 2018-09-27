using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyDonorPersonController : Controller
    {
        // GET: ModifyDonorPerson
        public ActionResult Index(string DonorPersonID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.Donor_Person myModel = new Models.Donor_Person();
            if (DonorPersonID != null)
            {
                var intDonorPersonID = Int32.Parse(DonorPersonID);
                var myDonorPerson = db.Donor_Person.Where(i => i.DonorPID == intDonorPersonID).FirstOrDefault();

                myModel.DonorPID = myDonorPerson.DonorPID;
                myModel.FirstName = myDonorPerson.FirstName;
                myModel.MiddleName = myDonorPerson.MiddleName;
                myModel.SecondName = myDonorPerson.SecondName;
                myModel.Initials = myDonorPerson.Initials;
                myModel.CellNum = myDonorPerson.CellNum;
                myModel.PersonalEmail = myDonorPerson.PersonalEmail;
                myModel.Thanked = myDonorPerson.Thanked;
                myModel.StreetAddress = myDonorPerson.StreetAddress;
                myModel.City = myDonorPerson.City;
                myModel.AreaCode = myDonorPerson.AreaCode;

                return View(myModel);
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyDonorPersonDetails(string donorPID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyDonorPerson", new { donorPID = donorPID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Donor_Person model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var DonorPerson = db.Donor_Person.Where(v => v.DonorPID == model.DonorPID).SingleOrDefault();

                if (DonorPerson != null)
                {
                    DonorPerson.DonorPID = model.DonorPID;
                    DonorPerson.FirstName = model.FirstName;
                    DonorPerson.MiddleName = model.MiddleName;
                    DonorPerson.SecondName = model.SecondName;
                    DonorPerson.Initials = model.Initials;
                    DonorPerson.CellNum = model.CellNum;
                    DonorPerson.PersonalEmail = model.PersonalEmail;
                    DonorPerson.Thanked = model.Thanked;
                    DonorPerson.StreetAddress = model.StreetAddress;
                    DonorPerson.City = model.City;
                    DonorPerson.AreaCode = model.AreaCode;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "DonorPerson");
            }

            return View("Index", model);
        }
    }
}