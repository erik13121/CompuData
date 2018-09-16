using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CompuData.Controllers
{
    public class ModifyVehicleScheduleController : Controller
    {
        // GET: ModifyVehicleSchedule
        public ActionResult Index(string scheduleID)
        {
            Models.VehicleSchedule myModel = new Models.VehicleSchedule();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (scheduleID != null)
            {
                var intScheduleID = Int32.Parse(scheduleID);
                var mySchedule = db.Vehicle_Schedule_Line.Where(i => i.Veh_Schedule_ID == intScheduleID).FirstOrDefault();

                myModel.Veh_Schedule_ID = mySchedule.Veh_Schedule_ID;
                myModel.Date = mySchedule.Date;
                myModel.StartTime = mySchedule.StartTime;
                myModel.EndTime = mySchedule.EndTime;
                myModel.VehicleID = mySchedule.VehicleID;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyVehicleScheduleDetails(string scheduleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyVehicleSchedule", new { scheduleID = scheduleID });
            return Json(new { Url = redirectUrl });
        }

        public string Modify(string day, string startTime, string endTime, string scheduleID, string vehicleID)
        {
            var db = new CodeFirst.CodeFirst();
            var xmlFileName = "vehicle" + vehicleID + ".xml";
            var xmlFilePath = "~/Files/" + xmlFileName;
            var absolutePath = HttpContext.Server.MapPath(xmlFilePath);
            XDocument doc = new XDocument();
            doc = XDocument.Load(absolutePath);

            var intScheduleID = int.Parse(scheduleID);
            var schedule = db.Vehicle_Schedule_Line.Where(v => v.Veh_Schedule_ID == intScheduleID).SingleOrDefault();
            var actualStart = "";
            var actualEnd = "";

            if (int.Parse(startTime) < 10)
            {
                actualStart = "0" + startTime + ":00:00";
            }
            else
            {
                actualStart = startTime + ":00:00";
            }

            if (int.Parse(endTime) < 10)
            {
                actualEnd = "0" + endTime + ":00:00";
            }
            else
            {
                actualEnd = endTime + ":00:00";
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<Date>), new XmlRootAttribute("Dates"));

            StringReader stringReader = new StringReader(doc.ToString());

            List<Date> myDates = (List<Date>)serializer.Deserialize(stringReader);

            foreach (var item in (from m in myDates
                                  where m.dayofWeek == schedule.Date &&
                                  m.startTime == schedule.StartTime.ToString("hh\\:mm\\:ss") &&
                                  m.endTime == schedule.EndTime.ToString("hh\\:mm\\:ss")
                                  select m))
                /*myDates.Where(m => m.dayofWeek == schedule.Date).Where(m => m.startTime == schedule.StartTime.ToString("hh\\:mm\\:ss")).Where(m => m.endTime == schedule.EndTime.ToString("hh\\:mm\\:ss")))*/
            {
                DayOfWeek weekDay1 = DayOfWeek.Monday;
                DayOfWeek weekDay2 = DayOfWeek.Monday;
                if (Enum.IsDefined(typeof(DayOfWeek), item.dayofWeek))
                {
                    weekDay1 = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), item.dayofWeek, true);
                }
                if (Enum.IsDefined(typeof(DayOfWeek), day))
                {
                    weekDay2 = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day, true);
                }
                var daysDiff = (7 + (weekDay2 - weekDay1)) % 7;
                
                var newDate = DateTime.ParseExact(item.date, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(daysDiff).Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                item.date = newDate;
                item.dayofWeek = day;
                item.startTime = actualStart;
                item.endTime = actualEnd;
            }

            using (FileStream fs = new FileStream(absolutePath, FileMode.Create))
            {
                serializer.Serialize(fs, myDates);
            }

            if (schedule != null)
            {
                schedule.Date = day;
                schedule.StartTime = TimeSpan.Parse(actualStart);
                schedule.EndTime = TimeSpan.Parse(actualEnd);
                db.SaveChanges();
            }

            return "myUpdateSuccess()";
        }
    }
}