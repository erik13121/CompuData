using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonationController : Controller
    {
        // GET: Donation
        public ActionResult Index()
        {
            Models.Donation myModel = new Models.Donation();
            if (TempData["model"] != null)
            {
                myModel = (Models.Donation)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Donation.GetData();

            var db = new CodeFirst.CodeFirst();
            var DonorP = db.Donor_Person.ToList();
            var DonorO = db.Donor_Org.ToList();
            var newData = (from d in data
                           join dP in DonorP on d.DonorPID equals dP.DonorPID
                           join dO in DonorO on d.DonorOrgID equals dO.DonorOrgID
                           select new
                           {
                               DonationID = d.DonationID,
                               Date = d.DateDate,
                               DonorPName = dP.FirstName + " " + dP.SecondName,
                               DonorOrgName = dO.OrgName
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.DonationID.ToString().Contains(request.Search.Value) ||
            _item.Date.ToString().ToString().Contains(request.Search.Value) ||
            _item.DonorPName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.DonorOrgName.ToUpper().Contains(request.Search.Value.ToUpper())
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

        /*[HttpPost]
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
        }*/

        [HttpPost]
        public ActionResult Delete(string donationID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intDonationID = int.Parse(donationID);
                var Donation = db.Donations.Where(v => v.DonationID == intDonationID).FirstOrDefault();
                db.Donations.Remove(Donation);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Donation");
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