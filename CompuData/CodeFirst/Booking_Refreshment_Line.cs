namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking_Refreshment_Line
    {
        public int Quantity { get; set; }

        public double UnitCost { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RefreshmentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [MaxLength(10)]
        public string RefreshBookingID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BookingID { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VenueID { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BuildingID { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VenueBookingID { get; set; }

        public virtual Building Building { get; set; }

        public virtual Refreshment Refreshment { get; set; }

        public virtual Refreshment_Booking Refreshment_Booking { get; set; }

        public virtual User User { get; set; }

        public virtual Venue_Booking Venue_Booking { get; set; }

        public virtual Venue_Booking_Line Venue_Booking_Line { get; set; }

        public virtual Venue Venue { get; set; }
    }
}
