namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Active_Login
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LoginID { get; set; }

        [Required]
        [MaxLength(20)]
        public string IPAddress { get; set; }

        public DateTime? LoginTimestamp { get; set; }

        public TimeSpan? LatestActionTimestamp { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserID { get; set; }

        public virtual User User { get; set; }
    }
}
