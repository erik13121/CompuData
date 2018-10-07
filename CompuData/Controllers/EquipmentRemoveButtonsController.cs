using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentRemoveButtonsController : Controller
    {
        public static int GlobalEquipmentID;

        // GET: EquipmentRemoveButtons
        public ActionResult Index()
        {
            var equipment = new Models.Equipment();
            equipment.EquipmentID = GlobalEquipmentID;
            return View(equipment);
        }

        [HttpPost]
        public ActionResult RedirectToEquipmentsRemove(string equipmentID)
        {
            try
            {
                GlobalEquipmentID = Convert.ToInt32(equipmentID);
                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentRemoveButtons", new { equipmentID = equipmentID });
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Error = "Error" });
            }
        }

        public ActionResult proofPDF([Bind(Prefix = "")]Models.Equipment model)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/ProofofRemovalEquipment.rpt")));
            rd.SetParameterValue("@EquipmentID", GlobalEquipmentID);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "ProofofRemovableEquipment.pdf");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult proofword([Bind(Prefix = "")]Models.Equipment model)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/ProofofRemovalEquipment.rpt")));
            rd.SetParameterValue("@EquipmentID", GlobalEquipmentID);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/doc", "ProofofRemovableEquipment.doc");
            }
            catch
            {
                throw;
            }
        }

    }
}