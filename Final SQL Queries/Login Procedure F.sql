 use PES_Test
 go
 
 ------Sign Up----------
 create proc uspSignUp
 (
	@cId varchar(7)
 )
 as
 begin
	insert into CustomerLoginCredentials(customerId)
	values (@cId)
 end

 drop proc uspSignUp

-----Check Login Credentials------
create proc uspCheckLoginCredentials
(
	@userId varchar(7),
	@password varchar(7),
	@loginType varchar(1)
)
as
begin
	if(@loginType = 'C')
	Begin
		select count(*) from CustomerLoginCredentials where loginId = @userId and customerId = @password 
	end
	if(@loginType = 'A')
	Begin
		select count(*) from AdminLoginCredentials where adminLoginId = @userId and adminPassword = @password
	end
end

drop proc uspCheckLoginCredentials

-----Check Login Credentials------
create proc uspGetCurrentLoginCredentials
as 
begin 
	Select top 1 * from  CustomerLoginCredentials order by Id DESC
end

-----Find Admin Login Credentials------
create proc uspFindAdmin
as
begin
	select count(*) from AdminLoginCredentials
end

-----Create Admin Login Credentials---------
create proc uspCreateAdmin
as
begin
	insert into AdminLoginCredentials(adminLoginId, adminPassword)
	values('SUL0001','SUP0001');
end

drop proc uspCreateAdmin

EXEC uspCreateAdmin




