using CompuData.CodeFirst;
using CompuData.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public JsonResult SavePCRALL(string SupplierID, int UserID, int ProjectID, DateTime? Date, EFTRLine[] pcrdetails)
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
                    newPCR.Date = Date.Value;
                    newPCR.SupplierID = Convert.ToInt32(SupplierID);
                    newPCR.ProjectID = ProjectID;
                    newPCR.UserID = UserID;
                }
                else
                {
                    newPCR.RequisitionID = 1;
                    newPCR.ApprovedCEO = false;
                    newPCR.ApprovedProjectManger = false;
                    newPCR.Date = Date.Value;
                    newPCR.SupplierID = Convert.ToInt32(SupplierID);
                    newPCR.ProjectID = ProjectID;
                    newPCR.UserID = UserID;
                }
                db.EFT_Requisition.Add(newPCR);

                //OrderLine
                foreach (var item in pcrdetails)
                {
                    CodeFirst.EFT_Requisition_Line tempLine = new CodeFirst.EFT_Requisition_Line();
                    tempLine.RequisitionID = (int)newPCR.RequisitionID;
                    tempLine.LineID = LineID;
                    tempLine.Details = item.Details;
                    tempLine.QuantityEFT = (int)item.QuantityEFT;
                    tempLine.UnitPriceEFT = (decimal)item.UnitPriceEFT;
                    tempLine.TotalEFT = decimal.Parse((item.TotalEFT.ToString().Substring(1, item.TotalEFT.ToString().Length - 1)), CultureInfo.InvariantCulture);
                    tempLine.SupplierID = (int)item.SupplierID;

                    Sum += decimal.Parse((item.TotalEFT.ToString().Substring(1, item.TotalEFT.ToString().Length - 1)), CultureInfo.InvariantCulture);
                    LineID++;
                    db.EFT_Requisition_Line.Add(tempLine);
                }

                newPCR.TotalAmount = Sum;

                if (Sum > 2000)
                {
                    db.SaveChanges();
                    result = "Success! Order is complete!";
                }
                else
                {
                    result = "Total Amount is less than R2000. Please Use Petty Cash Requisition for this Transaction";
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}