use PES_Test
go

------Endorsement Approval----------
 create proc uspEndorsementApproval
 (
	@eId varchar(7),
	@pN varchar(7),
	@flag varchar(10)
 )
 as
 begin
	BEGIN TRANSACTION
		update EndorsementStatus 
		set endorsementStatus = @flag where endorsementId = @eId;
		SAVE TRANSACTION statusUpdated;
		DECLARE @path varchar(255)
		SET @path = (select filePath from Policy where policyNumber = @pN);
		insert into Documents values
		(@eId, @path)
		if(@flag = 'Rejected')
			BEGIN
				ROLLBACK TRANSACTION statusUpdated
			END
	COMMIT
 end

 drop proc uspEndorsementApproval

 ------Display all Endorsement----------
 create proc uspGetAllEndorsements
 as
 begin
	select
		E.Id,
		E.endorsementId,
		E.policyNumber,
		E.FieldChanges,
		ES.endorsementStatus
		from Endorsements E inner join EndorsementStatus ES 
		on E.endorsementId = ES.endorsementId
		order by E.Id desc
 end

 drop proc uspGetAllEndorsements

 ------Display all Endorsement Status----------
 create proc uspGetAllEndorsementsStatus
 (
	@cId varchar(7)
 )
 as
 begin
	select * from EndorsementStatus where policyNumber IN (select policyNumber from Policy where customerId = @cId ) order by Id desc
 end

 drop proc uspGetAllEndorsementsStatus