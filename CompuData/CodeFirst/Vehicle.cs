namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            Repair_Request = new HashSet<Repair_Request>();
            Services = new HashSet<Service>();
            Vehicle_Schedule_Line = new HashSet<Vehicle_Schedule_Line>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VehicleID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Brand { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string Model { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string NumberPlate { get; set; }

        [Column(TypeName = "date")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfPurchase { get; set; }

        [Column(TypeName = "date")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DateofLastRepair { get; set; }

        [Column(TypeName = "date")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DateofLicencePurchase { get; set; }

        [Column(TypeName = "date")]
        //[DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? LicenseExpireDate { get; set; }

        public int? ServiceIntervalInMonths { get; set; }

        public int? ServiceIntervalInKMs { get; set; }

        public int TypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Repair_Request> Repair_Request { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle_Schedule_Line> Vehicle_Schedule_Line { get; set; }

        [ForeignKey(nameof(TypeID))]
        public Vehicle_Type Vehicle_Type { get; set; }
    }
}
