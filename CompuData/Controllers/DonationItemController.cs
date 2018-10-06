using CompuData.Models;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonationItemController : Controller
    {
        // GET: DonationItem
        public ActionResult Index()
        {
            DonationItem myModel = new DonationItem();
            if (TempData["model"] != null)
            {
                myModel = (DonationItem)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.DonationItem.GetData();

            var db = new CodeFirst.CodeFirst();
            var DonationTypes = db.Donation_Type.ToList();
            var QuantityTypes = db.Quantity_Type.ToList();
            var newData = (from d in data
                           join e in DonationTypes on d.TypeID equals e.TypeID
                           join q in QuantityTypes on d.QuantityTypeID equals q.QuantityTypeID
                           select new
                           {
                               DonationItemID = d.DonationItemID,
                               Description = d.Description,
                               TotalAmount = d.TotalAmount,
                               TypeName = e.TypeName,
                               QuantityDescription = q.Description,
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.DonationItemID.ToString().Contains(request.Search.Value) ||
            _item.Description.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.TotalAmount.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.TypeName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.QuantityDescription.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string donationItemID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intDonoItemID = int.Parse(donationItemID);
                var DonationItem = db.Donation_Item.Where(v => v.DonationItemID == intDonoItemID).FirstOrDefault();
                db.Donation_Item.Remove(DonationItem);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonationItem");
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