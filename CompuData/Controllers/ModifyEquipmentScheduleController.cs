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
    public class ModifyEquipmentScheduleController : Controller
    {
        // GET: ModifyEquipmentSchedule
        public ActionResult Index(string scheduleID)
        {
            Models.EquipmentSchedule myModel = new Models.EquipmentSchedule();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (scheduleID != null)
            {
                var intScheduleID = Int32.Parse(scheduleID);
                var mySchedule = db.Equipment_Schedule_Line.Where(i => i.LineID == intScheduleID).FirstOrDefault();

                myModel.LineID = mySchedule.LineID;
                myModel.Date = mySchedule.Date;
                myModel.TimeStart = mySchedule.TimeStart;
                myModel.TimeEnd = mySchedule.TimeEnd;
                myModel.EquipmentID = mySchedule.EquipmentID;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyEquipmentScheduleDetails(string scheduleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyEquipmentSchedule", new { scheduleID = scheduleID });
            return Json(new { Url = redirectUrl });
        }

        public string Modify(string day, string startTime, string endTime, string scheduleID, string equipmentID)
        {
            var db = new CodeFirst.CodeFirst();
            var xmlFileName = "equipment" + equipmentID + ".xml";
            var xmlFilePath = "~/Files/" + xmlFileName;
            var absolutePath = HttpContext.Server.MapPath(xmlFilePath);
            XDocument doc = new XDocument();
            doc = XDocument.Load(absolutePath);

            var intScheduleID = int.Parse(scheduleID);
            var schedule = db.Equipment_Schedule_Line.Where(v => v.LineID == intScheduleID).SingleOrDefault();
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
                                  m.startTime == schedule.TimeStart.ToString("hh\\:mm\\:ss") &&
                                  m.endTime == schedule.TimeEnd.ToString("hh\\:mm\\:ss")
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

                //IS THIS NAMES CORRECT??????????????????????????????????????????????????
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
                schedule.TimeStart = TimeSpan.Parse(actualStart);
                schedule.TimeEnd = TimeSpan.Parse(actualEnd);
                db.SaveChanges();
            }

            return "myUpdateSuccess()";
        }
    }
}