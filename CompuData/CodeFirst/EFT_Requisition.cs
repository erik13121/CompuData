namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFT_Requisition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EFT_Requisition()
        {
            EFT_Requisition_Line = new HashSet<EFT_Requisition_Line>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequisitionID { get; set; }

        [Column(TypeName = "bit")]
        public bool ApprovedProjectManger { get; set; }

        [Column(TypeName = "bit")]
        public bool ApprovedCEO { get; set; }

        [MaxLength(255)]
        public string ReceiptFile { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public decimal? TotalAmount { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierID { get; set; }

        public int ProjectID { get; set; }

        public int UserID { get; set; }

        public virtual Project Project { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EFT_Requisition_Line> EFT_Requisition_Line { get; set; }
    }
}
