use PES_Test
go

------Apply for Policy----------
create proc uspApplyForPolicy
(
	@custId varchar(7),
	@productId varchar(7),
	@policyNom varchar(30),
	@policyNomRelation varchar(30),
	@policyPremiumPaymentFreqn varchar(30),
	@filePath varchar(225)
)
as
begin
insert into Policy (customerId,productId,policyNominee,policyNomineeRelation,policyPremiumPaymentFrequency,filePath) 
values(@custId,@productId,@policyNom,@policyNomRelation,@policyPremiumPaymentFreqn,@filePath)
end
go

------Update Policy----------
create proc uspUpdatePolicy
(
	@custId varchar(7),
	@policyNum varchar(7),
	@policyNom varchar(30),
	@policyNomRelation varchar(30),
	@policyPremiumPaymentFreqn varchar(30),
	@filePath varchar(225),
	@custAddress varchar(50),
	@custTelephone varchar(10),
	@custSmoker bit
)
as 
begin
	Declare @changes varchar(500);
	if(@custAddress <> (select customerAddress from Customers where customerId = @custId))
	BEGIN
		SET @changes = CONCAT(' Address : ',@custAddress)
	END
	if(@custTelephone <> (select customerTelephone from Customers where customerId = @custId))
	BEGIN
		SET @changes = CONCAT(' Telephone : ',@custAddress)
	END
	if(@custSmoker <> (select customerSmoker from Customers where customerId = @custId))
	BEGIN
		SET @changes = CONCAT(' Smoker : ',@custAddress)
	END
	if(@policyNom <> (select policyNominee from Policy where policyNumber = @policyNum))
	BEGIN  
	  SET @changes = CONCAT('Nominee : ',@policyNom)
	END  
	if(@policyNomRelation <> (select policyNomineeRelation from Policy where policyNumber = @policyNum))
	BEGIN  
	  SET @changes = CONCAT('NomineeRelatation : ',@policyNomRelation)
	END  
	if(@policyPremiumPaymentFreqn <> (select policyPremiumPaymentFrequency from Policy where policyNumber = @policyNum))
	BEGIN
	  SET @changes = CONCAT('PremiumPaymetFrequency : ',@policyPremiumPaymentFreqn)
	END
	if(@filePath <> (select filePath from Policy where policyNumber = @policyNum))
	BEGIN
	  SET @changes = CONCAT('FilePath : ',@filePath)
	END
	update Customers set 
	customerAddress = @custAddress, customerTelephone=@custTelephone, customerSmoker=@custSmoker 
	where customerId=@custId;
	update Policy set 
	policyNominee =@policyNom, policyNomineeRelation=@policyNomRelation, policyPremiumPaymentFrequency=@policyPremiumPaymentFreqn, filePath=@filePath 
	where policyNumber=@policyNum;
	insert into Endorsements(policyNumber, FieldChanges) values
	(@policyNum, @changes);
	declare @eId varchar(7);
	SET @eId = (select Top 1 endorsementId from Endorsements order by Id desc);
	SET @policyNum = (select @policyNum from Endorsements where endorsementId = @eId);
	insert into EndorsementStatus(endorsementId,policyNumber)
	values(@eId, @policyNum);
 end

 drop proc uspUpdatePolicy

 ------Search Policy----------
 ----- Search by policy number -----
CREATE PROCEDURE uspSearchByPolicyNumber
(
	@policyNumber varchar(7)
)
as
	SELECT * from Policy Where policyNumber = @policyNumber;
go

----- Search by Customer ID -----
CREATE PROCEDURE uspSearchByCustomerId
(
	@customerId varchar(7)
)
as
begin
	SELECT * from Policy Where customerId = @customerId;
end
go

----- Search by Customer Name AND DOB -----
CREATE PROCEDURE uspSearchByCustomerNameAndDob
(
	@customerName varchar(50),
	@customerDOB datetime
)
as
	SELECT * from Policy
	Where customerId IN (SELECT customerId from Customers WHERE customerName = @customerName and customerDOB = @customerDOB);
go

Drop proc uspSearchByCustomerNameAndDob

----- Display All Policies ------
create proc uspGetAllPolicy
as
begin
	select * from Policy 
end

----- View Policies ------
Create proc uspViewPolicy
(
    @pN varchar(7)
)
as
Begin
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
	P.policyNomineeRelation, 
	P.policyPremiumPaymentFrequency,
	P.filePath,
	C.customerSmoker
	From  Policy P left join Customers C
		On  C.customerId = P.customerId
		left join InsuranceProducts I
		On P.productId = I.productId
	where  P.policyNumber = @pN
End


drop proc uspViewPolicy

-----Get Policy by Customer Number and Policy Number------
create proc uspSearchPolicyByCustomerIdAndPolicyNumber
(
	@cId varchar(7),
	@pN varchar(7)
)
as
begin
	select * from Policy where customerId = @cId and policyNumber = @pN
end