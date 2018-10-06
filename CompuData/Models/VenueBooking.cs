using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class VenueBooking
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookingID { get; set; }

        public int? NumberofPeople { get; set; }

        [Column(TypeName = "date")]
        public string DateBooked { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VenueID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BuildingID { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VenueBookingID { get; set; }

        public int ProjectID { get; set; }

        public VenueBooking() { }
        public VenueBooking(int id, int? people, string dateBooked, TimeSpan startTime, TimeSpan endTime, int userID, int venueID, int buildingID, int venueBookingID, int projectID)
        {
            BookingID = id;
            NumberofPeople = people;
            DateBooked = dateBooked;
            StartTime = startTime;
            EndTime = endTime;
            UserID = userID;
            VenueID = venueID;
            BuildingID = buildingID;
            VenueBookingID = venueBookingID;
            ProjectID = projectID;
        }

        public static IEnumerable<CodeFirst.Venue_Booking_Line> Data;
        public static IEnumerable<CodeFirst.Venue_Booking_Line> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var venueBookingLines = db.Venue_Booking_Line.ToList();

            Data = venueBookingLines;
            return Data;
        }
    }
}