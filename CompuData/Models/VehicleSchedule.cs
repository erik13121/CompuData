using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class VehicleSchedule
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Veh_Schedule_ID { get; set; }

        [Required(ErrorMessage = "The Schedule Day of the Week is required")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public string Date { get; set; }

        [Required(ErrorMessage = "The Schedule Start Time is required")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "The Schedule End Time is required")]
        public TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = "The Schedule Status (Active/Inactive) is required")]
        [MaxLength(25)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VehicleID { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string NumberPlate { get; set; }

        public string JavaScriptToRun { get; set; }

        public VehicleSchedule() { }
        public VehicleSchedule(int id, string scheduleDay, TimeSpan start, TimeSpan end, string status, int vehID)
        {
            Veh_Schedule_ID = id;
            Date = scheduleDay;
            StartTime = start;
            EndTime = end;
            Status = status;
            VehicleID = vehID;
            //if (Completed == 0)
            //{
            //    CompletedString = "False";
            //}
            //else
            //{
            //    CompletedString = "True";
            //}
        }

        public static IEnumerable<CodeFirst.Vehicle_Schedule_Line> Data;
        public static IEnumerable<CodeFirst.Vehicle_Schedule_Line> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var schedules = db.Vehicle_Schedule_Line.ToList();

            Data = schedules;
            return Data;
        }
    }
}