using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class EquipmentBooking
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookingID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EquipmentID { get; set; }

        public int? PagesPrinted { get; set; }

        public string DateBooked { get; set; }

        public TimeSpan TimeIn { get; set; }

        public TimeSpan TimeOut { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EquipmentBookingID { get; set; }

        public int? ProjectID { get; set; }

        public int UserID { get; set; }

        public EquipmentBooking() { }
        public EquipmentBooking(int id, int equipID, int? pages, string dateBooked, TimeSpan startTime, TimeSpan endTime, int bookingID, int? projectID, int userID)
        {
            BookingID = id;
            EquipmentID = equipID;
            PagesPrinted = pages;
            DateBooked = dateBooked;
            TimeIn = startTime;
            TimeOut = endTime;
            EquipmentBookingID = bookingID;
            ProjectID = projectID;
            UserID = userID;
        }

        public static IEnumerable<CodeFirst.Equipment_Booking_Line> Data;
        public static IEnumerable<CodeFirst.Equipment_Booking_Line> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var equipmentBookingLines = db.Equipment_Booking_Line.ToList();

            Data = equipmentBookingLines;
            return Data;
        }
    }
}