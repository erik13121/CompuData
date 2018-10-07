using CompuData.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class PCRDetailsController : Controller
    {
        Models.PCR GlobalModel = new Models.PCR();
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
                myModel.Lines = myPCR.Petty_Cash_Requisition_Line.ToList();

            }
            GlobalModel = myModel;
            return View(myModel);
        }      

        [HttpPost]
        public ActionResult RedirectToPCRDetails(string requisitionID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "PCRDetails", new { requisitionID = requisitionID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult RedirectToUploadFile(string requisitionID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AddFiletoPCR", new { requisitionID = requisitionID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Download(PCR File)
        {
                string inFile = File.ReceiptFile;
                string myFile = inFile.Remove(0, 1);

                string path = AppDomain.CurrentDomain.BaseDirectory;

                byte[] fileBytes = System.IO.File.ReadAllBytes(path + myFile);
                var response = new FileContentResult(fileBytes, "application/octet-stream");
                response.FileDownloadName = "Receipt.pdf";
                return response;
        }
    }
}