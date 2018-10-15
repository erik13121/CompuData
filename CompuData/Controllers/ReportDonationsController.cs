using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ReportDonationsController : Controller
    {
        // GET: ReportDonations
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DonationsReport(DateTime fromDate, DateTime toDate)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/DonationsReport1.rpt")));
            //rd.SetParameterValue("@startdate", fromDate);
            //rd.SetParameterValue("@enddate", toDate);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "DonationsReport.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}