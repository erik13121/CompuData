namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Software_Licenses_Line
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastActivatedDate { get; set; }

        [Column(TypeName = "bit")]
        public bool Activated { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EquipmentID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LicenceID { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual Software_Licenses Software_Licenses { get; set; }
    }
}
