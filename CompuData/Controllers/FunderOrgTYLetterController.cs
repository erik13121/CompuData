using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class FunderOrgTYLetterController : Controller
    {
        // GET: FunderOrgTYLetter
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var FunderOrg = new Models.Funder_Org();
            FunderOrg.FunderOrgs = db.Funder_Org.ToList();
            return View(FunderOrg);
        }

        public ActionResult FunderOrgTYLetter(int FunderOrgID)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/FunderThankYouLetter.rpt")));
            rd.SetParameterValue("@OrgID", FunderOrgID);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/doc", "FunderThankYouLetterTemplate.doc");
            }
            catch
            {
                throw;
            }
        }
    }
}