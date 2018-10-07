using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SelectRemovableEquipmentNewController : Controller
    {
        // GET: SelectRemovableEquipmentNew
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Equipment.GetData();


            var db = new CodeFirst.CodeFirst();
            var newData = (from e in data
                           join u in db.Users.ToList() on e.UserID equals u.UserID
                           into users
                           from mU in users.DefaultIfEmpty()
                           join t in db.Equipment_Type.ToList() on e.TypeID equals t.TypeID
                           where t.Removable == true
                           select new
                           {
                               EquipmentID = e.EquipmentID,
                               ManufacturerName = e.ManufacturerName,
                               ModelNumber = e.ModelNumber,
                               DatePurchased = e.DatePurchased.ToString("dd-MM-yyyy"),
                               ServiceIntervalMonths = e.ServiceIntervalMonths,
                               Status = e.Status,
                               UserName = mU != null ? mU.FirstName + " " + mU.LastName : "Not linked",
                               TypeName = t.TypeName,
                               Removable = t.Removable
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.EquipmentID.ToString().Contains(request.Search.Value) ||
            _item.ManufacturerName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ModelNumber.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item != null ? _item.DatePurchased.ToString().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.ServiceIntervalMonths.ToString().Contains(request.Search.Value) ||
            _item.Status.ToUpper().Contains(request.Search.Value) ||
            _item.UserName.ToUpper().Contains(request.Search.Value) ||
            _item.TypeName.ToUpper().Contains(request.Search.Value.ToUpper())
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
    }
}