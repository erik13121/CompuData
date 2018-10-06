using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonorOrgController : Controller
    {
        // GET: DonorOrg
        public ActionResult Index()
        {
            Models.Donor_Org myModel = new Models.Donor_Org();
            if (TempData["model"] != null)
            {
                myModel = (Models.Donor_Org)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Donor_Org.GetData();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item =>
            _item.DonorOrgID.ToString().Contains(request.Search.Value) ||
            _item.ContactNum.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ContactEmail.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Thanked.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.StreetAddress != null ? _item.StreetAddress.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.City != null ? _item.City.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.AreaCode != null ? _item.AreaCode.ToUpper().Contains(request.Search.Value.ToUpper()) : false)
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
        public ActionResult Delete(string DonorOrgID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intDonorID = int.Parse(DonorOrgID);
                var DonorOrg = db.Donor_Org.Where(v => v.DonorOrgID == intDonorID).FirstOrDefault();
                db.Donor_Org.Remove(DonorOrg);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonorOrg");
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