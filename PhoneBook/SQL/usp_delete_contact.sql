IF EXISTS (SELECT * FROM SYS.PROCEDURES WHERE NAME = 'usp_delete_contact')
	DROP PROCEDURE dbo.usp_delete_contact
GO

	CREATE PROCEDURE dbo.usp_delete_contact
	AS 
	BEGIN
			DELETE FROM Contacts
			WHERE userId = 'userId'
	END
GO	
	