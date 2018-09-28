Use CompuDataSQL
Go

alter table Donor_Org
alter column ContactNum varchar(50) null

alter table Donor_Person
alter column Initials varchar(10) null

alter table Donor_Person
alter column CellNum varchar(50) null

alter table Funder_Org
alter column AccountNumber varchar(50) null

alter table Funder_Org
alter column BranchCode varchar(50) null

alter table Funder_Org
alter column ContactNumber varchar(50) null

alter table Funder_Person
alter column AccountNumber varchar(50) null

alter table Funder_Person
alter column BranchCode varchar(50) null

alter table Funder_Person
alter column CellNum varchar(50) null

alter table Funder_Person
alter column Initials varchar(10) null

alter table Supplier
alter column AccountNumber varchar(50) null

alter table Supplier
alter column BranchCode varchar(50) null

alter table Supplier
alter column ContactNumber varchar(50) null

alter table [User]
alter column Initials varchar(10) null

alter table [User]
alter column CellNum varchar(50) null

alter table [User]
alter column TelNum varchar(50) null

alter table [User]
alter column WorkNum varchar(50) null