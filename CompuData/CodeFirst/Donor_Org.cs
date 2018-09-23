namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donor_Org
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donor_Org()
        {
            Donations = new HashSet<Donation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonorOrgID { get; set; }

        [MaxLength(50)]
        public string OrgName { get; set; }

        [MaxLength(10)]
        public string ContactNum { get; set; }

        [MaxLength(50)]
        public string ContactEmail { get; set; }

        [Column(TypeName = "bit")]
        public bool? Thanked { get; set; }

        [MaxLength(50)]
        public string StreetAddress { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string AreaCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation> Donations { get; set; }
    }
}
