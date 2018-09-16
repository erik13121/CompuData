namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Donation")]
    public partial class Donation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donation()
        {
            Donation_Line = new HashSet<Donation_Line>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonationID { get; set; }

        [Required]
        [MaxLength(25)]
        public string QuantityAmount { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateDate { get; set; }

        public int QuatityTypeID { get; set; }

        public int? DonorPID { get; set; }

        public int? DonorOrgID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation_Line> Donation_Line { get; set; }

        public virtual Donor_Org Donor_Org { get; set; }

        public virtual Donor_Person Donor_Person { get; set; }

        public virtual Quantity_Type Quantity_Type { get; set; }
    }
}
