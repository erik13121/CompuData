namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donation_Item
    {
        [Required]
        public int DonationItemID { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }


        [Required]
        public int TotalAmount { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeID { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuantityTypeID { get; set; }


        public virtual Donation_Type Donation_Type { get; set; }

        public virtual Quantity_Type Quantity_Type { get; set; }

    }
}
