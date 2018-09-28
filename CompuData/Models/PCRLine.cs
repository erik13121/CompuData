using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class PCRLine
    {
        public int LineID { get; set; }

        [MaxLength(50)]
        public string Details { get; set; }

        public int Quantity { get; set; }

        [RegularExpression("^[1-9]\\d{1,17}(\\.\\d{2}$)", ErrorMessage = "Unit Price must have between 1 and 18 characters and 2 decimal points")]
        public decimal? UnitPrice { get; set; }

        public int RequisitionID { get; set; }

        public int SupplierID { get; set; }

        public List<Supplier> Suppliers { get; set; }

        public string JavaScriptToRun { get; set; }

        public PCRLine() { }

        public PCRLine(int ID, string Dets, int Quants, decimal? Price, int SupID, int reqID)
        {
            LineID = ID;
            Details = Dets;
            Quantity = Quants;
            UnitPrice = Price;
            SupplierID = SupID;
            RequisitionID = reqID;
        }

        public static IEnumerable<CodeFirst.Petty_Cash_Requisition_Line> Data;
        public static IEnumerable<CodeFirst.Petty_Cash_Requisition_Line> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var PCRLine = db.Petty_Cash_Requisition_Line.ToList();

            Data = PCRLine;
            return Data;
        }
    }
}