namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Project")]
    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            Donation_Line = new HashSet<Donation_Line>();
            EFT_Requisition = new HashSet<EFT_Requisition>();
            Equipment_Booking_Line = new HashSet<Equipment_Booking_Line>();
            Funder_Org = new HashSet<Funder_Org>();
            Funder_Person = new HashSet<Funder_Person>();
            Petty_Cash_Requisition = new HashSet<Petty_Cash_Requisition>();
            Vehicle_Booking_Line = new HashSet<Vehicle_Booking_Line>();
            Venue_Booking_Line = new HashSet<Venue_Booking_Line>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProjectName { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime ExpectedFinishDate { get; set; }

        [Column(TypeName = "bit")]
        public bool Finished { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProjectDescription { get; set; }

        public int TypeID { get; set; }

        public int UserID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation_Line> Donation_Line { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EFT_Requisition> EFT_Requisition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment_Booking_Line> Equipment_Booking_Line { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funder_Org> Funder_Org { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funder_Person> Funder_Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Petty_Cash_Requisition> Petty_Cash_Requisition { get; set; }

        public virtual Project_Type Project_Type { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle_Booking_Line> Vehicle_Booking_Line { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venue_Booking_Line> Venue_Booking_Line { get; set; }
    }
}
