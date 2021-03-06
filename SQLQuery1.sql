
create database WebBDS
use WebBDS

create table AccountCustomer(
	Name nvarchar(100),
	Phone nvarchar (100),
	Email nvarchar (100) Primary Key,
	Password nvarchar (100)
);

CREATE TABLE House (
	Id int identity(1,1) primary key,
	Acreage float(50),
	Caption nvarchar(1000),
	Type nvarchar(100),
	Address nvarchar(1000),
	Name nvarchar(100),
	Phone nvarchar(100),
	Description nvarchar(4000),
	Price float(50)
);

create table ImageNameOfHouse(
	Id int identity(1,1) primary key,
	IdImage int,
	ImageName nvarchar(1000),
	constraint FK_IdImage Foreign key (IdImage) references House(Id)
);
