using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class EFTRLine
    {
        public int LineID { get; set; }

        [MaxLength(50)]
        public string Details { get; set; }

        public int QuantityEFT { get; set; }

        [RegularExpression("^[1-9]\\d{1,17}(\\.\\d{2}$)", ErrorMessage = "Unit Price must have between 1 and 18 characters and 2 decimal points")]
        public decimal UnitPriceEFT { get; set; }

        public string TotalEFT { get; set; }

        public int RequisitionID { get; set; }

        public int? SupplierID { get; set; }

        public List<Supplier> Suppliers { get; set; }

        public string JavaScriptToRun { get; set; }

        public EFTRLine() { }

        public EFTRLine(int ID, string Dets, int Quants, decimal Price, int SupID, int reqID, string TotalAmount)
        {
            LineID = ID;
            Details = Dets;
            QuantityEFT = Quants;
            UnitPriceEFT = Price;
            SupplierID = SupID;
            RequisitionID = reqID;
            TotalEFT = TotalAmount;
        }

        public static IEnumerable<CodeFirst.EFT_Requisition_Line> Data;
        public static IEnumerable<CodeFirst.EFT_Requisition_Line> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var EFTRLine = db.EFT_Requisition_Line.ToList();

            Data = EFTRLine;
            return Data;
        }
    }
}