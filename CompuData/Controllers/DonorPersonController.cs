using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonorPersonController : Controller
    {
        // GET: DonorPerson
        public ActionResult Index()
        {
            Models.Donor_Person myModel = new Models.Donor_Person();
            if (TempData["model"] != null)
            {
                myModel = (Models.Donor_Person)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Donor_Person.GetData();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item =>
            _item.DonorPID.ToString().Contains(request.Search.Value) ||
            _item.FirstName.ToString().Contains(request.Search.Value) ||
            _item.MiddleName.ToString().Contains(request.Search.Value) ||
            _item.SecondName.ToString().Contains(request.Search.Value) ||
            _item.Initials.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.CellNum.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.PersonalEmail.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Thanked.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.StreetAddress.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.City.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.AreaCode.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string DonorPID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intDonorID = int.Parse(DonorPID);
                var DonorPerson = db.Donor_Person.Where(v => v.DonorPID == intDonorID).FirstOrDefault();
                db.Donor_Person.Remove(DonorPerson);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonorPerson");
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