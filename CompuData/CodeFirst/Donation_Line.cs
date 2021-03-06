namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donation_Line
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LineID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonationID { get; set; }

        public int DonationItemID { get; set; }

        public int? ProjectID { get; set; }

        public int DonationAmount { get; set; }

        public virtual Donation Donation { get; set; }

        public virtual Donation_Item Donation_Item { get; set; }

        public virtual Project Project { get; set; }
    }
}
