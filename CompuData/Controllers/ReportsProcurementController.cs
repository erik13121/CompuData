using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ReportsProcurementController : Controller
    {
        // GET: ReportsProcurement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProcurementReport(DateTime fromDate, DateTime toDate)
        {

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/ProcurementReport.rpt")));
            rd.SetParameterValue("@startdate", fromDate);
            rd.SetParameterValue("@enddate", toDate);
            rd.SetParameterValue("@startdatePCR", fromDate);
            rd.SetParameterValue("@enddatePCR", toDate);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "ProcurementReport.pdf");
            }
            catch(Exception e)
            {
                throw(e);
            }
        }
    }
}