namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Repair_Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequestID { get; set; }

        [Column(TypeName = "bit")]
        public bool Approved { get; set; }

        [Column(TypeName = "bit")]
        public bool Repaired { get; set; }

        [Required]
        [MaxLength(50)]
        public string Reason { get; set; }

        public int? VehicleID { get; set; }

        public int? EquipmentID { get; set; }

        public int RepPersonID { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual RepairPerson RepairPerson { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
