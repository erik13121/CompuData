using CompuData.Models;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonationItemDetailsController : Controller
    {
        // GET: DonationItemDetails
        public ActionResult Index(string donationItemID)
        {
            Models.DonationItem myModel = new Models.DonationItem();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (donationItemID != null)
            {
                var intDonoItemID = Int32.Parse(donationItemID);
                var myDonationItem = db.Donation_Item.Where(i => i.DonationItemID == intDonoItemID).FirstOrDefault();
                var myDonationType = db.Donation_Type.Where(i => i.TypeID == myDonationItem.TypeID).FirstOrDefault();
                var myQuantityType = db.Quantity_Type.Where(i => i.QuantityTypeID == myDonationItem.QuantityTypeID).FirstOrDefault();

                myModel.DonationItemID = myDonationItem.DonationItemID;
                myModel.Description = myDonationItem.Description;
                myModel.TotalAmount = myDonationItem.TotalAmount;
                myModel.TypeID = myDonationType.TypeID;
                myModel.QuantityTypeID = myQuantityType.QuantityTypeID;
                myModel.TypeName = db.Donation_Type.Where(i => i.TypeID == myDonationType.TypeID).FirstOrDefault().TypeName;
                myModel.QuantityDescription = db.Quantity_Type.Where(i => i.QuantityTypeID == myQuantityType.QuantityTypeID).FirstOrDefault().Description;
            }

            myModel.DonationTypes = db.Donation_Type.ToList();
            myModel.QuantityTypes = db.Quantity_Type.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToDonationItemDetails(string donationItemID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonationItemDetails", new { donationItemID = donationItemID });
            return Json(new { Url = redirectUrl });
        }
    }
}