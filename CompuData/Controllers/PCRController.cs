using CompuData.CodeFirst;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class PCRController : Controller
    {
        // GET: PCR
        public ActionResult Index()
        {
            Models.PCR myModel = new Models.PCR();
            if (TempData["model"] != null)
            {
                myModel = (Models.PCR)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.PCR.GetData();

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
                               RequisitionID = d.RequisitionID ,
                               ApprovalStatus = d.ApprovalStatus,
                               VATInclusive = d.VATInclusive,
                               ReqDate = d.ReqDate,
                               TotalAmount = d.TotalAmount,
                               SupplierName = s.Name,
                               ProjectName = a.ProjectName,
                               UserInitials = e.Initials,
                               UserLastName = e.LastName,

                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.RequisitionID.ToString().Contains(request.Search.Value) ||
            _item.ApprovalStatus.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.ReqDate != null ? _item.ReqDate.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.TotalAmount.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.SupplierName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ProjectName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.UserInitials.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.UserLastName.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string ReqID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intReqID = int.Parse(ReqID);
                var PCR = db.Petty_Cash_Requisition.Where(v => v.RequisitionID == intReqID).FirstOrDefault();
                db.Petty_Cash_Requisition.Remove(PCR);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "PCR");
                return Json(new { Url = redirectUrl });
            }
            catch
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