using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ReportsVehiclesRegisterController : Controller
    {
        // GET: ReportsVehiclesRegister
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var VehicleTypes1 = new Models.Vehicle();
            VehicleTypes1.VehicleTypes = db.Vehicle_Type.ToList();
            return View(VehicleTypes1);
        }

        //VehicleRegister
        public ActionResult VehicleRegister(DateTime fromDate, DateTime toDate)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/VehicleRegister.rpt")));
            rd.SetParameterValue("@startdate", fromDate);
            rd.SetParameterValue("@enddate", toDate);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "VehiclesRegister.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}