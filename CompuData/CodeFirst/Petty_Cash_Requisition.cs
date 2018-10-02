namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Petty_Cash_Requisition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Petty_Cash_Requisition()
        {
            Petty_Cash_Requisition_Line = new HashSet<Petty_Cash_Requisition_Line>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequisitionID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ApprovalStatus { get; set; }

        [Column(TypeName = "bit")]
        public bool? VATInclusive { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReqDate { get; set; }

        public decimal? TotalAmount { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? SupplierID { get; set; }

        public int? ProjectID { get; set; }

        public int? UserID { get; set; }

        public virtual Project Project { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Petty_Cash_Requisition_Line> Petty_Cash_Requisition_Line { get; set; }
    }
}
