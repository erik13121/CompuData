namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Equipment")]
    public partial class Equipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equipment()
        {
            Equipment_Schedule_Line = new HashSet<Equipment_Schedule_Line>();
            Repair_Request = new HashSet<Repair_Request>();
            Software_Licenses_Line = new HashSet<Software_Licenses_Line>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EquipmentID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ManufacturerName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ModelNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime DatePurchased { get; set; }

        public int ServiceIntervalMonths { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public int? UserID { get; set; }

        public int TypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Equipment_Schedule_Line> Equipment_Schedule_Line { get; set; }

        public virtual Equipment_Type Equipment_Type { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Repair_Request> Repair_Request { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Software_Licenses_Line> Software_Licenses_Line { get; set; }
    }
}
