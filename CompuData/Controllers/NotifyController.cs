using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class NotifyController : Controller
    {
        // GET: Notify
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotifyDonorTY()
        {
            var db = new CodeFirst.CodeFirst();
            var orgs = new Dictionary<string, string>();
            var people = new Dictionary<string, string>();
            var checkDate = DateTime.Today.AddMonths(-2);

            var donations = db.Donations.Where(d => d.DateDate < checkDate);

            if (donations.ToList().Count > 0)
            {
                foreach (var item in donations)
                {
                    if (item.DonorOrgID != null && item.Donor_Org.Thanked == false)
                    {
                        orgs.Add(item.Donor_Org.OrgName, item.Donor_Org.ContactEmail);
                    }
                    if (item.DonorPID != null && item.Donor_Person.Thanked == false)
                    {
                        people.Add(item.Donor_Person.FirstName + " " + item.Donor_Person.SecondName, item.Donor_Person.PersonalEmail);
                    }
                }
                if (orgs.Count > 0 || people.Count > 0)
                {
                    SendEmail("Donors need thanking", "The following Donors need thanking: <br>", orgs, people);
                }
            }

            return new EmptyResult();
        }

        public void SendEmail(string subject, string body, Dictionary<string, string> orgs, Dictionary<string, string> people)
        {
            // Email stuff
            //string subject = "Reset your password for CompuData";
            //string body = "Your Reset Password link: " + resetLink;
            string from = "donotreply@CompuData.com";

            if (orgs.Count > 0)
            {
                body += "<br><u style='font-weight: 600'>Donor Organisations</u><br>";
                foreach (KeyValuePair<string, string> item in orgs)
                {
                    body += String.Format("{0} : {1}<br>", item.Key, item.Value);
                }
            }

            if (people.Count > 0)
            {
                body += "<br><u style='font-weight: 600'>Donor People</u><br>";
                foreach (KeyValuePair<string, string> item in people)
                {
                    body += String.Format("{0} : {1}<br>", item.Key, item.Value);
                }
            }

            MailMessage message = new MailMessage(from, "erik13121@yahoo.com");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;

            // Attempt to send the email
            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Issue sending email: " + e.Message);
            }
        }

        public ActionResult NotifyFunderTY()
        {
            var db = new CodeFirst.CodeFirst();
            var orgs = new Dictionary<string, string>();
            var people = new Dictionary<string, string>();

            var funderOrgs = db.Funder_Org.Where(d => d.Thanked == false);

            if (funderOrgs.ToList().Count > 0)
            {
                foreach (var item in funderOrgs)
                {
                    orgs.Add(item.OrgName, item.EmailAddress);
                }
            }

            var funderPeople = db.Funder_Person.Where(d => d.Thanked == false);

            if (funderPeople.ToList().Count > 0)
            {
                foreach (var item in funderPeople)
                {
                    people.Add(item.FirstName + " " + item.LastName, item.PersonalEmail);
                }
            }

            if (orgs.Count > 0 || people.Count > 0)
            {
                SendFunderEmail("Funders need thanking", "The following Funders need thanking: <br>", orgs, people);
            }

            return new EmptyResult();
        }

        public void SendFunderEmail(string subject, string body, Dictionary<string, string> orgs, Dictionary<string, string> people)
        {
            // Email stuff
            //string subject = "Reset your password for CompuData";
            //string body = "Your Reset Password link: " + resetLink;
            string from = "donotreply@CompuData.com";

            if (orgs.Count > 0)
            {
                body += "<br><u style='font-weight: 600'>Funder Organisations</u><br>";
                foreach (KeyValuePair<string, string> item in orgs)
                {
                    body += String.Format("{0} : {1}<br>", item.Key, item.Value);
                }
            }

            if (people.Count > 0)
            {
                body += "<br><u style='font-weight: 600'>Funder People</u><br>";
                foreach (KeyValuePair<string, string> item in people)
                {
                    body += String.Format("{0} : {1}<br>", item.Key, item.Value);
                }
            }

            MailMessage message = new MailMessage(from, "erik13121@yahoo.com");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;

            // Attempt to send the email
            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Issue sending email: " + e.Message);
            }
        }

        public ActionResult NotifyVehicleServiceInterval()
        {
            var db = new CodeFirst.CodeFirst();
            var vehicles = new Dictionary<string, string>();

            var services = db.Services.Where(d => (d.ServiceDate.AddMonths(d.Vehicle.ServiceIntervalInMonths.Value) - d.ServiceDate).Days < 7);

            /*if (funderOrgs.ToList().Count > 0)
            {
                foreach (var item in funderOrgs)
                {
                    orgs.Add(item.OrgName, item.EmailAddress);
                }
            }

            var funderPeople = db.Funder_Person.Where(d => d.Thanked == false);

            if (funderPeople.ToList().Count > 0)
            {
                foreach (var item in funderPeople)
                {
                    people.Add(item.FirstName + " " + item.LastName, item.PersonalEmail);
                }
            }

            if (orgs.Count > 0 || people.Count > 0)
            {
                SendFunderEmail("Funders need thanking", "The following Funders need thanking: <br>", orgs, people);
            }*/

            return new EmptyResult();
        }

        /*public void SendFunderEmail(string subject, string body, Dictionary<string, string> orgs, Dictionary<string, string> people)
        {
            // Email stuff
            //string subject = "Reset your password for CompuData";
            //string body = "Your Reset Password link: " + resetLink;
            string from = "donotreply@CompuData.com";

            if (orgs.Count > 0)
            {
                body += "<br><u style='font-weight: 600'>Funder Organisations</u><br>";
                foreach (KeyValuePair<string, string> item in orgs)
                {
                    body += String.Format("{0} : {1}<br>", item.Key, item.Value);
                }
            }

            if (people.Count > 0)
            {
                body += "<br><u style='font-weight: 600'>Funder People</u><br>";
                foreach (KeyValuePair<string, string> item in people)
                {
                    body += String.Format("{0} : {1}<br>", item.Key, item.Value);
                }
            }

            MailMessage message = new MailMessage(from, "erik13121@yahoo.com");
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;

            // Attempt to send the email
            try
            {
                client.Send(message);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Issue sending email: " + e.Message);
            }
        }*/
    }
}