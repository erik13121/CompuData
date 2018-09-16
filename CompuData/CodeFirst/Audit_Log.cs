namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Audit_Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LogID { get; set; }

        [Required]
        [MaxLength(50)]
        public string TableEffected { get; set; }

        [Required]
        [MaxLength(50)]
        public string AttrivuteEffected { get; set; }

        public TimeSpan Time { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string NewRecordValue { get; set; }

        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
