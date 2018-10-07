using CompuData.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CompuData.Controllers
{
    public class AddVenueScheduleController : Controller
    {
        // GET: AddVenueSchedule
        public ActionResult Index(string venueID)
        {
            if (venueID != null)
            {
                VenueSchedule mySchedule = new VenueSchedule();
                mySchedule.VenueID = int.Parse(venueID);

                return View(mySchedule);
            }

            return View();
        }

        public string AddDate(string day, int startTime, int endTime, int venueID)
        {
            var db = new CodeFirst.CodeFirst();
            var model = new Models.VenueSchedule();
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            var currentYear = currentDate.Year;
            var currentMonth = currentDate.Month;
            var loopYear = currentYear;
            var loopMonth = currentMonth;
            var dates = new List<DateTime>();

            var xmlFileName = "venue" + venueID.ToString() + ".xml";
            var xmlFilePath = "~/Files/" + xmlFileName;
            var absolutePath = HttpContext.Server.MapPath(xmlFilePath);
            XDocument doc = new XDocument();

            string actualStartTime = "";
            string actualEndTime = "";
            if (startTime < 10)
            {
                actualStartTime = "0" + startTime + ":00:00";
            }
            else
            {
                actualStartTime = startTime + ":00:00";
            }
            if (endTime < 10)
            {
                actualEndTime = "0" + endTime + ":00:00";
            }
            else
            {
                actualEndTime = endTime + ":00:00";
            }

            if (System.IO.File.Exists(absolutePath))
            {
                doc = XDocument.Load(absolutePath);
                var dayExists = doc.Element("Dates").Elements("Date").Where(d => d.Element("dayofWeek").Value == day).FirstOrDefault();

                if (dayExists != null)
                {
                    dates = GetAllDaysForDayOfWeek(day, startTime, endTime);
                    dates.Sort();

                    var currentDates = (from date in doc.Root.Elements("Date")
                                        where date.Element("dayofWeek").Value == day
                                        select date).ToList();

                    int countofStartTimes = 0;

                    foreach (var item in currentDates)
                    {
                        TimeSpan startTimeXml = TimeSpan.ParseExact(Convert.ToString(item.Element("startTime").Value), "g",
                                        CultureInfo.InvariantCulture);
                        TimeSpan endTimeXml = TimeSpan.ParseExact(Convert.ToString(item.Element("endTime").Value), "g",
                                        CultureInfo.InvariantCulture);
                        int convertedStartTime = startTimeXml.Hours;
                        int convertedEndTime = endTimeXml.Hours;

                        if (item.Element("dayofWeek").Value == day)
                        {
                            var currentDateStartTimes = (from date in currentDates
                                                         where date.Element("date").Value == item.Element("date").Value
                                                         select date).ToList();
                            countofStartTimes = currentDateStartTimes.Count;

                            if (countofStartTimes < 2)
                            {
                                if (endTime < convertedStartTime)
                                {
                                    if (db.Venue_Schedule_Line.ToList().Count() > 0)
                                    {
                                        var newID = db.Venue_Schedule_Line.OrderByDescending(m => m.ScheduleID).FirstOrDefault().ScheduleID + 1;

                                        db.Venue_Schedule_Line.Add(new CodeFirst.Venue_Schedule_Line
                                        {
                                            ScheduleID = newID,
                                            DateAvailable = day,
                                            StartTime = TimeSpan.Parse(actualStartTime),
                                            EndTime = TimeSpan.Parse(actualEndTime),
                                            Status = "Active",
                                            VenueID = venueID,
                                        });
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        db.Venue_Schedule_Line.Add(new CodeFirst.Venue_Schedule_Line
                                        {
                                            ScheduleID = 1,
                                            DateAvailable = day,
                                            StartTime = TimeSpan.Parse(actualStartTime),
                                            EndTime = TimeSpan.Parse(actualEndTime),
                                            Status = "Active",
                                            VenueID = venueID,
                                        });
                                        db.SaveChanges();
                                    }
                                    model.JavaScriptToRun = "mySuccess()";

                                    dates = GetAllDaysForDayOfWeek(day, startTime, endTime);
                                    dates.Sort();

                                    var startDates = dates.ToList().Where((c, i) => i % 2 == 0);
                                    var endDates = dates.ToList().Where((c, i) => i % 2 != 0);

                                    XElement date = doc.Element("Dates");
                                    date.Add(
                                        from start in startDates
                                        join end in endDates on start.Date equals end.Date
                                        select new XElement("Date",
                                        new XElement("dayofWeek", day),
                                        new XElement("date", start.ToString("dd/MM/yyyy")),
                                        new XElement("startTime", start.TimeOfDay.ToString()),
                                        new XElement("endTime", end.TimeOfDay.ToString()))
                                        );
                                    doc.Save(absolutePath);
                                    break;
                                }

                                if (startTime > convertedEndTime)
                                {
                                    if (db.Venue_Schedule_Line.ToList().Count() > 0)
                                    {
                                        var newID = db.Venue_Schedule_Line.OrderByDescending(m => m.ScheduleID).FirstOrDefault().ScheduleID + 1;

                                        db.Venue_Schedule_Line.Add(new CodeFirst.Venue_Schedule_Line
                                        {
                                            ScheduleID = newID,
                                            DateAvailable = day,
                                            StartTime = TimeSpan.Parse(actualStartTime),
                                            EndTime = TimeSpan.Parse(actualEndTime),
                                            Status = "Active",
                                            VenueID = venueID,
                                        });
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                        db.Venue_Schedule_Line.Add(new CodeFirst.Venue_Schedule_Line
                                        {
                                            ScheduleID = 1,
                                            DateAvailable = day,
                                            StartTime = TimeSpan.Parse(actualStartTime),
                                            EndTime = TimeSpan.Parse(actualEndTime),
                                            Status = "Active",
                                            VenueID = venueID,
                                        });
                                        db.SaveChanges();
                                    }
                                    model.JavaScriptToRun = "mySuccess()";

                                    dates = GetAllDaysForDayOfWeek(day, startTime, endTime);
                                    dates.Sort();

                                    var startDates = dates.ToList().Where((c, i) => i % 2 == 0);
                                    var endDates = dates.ToList().Where((c, i) => i % 2 != 0);

                                    XElement date = doc.Element("Dates");
                                    date.Add(
                                        from start in startDates
                                        join end in endDates on start.Date equals end.Date
                                        select new XElement("Date",
                                        new XElement("dayofWeek", day),
                                        new XElement("date", start.ToString("dd/MM/yyyy")),
                                        new XElement("startTime", start.TimeOfDay.ToString()),
                                        new XElement("endTime", end.TimeOfDay.ToString()))
                                        );
                                    doc.Save(absolutePath);
                                    break;
                                }

                                if (startTime >= convertedStartTime)
                                {
                                    if (endTime > convertedEndTime)
                                    {
                                        var schedule = db.Venue_Schedule_Line.Where(m => m.VenueID == venueID && m.DateAvailable == day).FirstOrDefault();

                                        schedule.EndTime = TimeSpan.Parse(actualEndTime);
                                        db.SaveChanges();

                                        item.Element("endTime").Value = actualEndTime;
                                    }
                                }
                                else
                                {
                                    if (endTime > convertedEndTime)
                                    {
                                        var schedule = db.Venue_Schedule_Line.Where(m => m.VenueID == venueID && m.DateAvailable == day).FirstOrDefault();

                                        schedule.StartTime = TimeSpan.Parse(actualStartTime);
                                        schedule.EndTime = TimeSpan.Parse(actualEndTime);
                                        db.SaveChanges();

                                        item.Element("startTime").Value = actualStartTime;
                                        item.Element("endTime").Value = actualEndTime;
                                    }
                                    else
                                    {
                                        var schedule = db.Venue_Schedule_Line.Where(m => m.VenueID == venueID && m.DateAvailable == day).FirstOrDefault();

                                        schedule.StartTime = TimeSpan.Parse(actualStartTime);
                                        db.SaveChanges();

                                        item.Element("startTime").Value = actualStartTime;
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    XmlSerializer serializer = new XmlSerializer(typeof(List<Date>), new XmlRootAttribute("Dates"));

                    StringReader stringReader = new StringReader(doc.ToString());

                    List<Date> myDates = (List<Date>)serializer.Deserialize(stringReader);

                    /*var query = myDates.GroupBy(d => d.date).Select(k => k.Key).ToList();

                    foreach (var item in query)
                    {
                        var datesinXML = myDates.Where(d => d.date == item).Distinct();
                    }*/

                    using (FileStream fs = new FileStream(absolutePath, FileMode.Create))
                    {
                        serializer.Serialize(fs, myDates);
                    }


                    if (countofStartTimes > 2)
                    {

                    }
                }
                else
                {
                    if (db.Venue_Schedule_Line.ToList().Count() > 0)
                    {
                        var newID = db.Venue_Schedule_Line.OrderByDescending(m => m.ScheduleID).FirstOrDefault().ScheduleID + 1;

                        db.Venue_Schedule_Line.Add(new CodeFirst.Venue_Schedule_Line
                        {
                            ScheduleID = newID,
                            DateAvailable = day,
                            StartTime = TimeSpan.Parse(actualStartTime),
                            EndTime = TimeSpan.Parse(actualEndTime),
                            Status = "Active",
                            VenueID = venueID,
                        });
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Venue_Schedule_Line.Add(new CodeFirst.Venue_Schedule_Line
                        {
                            ScheduleID = 1,
                            DateAvailable = day,
                            StartTime = TimeSpan.Parse(actualStartTime),
                            EndTime = TimeSpan.Parse(actualEndTime),
                            Status = "Active",
                            VenueID = venueID,
                        });
                        db.SaveChanges();
                    }
                    model.JavaScriptToRun = "mySuccess()";

                    dates = GetAllDaysForDayOfWeek(day, startTime, endTime);
                    dates.Sort();

                    var startDates = dates.ToList().Where((c, i) => i % 2 == 0);
                    var endDates = dates.ToList().Where((c, i) => i % 2 != 0);

                    XElement date = doc.Element("Dates");
                    date.Add(
                        from start in startDates
                        join end in endDates on start.Date equals end.Date
                        select new XElement("Date",
                        new XElement("dayofWeek", day),
                        new XElement("date", start.ToString("dd/MM/yyyy")),
                        new XElement("startTime", start.TimeOfDay.ToString()),
                        new XElement("endTime", end.TimeOfDay.ToString()))
                        );
                    doc.Save(absolutePath);
                }
            }
            else
            {
                if (db.Venue_Schedule_Line.ToList().Count() > 0)
                {
                    var newID = db.Venue_Schedule_Line.OrderByDescending(m => m.ScheduleID).FirstOrDefault().ScheduleID + 1;

                    db.Venue_Schedule_Line.Add(new CodeFirst.Venue_Schedule_Line
                    {
                        ScheduleID = newID,
                        DateAvailable = day,
                        StartTime = TimeSpan.Parse(actualStartTime),
                        EndTime = TimeSpan.Parse(actualEndTime),
                        Status = "Active",
                        VenueID = venueID,
                    });
                    db.SaveChanges();
                }
                else
                {
                    db.Venue_Schedule_Line.Add(new CodeFirst.Venue_Schedule_Line
                    {
                        ScheduleID = 1,
                        DateAvailable = day,
                        StartTime = TimeSpan.Parse(actualStartTime),
                        EndTime = TimeSpan.Parse(actualEndTime),
                        Status = "Active",
                        VenueID = venueID,
                    });
                    db.SaveChanges();
                }
                model.JavaScriptToRun = "mySuccess()";
                
                dates = GetAllDaysForDayOfWeek(day, startTime, endTime);
                dates.Sort();

                var startDates = dates.ToList().Where((c, i) => i % 2 == 0);
                var endDates = dates.ToList().Where((c, i) => i % 2 != 0);

                doc = new XDocument(
                    new XElement("Dates",
                    from start in startDates
                    join end in endDates on start.Date equals end.Date
                    select new XElement("Date",
                    new XElement("dayofWeek", day),
                    new XElement("date", start.ToString("dd/MM/yyyy")),
                    new XElement("startTime", start.TimeOfDay.ToString()),
                    new XElement("endTime", end.TimeOfDay.ToString()))
                    ));
                doc.Save(absolutePath);

                /*TimeSpan sTime = TimeSpan.FromHours(startTime);
                TimeSpan eTime = TimeSpan.FromHours(endTime);
                string resultStart = sTime.ToString("hh':'mm");
                string resultEnd = eTime.ToString("hh':'mm");
                doc = new XDocument(
                    new XElement("Dates",
                    new XElement("Date",
                        new XElement("dayofWeek", day),
                        new XElement("date", item.ToString("dd/MM/yyyy")),
                        new XElement("time", item.TimeOfDay.ToString())
                    )));*/
            }

            //DateTime dt = new DateTime();
            //foreach(var date in dates)
            //{
            //    doc.Root.Add(new XElement("date",
            //    new XAttribute("dayOfWeek", day),
            //    new XAttribute("date", date.Date),
            //    new XAttribute("time", date.TimeOfDay)
            //    ));
            //}
            //doc.Save(absolutePath);

            return model.JavaScriptToRun;
        }

        public List<DateTime> GetAllDaysForDayOfWeek(string day, int startTime, int endTime)
        {
            DateTime currentDate = new DateTime();
            currentDate = DateTime.Now;
            var currentYear = currentDate.Year;
            var currentMonth = currentDate.Month;
            var loopYear = currentYear;
            var loopMonth = currentMonth;

            var dates = new List<DateTime>();
            while (loopYear <= currentYear + 1)
            {
                if (loopYear == currentYear)
                {
                    while (loopMonth <= 12)
                    {
                        dates.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(loopYear, loopMonth)).Where(d =>
                            new DateTime(loopYear, loopMonth, d).ToString("dddd").Equals(day))
                            .Select(d => new DateTime(loopYear, loopMonth, d, startTime, 0, 0)).ToList());
                        dates.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(loopYear, loopMonth)).Where(d =>
                            new DateTime(loopYear, loopMonth, d).ToString("dddd").Equals(day))
                            .Select(d => new DateTime(loopYear, loopMonth, d, endTime, 0, 0)).ToList());
                        loopMonth++;
                    }
                    loopMonth = 1;
                    loopYear++;
                }
                while (loopMonth <= 12)
                {
                    dates.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(loopYear, loopMonth)).Where(d =>
                        new DateTime(loopYear, loopMonth, d).ToString("dddd").Equals(day))
                        .Select(d => new DateTime(loopYear, loopMonth, d, startTime, 0, 0)).ToList());
                    dates.AddRange(Enumerable.Range(1, DateTime.DaysInMonth(loopYear, loopMonth)).Where(d =>
                        new DateTime(loopYear, loopMonth, d).ToString("dddd").Equals(day))
                        .Select(d => new DateTime(loopYear, loopMonth, d, endTime, 0, 0)).ToList());
                    loopMonth++;
                }
                loopMonth = 1;
                loopYear++;
            }

            return dates;
        }

        [HttpPost]
        public ActionResult RedirectToAddVenueScheduleDetails(string venueID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AddVenueSchedule", new { venueID = venueID });
            return Json(new { Url = redirectUrl });
        }
    }

    /*public class Dates
    {
        [XmlArray("Dates"), XmlArrayItem(typeof(Date), ElementName = "Date")]
        public List<Date> DateList { get; set; }
    }*/

}
