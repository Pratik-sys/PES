use PES_Test
go

-----Update insurance Product Procedure-----
create proc uspUpdateInsuranceProduct
(
	@productId varchar(7),
	@productName varchar(30),
	@productLine varchar(8)
)
as
begin
	update InsuranceProducts set productName = @productName, productLine = @productLine
	where productId = @productId
end
go



-----Insert insurance Product Procedure-----
create procedure uspInsertInsuranceProduct
(
		 @productName varchar(30),
		 @productLine varchar(8)
)
as
begin
	insert into InsuranceProducts values(@productName,@productLine)
end
Go

-----Delete insurance Product Procedure-----
create procedure uspDeleteInsuranceProduct
(
	@insuranceId varchar(7)
)
as
begin
	delete from InsuranceProducts where productId = @insuranceId
end
Go

-----Search insurance Product Procedure-----
create procedure uspSearchInsuranceProduct
(
	@insuranceId varchar(7)
)
as
begin
	select * from InsuranceProducts where productId = @insuranceId;
end
Go

-----Display insurance Product Procedure-----
create proc uspDisplayInsuranceProducts
as
begin
	select * from InsuranceProducts
end