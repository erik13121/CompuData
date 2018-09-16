using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class VehicleBooking
    {
        [Key]
        [Required(ErrorMessage = "The Vehicle ID is required")]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VehicleID { get; set; }

        [Required(ErrorMessage = "The Reason for booking is required")]
        [MaxLength(40, ErrorMessage = "A maximum of 40 characters are allowed for the Reason")]
        public string Reason { get; set; }

        [Key]
        [Required(ErrorMessage = "The Project ID is required")]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectID { get; set; }

        public int? OdoEnd { get; set; }

        [Required(ErrorMessage = "The Date Booked for is required")]
        public DateTime DateBooked { get; set; }

        [Required(ErrorMessage = "The Start Time of booking is required")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "The End Time of booking is required")]
        public TimeSpan EndTime { get; set; }

        [Key]
        [Required(ErrorMessage = "The Booking ID is required")]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VehicleBookingID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? IntervalID { get; set; }

        [Key]
        [Required(ErrorMessage = "The User ID is required")]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        public List<CodeFirst.Project> Projects { get; set; }
        public List<CodeFirst.Service> Services { get; set; }
        public List<CodeFirst.User> Users { get; set; }
        public List<CodeFirst.Vehicle_Booking> VehicleBookings { get; set; }
        public string JavaScriptToRun { get; set; }
        public VehicleBooking() { }
        public VehicleBooking(int id, string reason, int projID, int? odoEnd, DateTime dateBooked, TimeSpan startTime, TimeSpan endTime, int bookingID, int intervalID, int userID)
        {
            VehicleID = id;
            Reason = reason;
            ProjectID = projID;
            OdoEnd = odoEnd;
            DateBooked = dateBooked;
            StartTime = startTime;
            EndTime = endTime;
            VehicleBookingID = bookingID;
            IntervalID = intervalID;
            UserID = userID;
        }

        public static IEnumerable<CodeFirst.Vehicle_Booking_Line> Data;
        public static IEnumerable<CodeFirst.Vehicle_Booking_Line> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var vehicleBookingLines = db.Vehicle_Booking_Line.ToList();

            Data = vehicleBookingLines;
            return Data;
        }
    }
}