using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddVenueController : Controller
    {
        // GET: AddVenue
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var venues = new Models.Venue();
            venues.Buildings = db.Buildings.ToList();
            return View(venues);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Venue model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Venues.Count() > 0)
                {
                    var item = db.Venues.OrderByDescending(a => a.VenueID).FirstOrDefault();

                    db.Venues.Add(new CodeFirst.Venue
                    {
                        VenueID = item.VenueID + 1,
                        Name = model.Name,
                        BuildingID = model.BuildingID
                    });
                }
                else
                {
                    db.Venues.Add(new CodeFirst.Venue
                    {
                        VenueID = 1,
                        Name = model.Name,
                        BuildingID = model.BuildingID
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "Venues");
            }

            model.Buildings = db.Buildings.ToList();
            return View("Index", model);
        }
    }
}