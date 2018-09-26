namespace CompuData.CodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Funder_Org
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FunderOrgID { get; set; }

        [MaxLength(50)]
        public string OrgName { get; set; }

        [MaxLength(50)]
        public string ContactNumber { get; set; }

        [MaxLength(50)]
        public string EmailAddress { get; set; }

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
        public bool? Thanked { get; set; }

        public int? ProjectID { get; set; }

        public int TypeID { get; set; }

        public virtual Project Project { get; set; }

        public virtual Funder_Type Funder_Type { get; set; }
    }
}
