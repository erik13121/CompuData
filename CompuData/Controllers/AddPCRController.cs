using CompuData.CodeFirst;
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
                    var item = db.Petty_Cash_Requisition.OrderByDescending(a => a.RequisitionID).FirstOrDefault();

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

        //public JsonResult SavePCRALL(int? RequisitionID, int? SupplierID, int? UserID, int? ProjectID , PCRLine[] pcrdetails)
        //{
        //    string result = "Error!information is incomplete";
        //    if (pcrdetails != null && SupplierID != null && UserID != null && ProjectID != null && RequisitionID != null)
        //    {
        //        var db = new CodeFirst.CodeFirst();
        //        var item = db.Petty_Cash_Requisition.OrderByDescending(a => a.ProjectID).FirstOrDefault();

        //        //Populate Order table
        //        Petty_Cash_Requisition newPCR = new Petty_Cash_Requisition();

        //        newPCR.RequisitionID = item.RequisitionID + 1;
        //        newPCR.ApprovalStatus = "Not Approved";
        //        newPCR.VATInclusive = VATInclusive;
        //        newPCR.ReqDate = DateTime.Parse(ReqDate.ToString("yyyy-MM-dd"));
        //        newPCR.SupplierID = SupplierID;
        //        newPCR.ProjectID = ProjectID;
        //        newPCR.UserID = UserID;

        //        //get list of all Suppliers
        //        var supplierList = db.Suppliers.ToList();
        //        //get list of all Projects
        //        var projectLists = db.Projects.ToList();
        //        //get list of all users
        //        var userList = db.Users.AsEnumerable().Select(u => new SelectListItem
        //        {
        //            Value = u.UserID.ToString(),
        //            Text = u.Initials + " " + u.LastName
        //        }).ToList();

        //        db.Petty_Cash_Requisition.Add(newPCR);
        //        db.SaveChanges();

        //        //OrderLine
        //        foreach (var item in pcrdetails)
        //        {
        //            //populate orderLine table
        //            OrderLine O = new OrderLine();
        //            O.OrderNum = (int)newOrder.OrderNum;
        //            O.InventoryID = (int)item.InventoryID;
        //            O.OrderItemPrice = (decimal)item.OrderItemPrice;
        //            O.OrderLineTotalQuantity = (int)item.OrderLineTotalQuantity;
        //            var inventory = db.Inventories.Find(item.InventoryID);
        //            O.UnitID = (int)inventory.Unit;
        //            O.OrderLineTotalAmount = (decimal)item.OrderLineTotalAmount;
        //            db.OrderLines.Add(O);
        //        }
        //        db.SaveChanges();
        //        result = "Success! Order is complete!";
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

    }
}