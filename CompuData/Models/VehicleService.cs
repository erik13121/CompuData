using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class VehicleService
    {
        [Key]
        [Column(Order = 0)]
        public int IntervalID { get; set; }

        [Required(ErrorMessage = "The Service Date is required")]
        [Column(TypeName = "date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ServiceDate { get; set; }

        [Required(ErrorMessage = "Whether the Service Date has been completed is required")]
        [Column(TypeName = "bit")]
        //[RegularExpression("\\d 0|1|0", ErrorMessage = "Only a 0 or 1 is allowed for Completed")]
        public bool Completed { get; set; }

        public string CompletedString { get; set; }

        [Required(ErrorMessage = "The Vehicle is required")]
        [Column(Order = 1)]
        public int VehicleID { get; set; }

        public string JavaScriptToRun { get; set; }

        public VehicleService() { }
        public VehicleService(int id, DateTime serviceDate, bool completed, int vehicleID)
        {
            IntervalID = id;
            ServiceDate = serviceDate;
            Completed = completed;
            VehicleID = vehicleID;
            //if (Completed == 0)
            //{
            //    CompletedString = "False";
            //}
            //else
            //{
            //    CompletedString = "True";
            //}
        }

        public static IEnumerable<CodeFirst.Service> Data;
        public static IEnumerable<CodeFirst.Service> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var services = db.Services.ToList();

            Data = services;
            return Data;
        }
    }
}