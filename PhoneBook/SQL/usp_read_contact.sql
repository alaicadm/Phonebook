IF EXISTS (SELECT * FROM SYS.PROCEDURES WHERE NAME = 'usp_read_contact')
	DROP PROCEDURE dbo.usp_read_contact
GO

	CREATE PROCEDURE dbo.usp_read_contact
	AS 
	BEGIN
			SELECT * FROM Contacts
	END
GO
	
	