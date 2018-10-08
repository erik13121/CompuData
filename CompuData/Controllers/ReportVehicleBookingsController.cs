﻿using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ReportVehicleBookingsController : Controller
    {
        // GET: ReportVehicleBookings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VehicleBookings(DateTime fromDate, DateTime toDate)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/VehicleBookingsReport.rpt")));
            rd.SetParameterValue("@startdate", fromDate);
            rd.SetParameterValue("@enddate", toDate);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "VehicleBookingsReport.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}