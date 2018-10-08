namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Active_Login = new HashSet<Active_Login>();
            Audit_Log = new HashSet<Audit_Log>();
            Booking_Refreshment_Line = new HashSet<Booking_Refreshment_Line>();
            EFT_Requisition = new HashSet<EFT_Requisition>();
            Equipments = new HashSet<Equipment>();
            Equipment_Booking_Line = new HashSet<Equipment_Booking_Line>();
            Petty_Cash_Requisition = new HashSet<Petty_Cash_Requisition>();
            Projects = new HashSet<Project>();
            Vehicle_Booking_Line = new HashSet<Vehicle_Booking_Line>();
            Venue_Booking_Line = new HashSet<Venue_Booking_Line>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        public string UserPicture { get; set; }

        [Required]
        [MaxLength(50)]
        public string Initials { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        [MaxLength(13)]
        public string NationalID { get; set; }

        [MaxLength(50)]
        public string CellNum { get; set; }

        [Required]
        [MaxLength(50)]
        public string TelNum { get; set; }

        [Required]
        [MaxLength(50)]
        public string WorkNum { get; set; }

        [Required]
        [MaxLength(50)]
        public string PersonalEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string WorkEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string StreetAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression("\\d{4,6}", ErrorMessage = "The Area Code has to consist of between 4 and 6 numbers")]
        public string AreaCode { get; set; }

        [MaxLength(50)]
        public string POAddress { get; set; }

        public string POCity { get; set; }

        public string POAreaCode { get; set; }

        [Column(TypeName = "bit")]
        public bool? ValidLicense { get; set; }

        public int JobTitleID { get; set; }

        public int AccessLevelID { get; set; }

        public virtual Access_Level Access_Level { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Active_Login> Active_Login { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audit_Log> Audit_Log { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking_Refreshment_Line> Booking_Refreshment_Line { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EFT_Requisition> EFT_Requisition { get; set; }

        public virtual Employee_Title Employee_Title { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment> Equipments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment_Booking_Line> Equipment_Booking_Line { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Petty_Cash_Requisition> Petty_Cash_Requisition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Projects { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle_Booking_Line> Vehicle_Booking_Line { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venue_Booking_Line> Venue_Booking_Line { get; set; }
    }
}
