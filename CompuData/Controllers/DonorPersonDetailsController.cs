using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonorPersonDetailsController : Controller
    {
        // GET: DonorPersonDetails
        public ActionResult Index(string DonorPersonID)
        {
            Models.Donor_Person myModel = new Models.Donor_Person();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (DonorPersonID != null)
            {
                var intDonoPID = Int32.Parse(DonorPersonID);
                var myDonorPerson = db.Donor_Person.Where(i => i.DonorPID == intDonoPID).FirstOrDefault();

                myModel.DonorPID = myDonorPerson.DonorPID;
                myModel.FirstName = myDonorPerson.FirstName;
                myModel.MiddleName = myDonorPerson.MiddleName;
                myModel.SecondName = myDonorPerson.SecondName;
                myModel.Initials = myDonorPerson.Initials;
                myModel.PersonalEmail = myDonorPerson.PersonalEmail;
                myModel.CellNum = myDonorPerson.CellNum;
                myModel.Thanked = myDonorPerson.Thanked;
                myModel.StreetAddress = myDonorPerson.StreetAddress;
                myModel.City = myDonorPerson.City;
                myModel.AreaCode = myDonorPerson.AreaCode;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToDonorPersonDetails(string donorPID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonorPersonDetails", new { donorPID = donorPID });
            return Json(new { Url = redirectUrl });
        }
    }
}