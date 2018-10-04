using CompuData.CodeFirst;
using CompuData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddEFTRController : Controller
    {
        // GET: AddEFTR
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var EFTR = new Models.EFTR();
            EFTR.Suppliers = db.Suppliers.ToList();
            EFTR.Projects = db.Projects.ToList();
            EFTR.Users = db.Users.AsEnumerable().Select(u => new SelectListItem
            {
                Value = u.UserID.ToString(),
                Text = u.Initials + " " + u.LastName
            }).ToList();
            return View(EFTR);
        }

        [HttpPost]
        public JsonResult SavePCRALL(string SupplierID, int UserID, int ProjectID, bool? VATInclusive, DateTime? Date, PCRLine[] pcrdetails)
        {
            string result = "Error!information is incomplete";
            int LineID = 1;
            decimal Sum = 0;
            if (pcrdetails != null && SupplierID != null && UserID != 0 && ProjectID != 0)
            {
                var db = new CodeFirst.CodeFirst();
                EFT_Requisition newPCR = new EFT_Requisition();
                if (db.EFT_Requisition.ToList().Count > 0)
                {
                    var waduuu = db.EFT_Requisition.OrderByDescending(a => a.RequisitionID).FirstOrDefault();

                    newPCR.RequisitionID = waduuu.RequisitionID + 1;
                    newPCR.ApprovedCEO = false;
                    newPCR.ApprovedProjectManger = false;
                    newPCR.Date = DateTime.Parse(Date.Value.ToString("yyyy-MM-dd"));
                    newPCR.SupplierID = Convert.ToInt32(SupplierID);
                    newPCR.ProjectID = ProjectID;
                    newPCR.UserID = UserID;
                }
                else
                {
                    newPCR.RequisitionID = 1;
                    newPCR.ApprovedCEO = false;
                    newPCR.ApprovedProjectManger = false;
                    newPCR.Date = DateTime.Parse(Date.Value.ToString("yyyy-MM-dd"));
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

                db.EFT_Requisition.Add(newPCR);

                //OrderLine
                foreach (var item in pcrdetails)
                {
                    CodeFirst.EFT_Requisition_Line FUckARrie = new CodeFirst.EFT_Requisition_Line();
                    FUckARrie.RequisitionID = (int)newPCR.RequisitionID;
                    FUckARrie.LineID = LineID;
                    FUckARrie.Details = item.Details;
                    FUckARrie.QuantityEFT = (int)item.Quantity;
                    FUckARrie.UnitPriceEFT = (decimal)item.UnitPrice;
                    FUckARrie.TotalEFT = decimal.Parse(item.Total.ToString().Substring(1, item.Total.ToString().Length));
                    FUckARrie.SupplierID = (int)item.SupplierID;

                    Sum += decimal.Parse(item.Total.ToString().Substring(1, item.Total.ToString().Length));
                    LineID++;
                    db.EFT_Requisition_Line.Add(FUckARrie);
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