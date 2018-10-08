using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class EquipmentSchedule
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LineID { get; set; }

        [Required(ErrorMessage = "The Schedule Day of the Week is required")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public string Date { get; set; }

        [Required(ErrorMessage = "The Schedule Start Time is required")]
        public TimeSpan TimeStart { get; set; }

        [Required(ErrorMessage = "The Schedule End Time is required")]
        public TimeSpan TimeEnd { get; set; }

        [Required(ErrorMessage = "The Schedule Status (Active/Inactive) is required")]
        [MaxLength(25)]
        public string Status { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EquipmentID { get; set; }

        public string ManufacturerName { get; set; }

        public string ModelNumber { get; set; }

        public string JavaScriptToRun { get; set; }

        public EquipmentSchedule() { }
        public EquipmentSchedule(int id, string scheduleDay, TimeSpan start, TimeSpan end, string status, int equipID)
        {
            LineID = id;
            Date = scheduleDay;
            TimeStart = start;
            TimeEnd = end;
            Status = status;
            EquipmentID = equipID;
            //if (Completed == 0)
            //{
            //    CompletedString = "False";
            //}
            //else
            //{
            //    CompletedString = "True";
            //}
        }

        public static IEnumerable<CodeFirst.Equipment_Schedule_Line> Data;
        public static IEnumerable<CodeFirst.Equipment_Schedule_Line> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var schedules = db.Equipment_Schedule_Line.ToList();

            Data = schedules;
            return Data;
        }
    }
}