using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class PCRDetailsController : Controller
    {
        // GET: PCRDetails
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
                myModel.ReqDate = DateTime.Parse(myPCR.ReqDate.Value.ToString("yyyy-MM-dd"));
                myModel.ApprovalStatus = myPCR.ApprovalStatus;
                myModel.SupplierID = mySupplierID.SupplierID;
                myModel.ProjectID = myProjectID.ProjectID;
                myModel.UserID = myUserID.UserID;
                myModel.Name = db.Suppliers.Where(i => i.SupplierID == mySupplierID.SupplierID).FirstOrDefault().Name;
                myModel.ProjectName = db.Projects.Where(i => i.ProjectID == myProjectID.ProjectID).FirstOrDefault().ProjectName;
                myModel.Initials = db.Users.Where(i => i.UserID == myUserID.UserID).FirstOrDefault().Initials;
                myModel.LastName = db.Users.Where(i => i.UserID == myUserID.UserID).FirstOrDefault().LastName;

            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToPCRDetails(string requisitionID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "PCRDetails", new { requisitionID = requisitionID });
            return Json(new { Url = redirectUrl });
        }
    }
}