using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonorOrgTYLetterController : Controller
    {
        // GET: DonorOrgTYLetter
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var DonorOrg = new Models.Donor_Org();
            DonorOrg.DonorOrgs = db.Donor_Org.ToList();
            return View(DonorOrg);
        }

        public ActionResult DonorOrgTYLetter(int DonorOrgID)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/DonorOrgThankYouLetter.rpt")));
            rd.SetParameterValue("@OrgID", DonorOrgID);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/doc", "DonorThankYouLetterTemplate.doc");
            }
            catch
            {
                throw;
            }
        }
    }
}