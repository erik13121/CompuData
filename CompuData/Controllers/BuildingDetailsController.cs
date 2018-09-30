using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class BuildingDetailsController : Controller
    {
        // GET: BuildingDetails
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
        public ActionResult RedirectToBuildingDetails(string buildingID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "BuildingDetails", new { buildingID = buildingID });
            return Json(new { Url = redirectUrl });
        }
    }
}