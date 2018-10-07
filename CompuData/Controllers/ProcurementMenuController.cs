using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ProcurementMenuController : Controller
    {
        // GET: ProcurementMenu
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewPurchaseHistory()
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/ViewPurchaseHistory.rpt")));
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "PurchaseHistory.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}