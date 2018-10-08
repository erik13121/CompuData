using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonationDetailsController : Controller
    {
        // GET: DonationDetails
        public ActionResult Index(string donationID)
        {
            Models.Donation myModel = new Models.Donation();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (donationID != null)
            {
                var intID = Int32.Parse(donationID);
                var myDonation = db.Donations.Where(i => i.DonationID == intID).FirstOrDefault();
                var myDonorPerson = db.Donor_Person.Where(i => i.DonorPID == myDonation.DonorPID).FirstOrDefault();
                var myDonorOrg = db.Donor_Org.Where(i => i.DonorOrgID == myDonation.DonorOrgID).FirstOrDefault();

                myModel.DonationID = myDonation.DonationID;
                myModel.DateDate = myDonation.DateDate;
                myModel.DonorPID = myDonation.DonorPID;
                myModel.DonorOrgID = myDonation.DonorOrgID;
                myModel.FirstName = myDonorPerson != null ? myDonorPerson.FirstName : "";
                myModel.LastName = myDonorPerson != null ? myDonorPerson.SecondName : "";
                myModel.OrgName = myDonorOrg != null ? myDonorOrg.OrgName : "";
                myModel.Lines = myDonation.Donation_Line.ToList();
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToDonationDetails(string donationID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonationDetails", new { donationID = donationID });
            return Json(new { Url = redirectUrl });
        }
    }
}