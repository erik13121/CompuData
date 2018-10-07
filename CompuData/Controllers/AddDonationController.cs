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
    }
}