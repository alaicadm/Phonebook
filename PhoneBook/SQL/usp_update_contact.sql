IF EXISTS (SELECT * FROM SYS.PROCEDURES WHERE NAME = 'usp_update_contact')
	DROP PROCEDURE dbo.usp_update_contact
GO

	CREATE PROCEDURE dbo.usp_update_contact
	AS 
	BEGIN
			UPDATE Contacts
			SET firstName = 'firstName',
				middleName = 'middleName',
				lastName = 'lastName',
				gender = 'gender',
				mobile = 'mobile'
			WHERE userId = 'userId';
	END
GO
	
	