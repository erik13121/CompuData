using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddPCRController : Controller
    {
        // GET: AddPCR
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var PCR = new Models.PCR();
            PCR.Suppliers = db.Suppliers.ToList();
            PCR.Projects = db.Projects.ToList();
            PCR.Users = db.Users.AsEnumerable().Select(u => new SelectListItem
            {
                Value = u.UserID.ToString(),
                Text = u.Initials + " " + u.LastName
            }).ToList();
            return View(PCR);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.PCR model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Petty_Cash_Requisition.Count() > 0)
                {
                    var item = db.Petty_Cash_Requisition.OrderByDescending(a => a.ProjectID).FirstOrDefault();

                    db.Petty_Cash_Requisition.Add(new CodeFirst.Petty_Cash_Requisition
                    {
                        RequisitionID = item.RequisitionID + 1,
                        ApprovalStatus = "Not Approved",
                        VATInclusive = model.VATInclusive,
                        ReqDate = DateTime.Parse(model.ReqDate.ToString("yyyy-MM-dd")),
                        SupplierID = model.SupplierID,
                        ProjectID = model.ProjectID,
                        UserID = model.UserID,

                    });
                }
                else
                {
                    db.Petty_Cash_Requisition.Add(new CodeFirst.Petty_Cash_Requisition
                    {
                        RequisitionID = 1,
                        ApprovalStatus = "Not Approved",
                        VATInclusive = model.VATInclusive,
                        ReqDate = DateTime.Parse(model.ReqDate.ToString("yyyy-MM-dd")),
                        SupplierID = model.SupplierID,
                        ProjectID = model.ProjectID,
                        UserID = model.UserID,
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "PCR");

            }

            model.Suppliers = db.Suppliers.ToList();
            model.Projects = db.Projects.ToList();
            model.Users = db.Users.AsEnumerable().Select(u => new SelectListItem
            {
                Value = u.UserID.ToString(),
                Text = u.Initials + " " + u.LastName
            }).ToList();
            return View("Index", model);
        }
    }
}