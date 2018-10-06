using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EFTRDetailsController : Controller
    {
        // GET: EFTRDetails
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
                myModel.Lines = myEFTR.EFT_Requisition_Line.ToList();

            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToEFTRDetails(string requisitionID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EFTRDetails", new { requisitionID = requisitionID });
            return Json(new { Url = redirectUrl });
        }
    }
}