namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EFT_Requisition_Line
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LineID { get; set; }

        [MaxLength(50)]
        public string Details { get; set; }

        public int? QuantityEFT { get; set; }

        public decimal? UnitPriceEFT { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RequisitionID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierID { get; set; }

        public virtual EFT_Requisition EFT_Requisition { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
