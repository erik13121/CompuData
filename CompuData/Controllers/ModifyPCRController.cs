using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyPCRController : Controller
    {
        // GET: ModifyPCR
        public ActionResult Index(string requisitionID)
        {
            Models.PCR myModel = new Models.PCR();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (requisitionID != null)
            {
                var intPCRID = Int32.Parse(requisitionID);
                var myPCR = db.Petty_Cash_Requisition.Where(i => i.RequisitionID == intPCRID).FirstOrDefault();
                var mySupplierID = db.Suppliers.Where(i => i.SupplierID == myPCR.SupplierID).FirstOrDefault();
                var myUserID = db.Users.Where(i => i.UserID == myPCR.UserID).FirstOrDefault();
                var myProjectID = db.Projects.Where(i => i.ProjectID == myPCR.ProjectID).FirstOrDefault();

                myModel.RequisitionID = myPCR.RequisitionID;
                myModel.ReqDate = myPCR.ReqDate.Value;
                myModel.ApprovalStatus = myPCR.ApprovalStatus;
                myModel.ReceiptFile = myPCR.ReceiptFile;
                myModel.SupplierID = mySupplierID.SupplierID;
                myModel.ProjectID = myProjectID.ProjectID;
                myModel.UserID = myUserID.UserID;
                myModel.Name = db.Suppliers.Where(i => i.SupplierID == mySupplierID.SupplierID).FirstOrDefault().Name;
                myModel.ProjectName = db.Projects.Where(i => i.ProjectID == myProjectID.ProjectID).FirstOrDefault().ProjectName;
                myModel.Initials = db.Users.Where(i => i.UserID == myUserID.UserID).FirstOrDefault().Initials;
                myModel.LastName = db.Users.Where(i => i.UserID == myUserID.UserID).FirstOrDefault().LastName;
                myModel.TotalAmount = myPCR.TotalAmount;

                myModel.Suppliers = db.Suppliers.ToList();
                myModel.Projects = db.Projects.ToList();
                myModel.Users = db.Users
                    .AsEnumerable()
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserID.ToString(),
                        Text = u.Initials + " " + u.LastName
                    }).ToList();
                myModel.Lines = myPCR.Petty_Cash_Requisition_Line.ToList();
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyPCRDetails(string requisitionID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyPCR", new { requisitionID = requisitionID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify(string RequisitionID, string SupplierID, int UserID, int ProjectID, DateTime ReqDate, Models.PCRLine[] pcrdetails)
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
                    var myPCR = db.Petty_Cash_Requisition.Where(a => a.RequisitionID == intReqID).FirstOrDefault();

                    myPCR.RequisitionID = intReqID;
                    myPCR.ReqDate = DateTime.Parse(ReqDate.ToString("yyyy-MM-dd"));
                    myPCR.SupplierID = Convert.ToInt32(SupplierID);
                    myPCR.ProjectID = ProjectID;
                    myPCR.UserID = UserID;

                    var lines = db.Petty_Cash_Requisition_Line.Where(p => p.RequisitionID == intReqID);
                    db.Petty_Cash_Requisition_Line.RemoveRange(lines);
                    //OrderLine
                    foreach (var item in pcrdetails)
                    {
                        var myItem = new CodeFirst.Petty_Cash_Requisition_Line();
                        myItem.RequisitionID = (int)myPCR.RequisitionID;
                        myItem.LineID = LineID;
                        myItem.Details = item.Details;
                        myItem.Quantity = (int)item.Quantity;
                        myItem.UnitPrice = (decimal)item.UnitPrice;
                        myItem.Total = decimal.Parse(item.Total.ToString().Substring(1, item.Total.ToString().Length - 1));
                        myItem.SupplierID = Convert.ToInt32(SupplierID);

                        Sum += decimal.Parse(item.Total.ToString().Substring(1, item.Total.ToString().Length - 1));
                        LineID++;
                        db.Petty_Cash_Requisition_Line.Add(myItem);
                    }

                    myPCR.TotalAmount = Sum;

                    if (Sum > 2000)
                    {
                        result = "Total Amount is over R2000. Only EFTs can be over this amount.";
                    }
                    else
                    {
                        db.SaveChanges();
                        result = "Success! Petty Cash Requisition updated!";
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