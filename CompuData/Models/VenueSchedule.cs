using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class VenueSchedule
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ScheduleID { get; set; }

        [Required(ErrorMessage = "The Schedule Start Time is required")]
        public TimeSpan StartTime { get; set; }


        [Required(ErrorMessage = "The Schedule End Time is required")]
        public TimeSpan EndTime { get; set; }


        [Required(ErrorMessage = "The Schedule Day of the Week is required")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public string DateAvailable { get; set; }


        [Required(ErrorMessage = "The Schedule Status (Active/Inactive) is required")]
        [MaxLength(25)]
        public string Status { get; set; }

        public string JavaScriptToRun { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BuildingID { get; set; }

        public string BuildingName { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VenueID { get; set; }

        public string Name { get; set; }

        public VenueSchedule() { }
        public VenueSchedule(int id, string scheduleDay, TimeSpan start, TimeSpan end, string status, int buildID, int venID)
        {
            ScheduleID = id;
            DateAvailable = scheduleDay;
            StartTime = start;
            EndTime = end;
            Status = status;
            BuildingID = buildID;
            VenueID = venID;
            //if (Completed == 0)
            //{
            //    CompletedString = "False";
            //}
            //else
            //{
            //    CompletedString = "True";
            //}
        }

        public static IEnumerable<CodeFirst.Venue_Schedule_Line> Data;
        public static IEnumerable<CodeFirst.Venue_Schedule_Line> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var schedules = db.Venue_Schedule_Line.ToList();

            Data = schedules;
            return Data;
        }
    }
}