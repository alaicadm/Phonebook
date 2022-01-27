IF EXISTS (SELECT * FROM SYS.PROCEDURES WHERE NAME = 'usp_update_contact')
	DROP PROCEDURE dbo.usp_update_contact
GO

	CREATE PROCEDURE dbo.usp_update_contact
		@userId INT,
		@firstName NVARCHAR(100), 
		@middleName NVARCHAR(100), 
		@lastName NVARCHAR(100),
		@gender NVARCHAR(50), 
		@mobile NVARCHAR(50) 
	AS 
	BEGIN
			UPDATE Contacts
			SET firstName = @firstName,
				middleName = @middleName,
				lastName = @lastName,
				gender = @gender,
				mobile = @mobile
			WHERE userId = @userId;
	END
GO
	
	