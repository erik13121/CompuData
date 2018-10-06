using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddDonationItemController : Controller
    {
        // GET: AddDonationItem
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var donationItem = new Models.DonationItem();
            donationItem.DonationTypes = db.Donation_Type.ToList();
            donationItem.QuantityTypes = db.Quantity_Type.ToList();
            return View(donationItem);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.DonationItem model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Donation_Item.Count() > 0)
                {
                    var item = db.Donation_Item.OrderByDescending(a => a.DonationItemID).FirstOrDefault();

                    db.Donation_Item.Add(new CodeFirst.Donation_Item
                    {
                        DonationItemID = item.DonationItemID + 1,
                        Description = model.Description,
                        TotalAmount = model.TotalAmount,
                        TypeID = model.TypeID,
                        QuantityTypeID = model.QuantityTypeID,
                    });
                }
                else
                {
                    db.Donation_Item.Add(new CodeFirst.Donation_Item
                    {
                        DonationItemID = 1,
                        Description = model.Description,
                        TotalAmount = model.TotalAmount,
                        TypeID = model.TypeID,
                        QuantityTypeID = model.QuantityTypeID,
                    });
                }
                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "DonationItem");

                //if (Request.Form["Referrer"] == "AddDonation")
                //{
                //    //TempData["EquipmentModel"] = equipmentModelToPassBack;
                //    return RedirectToAction("Index", "AddDonation");
                //}
                //else
                //{
                //    model.JavaScriptToRun = "mySuccess()";
                //    TempData["model"] = model;
                //    return RedirectToAction("Index", "DonationItem");
                //}
            }

            model.DonationTypes = db.Donation_Type.ToList();
            model.QuantityTypes = db.Quantity_Type.ToList();
            return View("Index", model);
        }
    }
}