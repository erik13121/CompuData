namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Funder_Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FunderPersonID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Initials { get; set; }

        [MaxLength(10)]
        public string CellNum { get; set; }

        [Required]
        [MaxLength(50)]
        public string PersonalEmail { get; set; }

        [MaxLength(50)]
        public string Bank { get; set; }

        public string AccountNumber { get; set; }

        public string BranchCode { get; set; }

        [MaxLength(50)]
        public string StreetAddress { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string AreaCode { get; set; }

        [Column(TypeName = "bit")]
        public bool Thanked { get; set; }

        public int? ProjectID { get; set; }

        public int TypeID { get; set; }

        public virtual Project Project { get; set; }

        public virtual Funder_Type Funder_Type { get; set; }
    }
}
