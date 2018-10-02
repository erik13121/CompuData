using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompuData.Models;
using System.IO;
using CompuData.Global;

namespace CompuData.Controllers.Reports
{
    public class ReportsMainController : Controller
    {
        ////Model Connection
        //CompuDataSQLEntities db = new CompuDataSQLEntities();

        // GET: ReportsMain
        public ActionResult Index()
        {
            return View();
        }

        ////Procurement
        //public ActionResult Procurement()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Report"), "ProcurementReport.rpt"));
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "ProcurementReport.pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ////VehicleRegister
        //public ActionResult VehicleRegister()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Report"), "VehicleRegisterReport.rpt"));
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "VehiclesRegister.pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ////Donations
        //public ActionResult Donations()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Report"), "DonationsReport.rpt"));
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "DonationsReport.pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ////EquipmentUsage
        //public ActionResult EquipmentUsageReport()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Report"), "EquipUsageRep.rpt"));
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "EquipmentUsageReport.pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ////EquipmentBookings
        //public ActionResult EquipmentBookingsReport()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Report"), "EquipmentBookings.rpt"));
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "EquipmentBookingsReport.pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ////VehicleBookings
        //public ActionResult VehicleBookingsReport()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Report"), "VehicleBooking.rpt"));
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "VehicleBookingsReport.pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        ////VenueBookings
        //public ActionResult VenueBookingsReport()
        //{
        //    ReportDocument rd = new ReportDocument();
        //    rd.Load(Path.Combine(Server.MapPath("~/Report"), "VenueBookingsReport.rpt"));
        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    try
        //    {
        //        Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //        stream.Seek(0, SeekOrigin.Begin);
        //        return File(stream, "application/pdf", "VenueBookingsReport.pdf");
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}
    }
}