namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supplier")]
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            EFT_Requisition = new HashSet<EFT_Requisition>();
            EFT_Requisition_Line = new HashSet<EFT_Requisition_Line>();
            Petty_Cash_Requisition = new HashSet<Petty_Cash_Requisition>();
            Petty_Cash_Requisition_Line = new HashSet<Petty_Cash_Requisition_Line>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int VATNumber { get; set; }

        [MaxLength(50)]
        public string PostalAddress { get; set; }

        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContactNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string Bank { get; set; }

        public int AccountNumber { get; set; }

        public int BranchCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EFT_Requisition> EFT_Requisition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EFT_Requisition_Line> EFT_Requisition_Line { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Petty_Cash_Requisition> Petty_Cash_Requisition { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Petty_Cash_Requisition_Line> Petty_Cash_Requisition_Line { get; set; }
    }
}
