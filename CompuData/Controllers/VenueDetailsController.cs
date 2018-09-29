using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class VenueDetailsController : Controller
    {
        // GET: VenueDetails
        public ActionResult Index(string venueID)
        {
            Models.Venue myModel = new Models.Venue();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (venueID != null)
            {
                var intVenueID = Int32.Parse(venueID);
                var myVenue = db.Venues.Where(i => i.VenueID == intVenueID).FirstOrDefault();
                var myBuilding = db.Buildings.Where(i => i.BuildingID == myVenue.BuildingID).FirstOrDefault();

                myModel.VenueID = myVenue.VenueID;
                myModel.Name = myVenue.Name;
                myModel.BuildingID = myVenue.BuildingID;
                myModel.BuildingName = myBuilding.Name;
            }

            myModel.Buildings = db.Buildings.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToVenueDetails(string venueID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "VenueDetails", new { venueID = venueID });
            return Json(new { Url = redirectUrl });
        }
    }
}