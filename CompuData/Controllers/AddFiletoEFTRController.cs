using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddFiletoEFTRController : Controller
    {
        // GET: AddFiletoEFTR
        public ActionResult Index(string requisitionID)
        {
            Models.EFTR myModel = new Models.EFTR();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (requisitionID != null)
            {
                var intPCRID = Int32.Parse(requisitionID);
                var myPCR = db.EFT_Requisition.Where(i => i.RequisitionID == intPCRID).FirstOrDefault();
                var mySupplierID = db.Suppliers.Where(i => i.SupplierID == myPCR.SupplierID).FirstOrDefault();
                var myUserID = db.Users.Where(i => i.UserID == myPCR.UserID).FirstOrDefault();
                var myProjectID = db.Projects.Where(i => i.ProjectID == myPCR.ProjectID).FirstOrDefault();

                myModel.RequisitionID = myPCR.RequisitionID;
                myModel.Date = myPCR.Date;
                myModel.ApprovedCEO = myPCR.ApprovedCEO;
                myModel.ApprovedCEO = myPCR.ApprovedProjectManger;
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
                myModel.Lines = myPCR.EFT_Requisition_Line.ToList();
            }

            return View(myModel);
        }

        public ActionResult Upload([Bind(Prefix = "")]Models.EFTR model, HttpPostedFileBase file)
        {
            var db = new CodeFirst.CodeFirst();

            //array of allowed extensions
            var allowedExtensions = new[] { ".pdf" };
            //checking extension of file uploaded
            var checkExtension = Path.GetExtension(file.FileName).ToLower();
            //check if does not contain the extension (not png/jpg/jpeg)
            var myReq = db.EFT_Requisition.Where(v => v.RequisitionID == model.RequisitionID).SingleOrDefault();

            if (!allowedExtensions.Contains(checkExtension))
            {
                ViewBag.Error = "Only PDF Files are allowed.";
                return View("Index", model);
            }
            if (file.ContentLength > 8 * 1024 * 1024)
            {
                ViewBag.Error = "File too big!";
            }
            if (ViewBag.Error == null)
            {
                if (myReq != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Files"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    myReq.ReceiptFile = "~/Files/" + file.FileName;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "EFTR");
            }
            return View("Index", model);
        }
    }
}