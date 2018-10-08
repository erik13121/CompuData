using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SelectDonationController : Controller
    {
        public static int GlobalProjectID;
        // GET: SelectDonation
        public ActionResult Index(string projectID)
        {
            GlobalProjectID = Int32.Parse(projectID);
            return View();
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var db = new CodeFirst.CodeFirst();
            var data = db.Donation_Line.Where(d => d.ProjectID == null).ToList();

            var newData = (from d in data
                           join i in db.Donation_Item.ToList() on d.DonationItemID equals i.DonationItemID
                           select new
                           {
                               DonationID = d.DonationID,
                               LineID = d.LineID,
                               Description = d.Description,
                               ItemName = i.Description,
                               DonationAmount = d.DonationAmount
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.Description.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ItemName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.DonationAmount.ToString().ToUpper().Contains(request.Search.Value.ToUpper())
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

        public JsonResult Assign(Data data)
        {
            var db = new CodeFirst.CodeFirst();

            var myDonationLine = db.Donation_Line.Where(d => d.DonationID == data.donationID && d.LineID == data.lineID).FirstOrDefault();
            myDonationLine.ProjectID = GlobalProjectID;

            db.SaveChanges();
            return Json(new { Url = "/Donation" });
        }

        public class Data
        {
            public int donationID { get; set; }

            public int lineID { get; set; }

            public string description { get; set; }

            public string itemName { get; set; }

            public int donationAmount { get; set; }
        }
    }
}