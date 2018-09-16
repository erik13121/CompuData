//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompuData.Global
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            this.Repair_Request = new HashSet<Repair_Request>();
            this.Services = new HashSet<Service>();
            this.Vehicle_Schedule_Line = new HashSet<Vehicle_Schedule_Line>();
        }
    
        public int VehicleID { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string NumberPlate { get; set; }
        public Nullable<System.DateTime> DateOfPurchase { get; set; }
        public Nullable<System.DateTime> DateofLastRepair { get; set; }
        public Nullable<System.DateTime> DateofLicencePurchase { get; set; }
        public Nullable<System.DateTime> LicenseExpireDate { get; set; }
        public Nullable<int> ServiceIntervalInMonths { get; set; }
        public Nullable<int> ServiceIntervalInKMs { get; set; }
        public int TypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Repair_Request> Repair_Request { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Service> Services { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vehicle_Schedule_Line> Vehicle_Schedule_Line { get; set; }
        public virtual Vehicle_Type Vehicle_Type { get; set; }
    }
}
