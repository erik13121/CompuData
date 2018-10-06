using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyEFTRController : Controller
    {
        // GET: ModifyEFTR
        public ActionResult Index(string requisitionID)
        {
            Models.EFTR myModel = new Models.EFTR();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (requisitionID != null)
            {
                var intEFTRid = Int32.Parse(requisitionID);
                var myEFTR = db.EFT_Requisition.Where(i => i.RequisitionID == intEFTRid).FirstOrDefault();
                var mySupplierID = db.Suppliers.Where(i => i.SupplierID == myEFTR.SupplierID).FirstOrDefault();
                var myUserID = db.Users.Where(i => i.UserID == myEFTR.UserID).FirstOrDefault();
                var myProjectID = db.Projects.Where(i => i.ProjectID == myEFTR.ProjectID).FirstOrDefault();

                myModel.RequisitionID = myEFTR.RequisitionID;
                myModel.Date = myEFTR.Date;
                myModel.ApprovedCEO = myEFTR.ApprovedCEO;
                myModel.ApprovedProjectManger = myEFTR.ApprovedProjectManger;
                myModel.SupplierID = mySupplierID.SupplierID;
                myModel.ProjectID = myProjectID.ProjectID;
                myModel.UserID = myUserID.UserID;
                myModel.Name = db.Suppliers.Where(i => i.SupplierID == mySupplierID.SupplierID).FirstOrDefault().Name;
                myModel.ProjectName = db.Projects.Where(i => i.ProjectID == myProjectID.ProjectID).FirstOrDefault().ProjectName;
                myModel.Initials = db.Users.Where(i => i.UserID == myUserID.UserID).FirstOrDefault().Initials;
                myModel.LastName = db.Users.Where(i => i.UserID == myUserID.UserID).FirstOrDefault().LastName;
                myModel.TotalAmount = myEFTR.TotalAmount;

                myModel.Suppliers = db.Suppliers.ToList();
                myModel.Projects = db.Projects.ToList();
                myModel.Users = db.Users
                    .AsEnumerable()
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserID.ToString(),
                        Text = u.Initials + " " + u.LastName
                    }).ToList();
                myModel.Lines = myEFTR.EFT_Requisition_Line.ToList();
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyEFTRDetails(string requisitionID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyEFTR", new { requisitionID = requisitionID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify(string RequisitionID, string SupplierID, int UserID, int ProjectID, DateTime Date, Models.EFTRLine[] pcrdetails)
        {
            try
            {
                string result = "information is incomplete.";
                int LineID = 1;
                decimal Sum = 0;
                if (pcrdetails != null && SupplierID != null && UserID != 0 && ProjectID != 0)
                {
                    var db = new CodeFirst.CodeFirst();
                    var intReqID = Int32.Parse(RequisitionID);
                    var myEFTR = db.EFT_Requisition.Where(a => a.RequisitionID == intReqID).FirstOrDefault();

                    myEFTR.RequisitionID = intReqID;
                    myEFTR.Date = Date;
                    myEFTR.SupplierID = Convert.ToInt32(SupplierID);
                    myEFTR.ProjectID = ProjectID;
                    myEFTR.UserID = UserID;

                    var lines = db.EFT_Requisition_Line.Where(p => p.RequisitionID == intReqID);
                    db.EFT_Requisition_Line.RemoveRange(lines);
                    //OrderLine
                    foreach (var item in pcrdetails)
                    {
                        var myItem = new CodeFirst.EFT_Requisition_Line();
                        myItem.RequisitionID = (int)myEFTR.RequisitionID;
                        myItem.LineID = LineID;
                        myItem.Details = item.Details;
                        myItem.QuantityEFT = (int)item.QuantityEFT;
                        myItem.UnitPriceEFT = (decimal)item.UnitPriceEFT;
                        myItem.TotalEFT = decimal.Parse(item.TotalEFT.ToString().Substring(1, item.TotalEFT.ToString().Length - 1));
                        myItem.SupplierID = Convert.ToInt32(SupplierID);

                        Sum += decimal.Parse(item.TotalEFT.ToString().Substring(1, item.TotalEFT.ToString().Length - 1));
                        LineID++;
                        db.EFT_Requisition_Line.Add(myItem);
                    }

                    myEFTR.TotalAmount = Sum;

                    if (Sum > 2000)
                    {
                        db.SaveChanges();
                        result = "Success! EFT Requisition updated!";                        
                    }
                    else
                    {
                        result = "Total Amount is less than R2000. Only Petty Cash Requisitions can be under this amount.";
                    }

                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                var result = "Error has occured!";

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}