using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class VehicleServiceDetailsController : Controller
    {
        // GET: VehicleServiceDetails
        public ActionResult Index(string intervalID)
        {
            Models.VehicleService myModel = new Models.VehicleService();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (intervalID != null)
            {
                var intIntervalID = Int32.Parse(intervalID);
                var myInterval = db.Services.Where(i => i.IntervalID == intIntervalID).FirstOrDefault();

                myModel.IntervalID = myInterval.IntervalID;
                myModel.ServiceDate = myInterval.ServiceDate;
                myModel.Completed = myInterval.Completed;
                myModel.VehicleID = myInterval.VehicleID;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToServiceDetails(string intervalID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "VehicleServiceDetails", new { intervalID = intervalID });
            return Json(new { Url = redirectUrl });
        }
    }
}