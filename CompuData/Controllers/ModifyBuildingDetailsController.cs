using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyBuildingDetailsController : Controller
    {
        // GET: ModifyBuildingDetails
        public ActionResult Index(string buildingID)
        {
            Models.Building myModel = new Models.Building();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (buildingID != null)
            {
                var intBuildingID = Int32.Parse(buildingID);
                var myBuilding = db.Buildings.Where(i => i.BuildingID == intBuildingID).FirstOrDefault();

                myModel.BuildingID = myBuilding.BuildingID;
                myModel.Name = myBuilding.Name;
                myModel.StreetAddress = myBuilding.StreetAddress;
                myModel.City = myBuilding.City;
                myModel.AreaCode = myBuilding.AreaCode;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult Update([Bind(Prefix = "")]Models.Building model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                var building = db.Buildings.Where(v => v.BuildingID == model.BuildingID).SingleOrDefault();

                if (building != null)
                {
                    building.BuildingID = model.BuildingID;
                    building.Name = model.Name;
                    building.StreetAddress = model.StreetAddress;
                    building.City = model.City;
                    building.AreaCode = model.AreaCode;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "Buildings");
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyBuildingDetails(string buildingID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyBuildingDetails", new { buildingID = buildingID });
            return Json(new { Url = redirectUrl });
        }
    }
}