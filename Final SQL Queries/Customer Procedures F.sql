use PES_Test
go

------Create Customer----------
create proc uspCreateCustomer
(
	@custName varchar(50),
	@custAddress varchar(50),
	@custTelephone varchar(10),
	@custGender char(1),
	@custDOB datetime ,
	@custSmoker bit,
	@custHobbies varchar(255),
	@custId varchar(7) output
)
as
begin
	insert into Customers values(@custName,@custAddress,@custTelephone,@custGender,@custDOB,@custSmoker,@custHobbies)
	SET @custId = (SELECT TOP 1 customerId from Customers ORDER BY Id DESC)
end
go

drop proc uspCreateCustomer

 ------Delete Customer----------
 create proc uspDeleteCustomer
 (
	@cId varchar(7)
 )
 as
 begin
	delete from Customers where customerId = @cId
 end

------Search Customer By Id----------
create proc uspSearchCustomerById
(
	@cId varchar(7)
)
as
begin
	select * from Customers where customerId = @cId
end

------Search Customer By Name and DOB----------
create proc uspSearchCustomerByNameAndDob
(
	@cName varchar(50),
	@cDOB datetime
)
as
begin
	select * from Customers where customerName = @cName and customerDOB = @cDOB
end




 

 ------Display All Customers------
 create proc uspGetAllCustomers
 as
 begin
	select * from Customers
 end
 