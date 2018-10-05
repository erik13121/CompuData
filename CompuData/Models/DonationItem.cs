using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class DonationItem
    {
        public int DonationItemID { get; set; }

        [Required(ErrorMessage = "The Donation Item Description is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(150, ErrorMessage = "Max of 150 characters allowed as the Donation Item Name")]
        public string Description { get; set; }

        [RegularExpression("\\d{1,6}", ErrorMessage = "The Total Amount has to consist of between 1 and 6-digit numbers")]
        [Required(ErrorMessage = "The Total Amount is Required")]
        public int TotalAmount { get; set; }


        [Required(ErrorMessage = "Please Add a Donation Type first")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeID { get; set; }

        public string TypeName { get; set; }


        [Required(ErrorMessage = "Please Add a Quantity Type first")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int QuantityTypeID { get; set; }

        public string QuantityDescription { get; set; }

        public DonationItem() { }
        public DonationItem(int id, string description, int totalAmount, int typeID, int quantityid)
        {
            DonationItemID = id;
            Description = description;
            TotalAmount = totalAmount;
            TypeID = typeID;
            QuantityTypeID = quantityid;
        }

        public static IEnumerable<CodeFirst.Donation_Item> Data;
        public static IEnumerable<CodeFirst.Donation_Item> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var DonoItem = db.Donation_Item.ToList();

            Data = DonoItem;
            return Data;
        }
    }
}