﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CompuDataSQLEntities : DbContext
    {
        public CompuDataSQLEntities()
            : base("name=CompuDataSQLEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Access_Level> Access_Level { get; set; }
        public virtual DbSet<Active_Login> Active_Login { get; set; }
        public virtual DbSet<Audit_Log> Audit_Log { get; set; }
        public virtual DbSet<Booking_Refreshment_Line> Booking_Refreshment_Line { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Donation> Donations { get; set; }
        public virtual DbSet<Donation_Item> Donation_Item { get; set; }
        public virtual DbSet<Donation_Line> Donation_Line { get; set; }
        public virtual DbSet<Donation_Type> Donation_Type { get; set; }
        public virtual DbSet<Donor_Org> Donor_Org { get; set; }
        public virtual DbSet<Donor_Person> Donor_Person { get; set; }
        public virtual DbSet<EFT_Requisition> EFT_Requisition { get; set; }
        public virtual DbSet<EFT_Requisition_Line> EFT_Requisition_Line { get; set; }
        public virtual DbSet<Employee_Title> Employee_Title { get; set; }
        public virtual DbSet<Equipment> Equipments { get; set; }
        public virtual DbSet<Equipment_Booking> Equipment_Booking { get; set; }
        public virtual DbSet<Equipment_Booking_Line> Equipment_Booking_Line { get; set; }
        public virtual DbSet<Equipment_Schedule_Line> Equipment_Schedule_Line { get; set; }
        public virtual DbSet<Equipment_Type> Equipment_Type { get; set; }
        public virtual DbSet<Funder_Org> Funder_Org { get; set; }
        public virtual DbSet<Funder_Person> Funder_Person { get; set; }
        public virtual DbSet<Funder_Type> Funder_Type { get; set; }
        public virtual DbSet<Petty_Cash_Requisition> Petty_Cash_Requisition { get; set; }
        public virtual DbSet<Petty_Cash_Requisition_Line> Petty_Cash_Requisition_Line { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Project_Type> Project_Type { get; set; }
        public virtual DbSet<Quantity_Type> Quantity_Type { get; set; }
        public virtual DbSet<Refreshment> Refreshments { get; set; }
        public virtual DbSet<Refreshment_Booking> Refreshment_Booking { get; set; }
        public virtual DbSet<Repair_Request> Repair_Request { get; set; }
        public virtual DbSet<RepairPerson> RepairPersons { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Software_Licenses> Software_Licenses { get; set; }
        public virtual DbSet<Software_Licenses_Line> Software_Licenses_Line { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vehicle_Booking> Vehicle_Booking { get; set; }
        public virtual DbSet<Vehicle_Booking_Line> Vehicle_Booking_Line { get; set; }
        public virtual DbSet<Vehicle_Schedule_Line> Vehicle_Schedule_Line { get; set; }
        public virtual DbSet<Vehicle_Type> Vehicle_Type { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }
        public virtual DbSet<Venue_Booking> Venue_Booking { get; set; }
        public virtual DbSet<Venue_Booking_Line> Venue_Booking_Line { get; set; }
        public virtual DbSet<Venue_Schedule_Line> Venue_Schedule_Line { get; set; }
    }
}
