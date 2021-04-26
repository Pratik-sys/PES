use PES_Test
go

delete from Customers where Id in (32,33,34,35,36,38)

select * from Customers

insert into InsuranceProducts(productName, productLine) values ('Ulip', 'Life')

update LoginCredentials
set loginType = 'A' where loginId = 'CL00001'

select * from LoginCredentials

delete from LoginCredentials where Id = 3

select * from Policy

select * from Endorsements

select * from EndorsementStatus
select * from Documents

delete from Policy where Id = 3

select * from InsuranceProducts

select * from Policy

Select 
	C.customerId, 
	C.customerName,
	C.customerAddress,
	customerGender,
	C.customerTelephone,
	C.customerDOB,
	I.productId, 
	I.productName,
	I.productLine,
	P.policyNumber, 
	P.policyNominee, 
	P.policyNomineeRelatation, 
	P.policyPremiumPaymentFrequency
	From  Policy P left join Customers C 
		On  C.customerId = P.customerId
		left join InsuranceProducts I
		On P.productId = I.productId
	where  P.policyNumber = 'PN00001'


select * from Endorsements
select * from EndorsementStatus
select * from Documents

Declare @changes varchar(500);
if('Pawan Bhumi 440025' <> (select customerAddress from Customers where customerId = 'CN00039'))
BEGIN
	SET @changes = CONCAT(' Address : ', '109 A Shakaryanagar Wardha Road')
END
if('9876543210' <> (select customerTelephone from Customers where customerId = 'CN00039'))
BEGIN
	SET @changes = CONCAT(@changes,' Telephone : ','9876543210')
END
select @changes

select * from EndorsementStatus

select * from LoginCredentials

select * from Customers

delete from Customers

delete from CustomerLoginCredentials

delete from InsuranceProducts

delete from Policy

delete from Endorsements

delete from Documents

delete from EndorsementStatus

select * from Policy

select * from AdminLoginCredentials

select * from InsuranceProducts

