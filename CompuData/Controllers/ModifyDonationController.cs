using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyDonationController : Controller
    {
        // GET: ModifyDonation
        public ActionResult Index(string donationID)
        {
            Models.Donation myModel = new Models.Donation();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (donationID != null)
            {
                var intDonationID = Int32.Parse(donationID);
                var myDonation = db.Donations.Where(i => i.DonationID == intDonationID).FirstOrDefault();

                myModel.DonationID = myDonation.DonationID;
                myModel.DateDate = myDonation.DateDate;
                myModel.DonorPID = myDonation.DonorPID;
                myModel.DonorOrgID = myDonation.DonorOrgID;

                myModel.DonorPeople = db.Donor_Person.AsEnumerable().Select(d => new SelectListItem
                {
                    Value = d.DonorPID.ToString(),
                    Text = d.FirstName + " " + d.SecondName
                }).ToList();

                myModel.DonorOrgs = db.Donor_Org.AsEnumerable().Select(o => new SelectListItem
                {
                    Value = o.DonorOrgID.ToString(),
                    Text = o.OrgName
                }).ToList();
                myModel.DonationTypes = db.Donation_Type.ToList();
                myModel.Lines = myDonation.Donation_Line.ToList();
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyDonationDetails(string donationID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyDonation", new { donationID = donationID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify(string DonationID, int? DonorPID, int? DonorOrgID, DateTime Date, inDonationItem[] pcrdetails)
        {
            try
            {
                string result = "information is incomplete.";
                int LineID = 1;
                decimal Sum = 0;
                if (pcrdetails != null && DonationID != null && DonorPID != 0 || DonorOrgID != 0)
                {
                    var db = new CodeFirst.CodeFirst();
                    var intDonationID = Int32.Parse(DonationID);
                    db.Configuration.LazyLoadingEnabled = true;
                    var myDonation = db.Donations.Where(a => a.DonationID == intDonationID).FirstOrDefault();

                    myDonation.DonationID = intDonationID;
                    myDonation.DateDate = Date;
                    myDonation.DonorPID = DonorPID;
                    myDonation.DonorOrgID = DonorOrgID;

                    foreach (var item in db.Donation_Line.Where(d => d.DonationID == intDonationID))
                    {
                        var forTotal = db.Donation_Item.Where(d => d.DonationItemID == item.DonationItemID).FirstOrDefault();
                        forTotal.TotalAmount -= item.DonationAmount;
                    }

                    //OrderLine
                    foreach (var item in pcrdetails)
                    {
                        var myItem = new CodeFirst.Donation_Line();
                        myItem.DonationID = (int)myDonation.DonationID;
                        myItem.LineID = LineID;
                        myItem.Description = item.Description;

                        myItem.DonationItemID = db.Donation_Item.Where(d => d.TypeID == db.Donation_Type.Where(d2 => d2.TypeName == item.ItemType).FirstOrDefault().TypeID).FirstOrDefault().DonationItemID;;
                        myItem.DonationAmount = item.DonationAmount;

                        var forTotal = db.Donation_Item.Where(di => di.DonationItemID == myItem.DonationItemID).FirstOrDefault();
                        forTotal.TotalAmount += item.DonationAmount;

                        LineID++;
                        db.Donation_Line.Add(myItem);
                    }

                    db.Donation_Line.RemoveRange(db.Donation_Line.Where(d => d.DonationID == myDonation.DonationID));

                    db.SaveChanges();
                    result = "Success! Donation updated!";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception error)
            {
                var result = "Error has occured!";

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public class inDonationItem
        {
            public string Description { get; set; }

            public string ItemType { get; set; }

            public string ItemName { get; set; }

            public int DonationAmount { get; set; }
        }
    }
}