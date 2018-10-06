using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonationItemModifyController : Controller
    {
        // GET: DonationItemModify
        public ActionResult Index(string donationItemID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (donationItemID != null)
            {
                Models.DonationItem myModel = new Models.DonationItem();

                var intDonoItemID = Int32.Parse(donationItemID);
                var myDonationItem = db.Donation_Item.Where(i => i.DonationItemID == intDonoItemID).FirstOrDefault();

                myModel.DonationItemID = myDonationItem.DonationItemID;
                myModel.Description = myDonationItem.Description;
                myModel.TotalAmount = myDonationItem.TotalAmount;
                myModel.TypeID = myDonationItem.TypeID;
                myModel.QuantityTypeID = myDonationItem.QuantityTypeID;

                myModel.DonationTypes = db.Donation_Type.ToList();
                myModel.QuantityTypes = db.Quantity_Type.ToList();
                return View(myModel);
            }

            Models.DonationItem model = new Models.DonationItem();
            model.DonationTypes = db.Donation_Type.ToList();
            model.QuantityTypes = db.Quantity_Type.ToList();
            return View(model);
        }     

        [HttpPost]
        public ActionResult RedirectToModifyDonationItemDetails(string donationItemID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonationItemModify", new { donationItemID = donationItemID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.DonationItem model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var myDonationItem = db.Donation_Item.Where(v => v.DonationItemID == model.DonationItemID).SingleOrDefault();

                if (myDonationItem != null)
                {
                    myDonationItem.DonationItemID = model.DonationItemID;
                    myDonationItem.Description = model.Description;
                    myDonationItem.TotalAmount = model.TotalAmount;
                    myDonationItem.TypeID = model.TypeID;
                    myDonationItem.QuantityTypeID = model.QuantityTypeID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "DonationItem");
            }

            model.DonationTypes = db.Donation_Type.ToList();
            model.QuantityTypes = db.Quantity_Type.ToList();
            return View("Index", model);
        }
    }
}