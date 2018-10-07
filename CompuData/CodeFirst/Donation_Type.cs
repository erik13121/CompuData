namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Donation_Type
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Donation_Type()
        {
            Donation_Item = new HashSet<Donation_Item>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeID { get; set; }

        [Required]
        [MaxLength(250)]
        public string TypeName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Donation_Item> Donation_Item { get; set; }
    }
}
