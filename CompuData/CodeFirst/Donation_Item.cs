namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donation_Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donation_Item()
        {
            Donation_Line = new HashSet<Donation_Line>();
        }

        [Key]
        [Required]
        public int DonationItemID { get; set; }

        [Required]
        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public int TotalAmount { get; set; }
        
        public int TypeID { get; set; }
        
        public int QuantityTypeID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation_Line> Donation_Line { get; set; }

        public virtual Donation_Type Donation_Type { get; set; }

        public virtual Quantity_Type Quantity_Type { get; set; }

    }
}
