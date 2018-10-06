using CompuData.CodeFirst;
using CompuData.Models;
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
        public JsonResult SavePCRALL(string SupplierID, int? UserID, int? ProjectID,bool? VATInclusive , DateTime? ReqDate , PCRLine[] pcrdetails)
        {
            string result = "information is incomplete.";
            int LineID = 1;
            decimal Sum = 0;
            if (pcrdetails != null && SupplierID != null && UserID != 0 && ProjectID != 0)
            {
                var db = new CodeFirst.CodeFirst();
                Petty_Cash_Requisition newPCR = new Petty_Cash_Requisition();
                if (db.Petty_Cash_Requisition.ToList().Count > 0)
                {
                    var waduuu = db.Petty_Cash_Requisition.OrderByDescending(a => a.RequisitionID).FirstOrDefault();

                    newPCR.RequisitionID = waduuu.RequisitionID + 1;
                    newPCR.ApprovalStatus = "Not Approved";
                    newPCR.ReqDate = DateTime.Parse(ReqDate.Value.ToString("yyyy-MM-dd"));
                    newPCR.SupplierID = Convert.ToInt32(SupplierID);
                    newPCR.ProjectID = ProjectID;
                    newPCR.UserID = UserID;
                }
                else
                {
                    newPCR.RequisitionID = 1;
                    newPCR.ApprovalStatus = "Not Approved";
                    newPCR.ReqDate = DateTime.Parse(ReqDate.Value.ToString("yyyy-MM-dd"));
                    newPCR.SupplierID = Convert.ToInt32(SupplierID);
                    newPCR.ProjectID = ProjectID;
                    newPCR.UserID = UserID;
                }

                //get list of all Suppliers
                var supplierList = db.Suppliers.ToList();
                //get list of all Projects
                var projectLists = db.Projects.ToList();
                //get list of all users
                var userList = db.Users.AsEnumerable().Select(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = u.Initials + " " + u.LastName
                }).ToList();

                db.Petty_Cash_Requisition.Add(newPCR);

                //OrderLine
                foreach (var item in pcrdetails)
                {
                    CodeFirst.Petty_Cash_Requisition_Line tempLine = new CodeFirst.Petty_Cash_Requisition_Line();
                    tempLine.RequisitionID = (int)newPCR.RequisitionID;
                    tempLine.LineID = LineID;
                    tempLine.Details = item.Details;
                    tempLine.Quantity = (int)item.Quantity;
                    tempLine.UnitPrice = (decimal)item.UnitPrice;
                    tempLine.Total = decimal.Parse(item.Total.ToString().Substring(1, item.Total.ToString().Length - 1));
                    tempLine.SupplierID = (int)item.SupplierID;

                    Sum += decimal.Parse(item.Total.ToString().Substring(1, item.Total.ToString().Length - 1));
                    LineID++;
                    db.Petty_Cash_Requisition_Line.Add(tempLine);
                }

                newPCR.TotalAmount = Sum;

                if (Sum > 2000)
                {
                    result = "Total Amount is over R2000. Please Use EFT Requisition for this Transaction";
                }
                else
                {
                    db.SaveChanges();
                    result = "Success! Order is complete!";
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}