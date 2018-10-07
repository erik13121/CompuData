using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddDonationController : Controller
    {
        // GET: AddDonation
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var model = new Models.Donation();

            model.DonorPeople = db.Donor_Person.AsEnumerable().Select(d => new SelectListItem
            {
                Value = d.DonorPID.ToString(),
                Text = d.FirstName + " " + d.SecondName
            }).ToList();

            model.DonorOrgs = db.Donor_Org.AsEnumerable().Select(o => new SelectListItem
            {
                Value = o.DonorOrgID.ToString(),
                Text = o.OrgName
            }).ToList();

            model.DonationTypes = db.Donation_Type.ToList();

            return View(model);
        }

        [HttpPost]
        public JsonResult SavePCRALL(DateTime Date, int? DonorPID, int? DonorOrgID, inDonationItem[] pcrdetails)
        {
            try
            {
                string result = "Error!information is incomplete";
                int LineID = 1;
                if (pcrdetails != null && DonorPID != 0 || DonorOrgID != 0)
                {
                    var db = new CodeFirst.CodeFirst();
                    db.Configuration.LazyLoadingEnabled = false;
                    CodeFirst.Donation newDonation = new CodeFirst.Donation();
                    if (db.Donations.ToList().Count > 0)
                    {
                        var waduuu = db.Donations.OrderByDescending(a => a.DonationID).FirstOrDefault();

                        newDonation.DonationID = waduuu.DonationID + 1;
                        newDonation.DateDate = Date;
                        newDonation.DonorPID = DonorPID;
                        newDonation.DonorOrgID = DonorOrgID;
                    }
                    else
                    {
                        newDonation.DonationID = 1;
                        newDonation.DateDate = Date;
                        newDonation.DonorPID = DonorPID;
                        newDonation.DonorOrgID = DonorOrgID;
                    }

                    //OrderLine
                    foreach (var item in pcrdetails)
                    {
                        CodeFirst.Donation_Line tempLine = new CodeFirst.Donation_Line();
                        tempLine.DonationID = newDonation.DonationID;
                        tempLine.Description = item.Description;
                        tempLine.LineID = LineID;

                        var typeID = db.Donation_Type.Where(dt => dt.TypeName == item.ItemType).FirstOrDefault().TypeID;
                        var itemID = db.Donation_Item.Where(di => di.TypeID == typeID && di.Description == item.ItemName).FirstOrDefault().DonationItemID;

                        tempLine.DonationItemID = itemID;

                        var donationAmount = item.DonationAmount;
                        tempLine.DonationAmount = donationAmount;

                        var toAddTotal = db.Donation_Item.Where(di => di.DonationItemID == itemID).FirstOrDefault();
                        toAddTotal.TotalAmount += donationAmount;

                        LineID++;
                        newDonation.Donation_Line.Add(tempLine);
                    }
                    db.Donations.Add(newDonation);

                    db.SaveChanges();
                    result = "Success! Order is complete!";
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Error)
            {
                return Json(Error.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetItems(short? typeID)
        {
            if (!typeID.HasValue)
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }

            var db = new CodeFirst.CodeFirst();
            var data = db.Donation_Item.Where(i => i.TypeID == typeID).Select(i => new { Text = i.Description, Value = i.DonationItemID });

            return Json(data, JsonRequestBehavior.AllowGet);
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