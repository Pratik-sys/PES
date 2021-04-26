use PES_Test
go

-----Customers-----
Create table Customers(
	Id INT NOT NULL IDENTITY(1,1),
	customerId AS isnull('CN' + RIGHT('0000'+CONVERT(varchar(5),Id),5),0) PERSISTED Primary key,
	customerName varchar(50) NOT NULL,
	customerAddress varchar(50) NOT NULL,
	customerTelephone varchar(10) NOT NULL,
	customerGender char(1) NOT NULL,
	customerDOB datetime NOT NULL,
	customerSmoker bit NOT NULL check (customerSmoker in (0,1)) default 0,
	customerHobbies varchar(255) 
);


-----InsuranceProducts-----
Create table InsuranceProducts(
	Id INT NOT NULL IDENTITY(1,1),
	productId AS isnull('PI' + RIGHT('0000'+CONVERT(varchar(5),Id),5),0) PERSISTED Primary key,
	productName varchar(30) NOT NULL,
	productLine varchar(8) NOT NULL
);


-----CustomerLoginCredentials-----
Create table CustomerLoginCredentials(
	Id INT NOT NULL IDENTITY(1,1),
	loginId AS isnull('CL' + RIGHT('0000'+CONVERT(varchar(5),Id),5),0) PERSISTED Primary key,
	customerId varchar(7) Foreign Key references dbo.Customers(customerId),
);

-----AdminLoginCredentials-----
Create table AdminLoginCredentials(
	Id INT NOT NULL IDENTITY(1,1),
	adminLoginId varchar(7) Primary key,
	adminPassword varchar(7)
);

-----Policy-----
Create table Policy(
	Id INT NOT NULL IDENTITY(1,1),
	policyNumber AS isnull('PN' + RIGHT('0000'+CONVERT(varchar(5),Id),5),0) PERSISTED Primary key,
	customerId varchar(7) Foreign Key references dbo.Customers(customerId),
	productId varchar(7) Foreign Key references dbo.InsuranceProducts(productId),
	policyNominee varchar(30) NOT NULL,
	policyNomineeRelation varchar(30) NOT NULL,
	policyPremiumPaymentFrequency varchar(30) NOT NULL,
	filePath varchar(255) NOT NULL
);


-----Endorsements-----
Create table Endorsements(
	Id INT NOT NULL IDENTITY(1,1),
	endorsementId AS isnull('EI' + RIGHT('0000'+CONVERT(varchar(5),Id),5),0) PERSISTED Primary key,
	policyNumber varchar(7) Foreign Key references Policy(policyNumber),
	FieldChanges varchar(500)  
);

-----EndorsementsSatatus-----
Create table EndorsementStatus(
	Id INT NOT NULL IDENTITY(1,1),
	endorsementId varchar(7) foreign key references Endorsements(endorsementId),
	policyNumber varchar(7) Foreign Key references Policy(policyNumber),
	endorsementStatus varchar(10) NOT NULL Default 'Pending' 
);

drop table EndorsementStatus

-----Document-----
Create table Documents(
	endorsementId varchar(7) Foreign Key references dbo.Endorsements(endorsementId),
	document varchar(255) NOT NULL
);

select * from EndorsementStatus where policyNumber IN (select * from Customers where customerId = '1');