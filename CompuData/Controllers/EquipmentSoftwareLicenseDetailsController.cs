using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentSoftwareLicenseDetailsController : Controller
    {
        // GET: EquipmentSoftwareLicenseDetails
        public ActionResult Index(string lineID)
        {
            Models.SoftwareLicensesLine myModel = new Models.SoftwareLicensesLine();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (lineID != null)
            {
                var intLineID = Int32.Parse(lineID);
                var myLine = db.Software_Licenses_Line.Where(i => i.LineID == intLineID).FirstOrDefault();
                var myEquipment = db.Equipments.Where(i => i.EquipmentID == myLine.EquipmentID).FirstOrDefault();
                var mySoftware = db.Software_Licenses.Where(i => i.LicenceID == myLine.LicenceID).FirstOrDefault();


                myModel.LineID = myLine.LineID;
                myModel.EquipmentID = myLine.EquipmentID;
                myModel.LicenceID = myLine.LicenceID;
                myModel.LastActivatedDate = myLine.LastActivatedDate;
                myModel.Activated = myLine.Activated;
                myModel.Manufacturer = myEquipment.ManufacturerName;
                myModel.ModelNumber = myEquipment.ModelNumber;
                myModel.SoftwareName = mySoftware.SoftwareName;
            }
            
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToEquipmentSoftwareLicenseDetails(string lineID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentSoftwareLicenseDetails", new { lineID = lineID });
            return Json(new { Url = redirectUrl });
        }
    }
}