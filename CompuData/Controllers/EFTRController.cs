using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EFTRController : Controller
    {
        // GET: EFTR
        public ActionResult Index()
        {
            Models.EFTR myModel = new Models.EFTR();
            if (TempData["model"] != null)
            {
                myModel = (Models.EFTR)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.EFTR.GetData();

            var db = new CodeFirst.CodeFirst();
            var Users = db.Users.ToList();
            var Project = db.Projects.ToList();
            var Suppliers = db.Suppliers.ToList();
            var newData = (from d in data
                           join e in Users on d.UserID equals e.UserID
                           join a in Project on d.ProjectID equals a.ProjectID
                           join s in Suppliers on d.SupplierID equals s.SupplierID
                           select new
                           {
                               RequisitionID = d.RequisitionID,
                               ApprovalCEO = d.ApprovedCEO,
                               ApprovalPM = d.ApprovedProjectManger,
                               ReceiptFile = d.ReceiptFile,
                               Date = d.Date.ToString("dd/MM/yyyy"),
                               TotalAmount = d.TotalAmount,
                               SupplierName = s.Name,
                               ProjectName = a.ProjectName,
                               UserInitials = e.Initials,
                               UserLastName = e.LastName
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.RequisitionID.ToString().Contains(request.Search.Value) ||
            _item.ApprovalPM.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ApprovalCEO.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Date.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.TotalAmount.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.SupplierName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ProjectName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.UserInitials != null ? _item.UserInitials.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.UserLastName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.TotalAmount.ToString().Contains(request.Search.Value)
            );

            // Paging filtered data.
            // Paging is rather manual due to in-memmory (IEnumerable) data.
            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            // Response creation. To create your response you need to reference your request, to avoid
            // request/response tampering and to ensure response will be correctly created.
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Approve(string requisitionID)
        {
            var db = new CodeFirst.CodeFirst();
            var intRequisitionID = int.Parse(requisitionID);
            var requisition = db.EFT_Requisition.Where(r => r.RequisitionID == intRequisitionID).FirstOrDefault();
            if (requisition.ApprovedProjectManger == false)
            {
                requisition.ApprovedProjectManger = true;
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EFTR");
                return Json(new { Url = redirectUrl, Result = "PM" });
            }
            else
            {
                requisition.ApprovedCEO = true;
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EFTR");
                return Json(new { Url = redirectUrl, Result = "CEO" });
            }
        }

        [HttpPost]
        public ActionResult Delete(string requisitionID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intReqID = int.Parse(requisitionID);
                var EFTR = db.EFT_Requisition.Where(v => v.RequisitionID == intReqID).FirstOrDefault();
                db.EFT_Requisition.Remove(EFTR);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EFTR");
                return Json(new { Url = redirectUrl });
            }
            catch (Exception error)
            {
                return Json(new { Url = "Cascading error!" });
            }
        }

        [HttpPost]
        public void SetTempData()
        {
            TempData["js"] = "";
        }
    }
}