using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyVenueController : Controller
    {
        // GET: ModifyVenue
        public ActionResult Index(string venueID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.Venue myModel = new Models.Venue();
            if (venueID != null)
            {
                var intVenueID = Int32.Parse(venueID);
                var myVenue = db.Venues.Where(i => i.VenueID == intVenueID).FirstOrDefault();

                myModel.VenueID = myVenue.VenueID;
                myModel.Name = myVenue.Name;
                myModel.BuildingID = myVenue.BuildingID;

                myModel.Buildings = db.Buildings.ToList();
                return View(myModel);
            }

            myModel.Buildings = db.Buildings.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyVenueDetails(string venueID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyVenue", new { venueID = venueID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Venue model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var venue = db.Venues.Where(v => v.VenueID == model.VenueID).SingleOrDefault();

                if (venue != null)
                {
                    venue.Name = model.Name;
                    venue.BuildingID = model.BuildingID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "Venues");
            }
            model.Buildings = db.Buildings.ToList();
            return View("Index", model);
        }
    }
}