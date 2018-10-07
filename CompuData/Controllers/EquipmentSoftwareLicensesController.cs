using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentSoftwareLicensesController : Controller
    {
        // GET: EquipmentSoftwareLicenses
        public ActionResult Index()
        {
            Models.SoftwareLicensesLine myModel = new Models.SoftwareLicensesLine();
            if (TempData["model"] != null)
            {
                myModel = (Models.SoftwareLicensesLine)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.SoftwareLicensesLine.GetData();

            var db = new CodeFirst.CodeFirst();
            var newData = (from d in data
                           join l in db.Software_Licenses.ToList() on d.LicenceID equals l.LicenceID
                           join e in db.Equipments.ToList() on d.EquipmentID equals e.EquipmentID
                           select new
                           {
                               LineID = d.LineID,
                               LicenceID = d.LicenceID,
                               EquipmentID = d.EquipmentID,
                               Activated = d.Activated,
                               LastActivatedDate = d.LastActivatedDate.Value.ToString("dd-MM-yyyy"),
                               Manufacturer = e.ManufacturerName,
                               ModelNumber = e.ModelNumber,
                               SoftwareName = l.SoftwareName
                           })
                           .ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.LicenceID.ToString().Contains(request.Search.Value) ||
            _item.EquipmentID.ToString().Contains(request.Search.Value) ||
            _item.Activated.ToString().Contains(request.Search.Value) ||
            (_item.LastActivatedDate != null ? _item.LastActivatedDate.ToString().Contains(request.Search.Value) : false) ||
            _item.Manufacturer.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ModelNumber.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.SoftwareName.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string lineID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intID = int.Parse(lineID);
                var SoftLicence = db.Software_Licenses_Line.Where(v => v.LineID == intID).FirstOrDefault();
                db.Software_Licenses_Line.Remove(SoftLicence);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentSoftwareLicenses");
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