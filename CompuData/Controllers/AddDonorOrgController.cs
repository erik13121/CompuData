using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddDonorOrgController : Controller
    {
        // GET: AddDonorOrg
        public ActionResult Index()
        {  
            var db = new CodeFirst.CodeFirst();
            var DonorOrg = new Models.Donor_Org();
            return View(DonorOrg);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Donor_Org model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Donor_Org.Count() > 0)
                {
                    var item = db.Donor_Org.OrderByDescending(a => a.DonorOrgID).FirstOrDefault();

                    db.Donor_Org.Add(new CodeFirst.Donor_Org
                    {
                        DonorOrgID = item.DonorOrgID + 1,
                        OrgName = model.OrgName,
                        ContactNum = model.ContactNum,
                        ContactEmail = model.ContactEmail,
                        Thanked = model.Thanked,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,                        
                    });
                }
                else
                {
                    db.Donor_Org.Add(new CodeFirst.Donor_Org
                    {
                        DonorOrgID = 1,
                        OrgName = model.OrgName,
                        ContactNum = model.ContactNum,
                        ContactEmail = model.ContactEmail,
                        Thanked = model.Thanked,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "DonorOrg");
            }

            return View("Index", model);
        }
    }
}