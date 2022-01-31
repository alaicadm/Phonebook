IF EXISTS (SELECT * FROM SYS.PROCEDURES WHERE NAME = 'usp_add_contact')
	DROP PROCEDURE dbo.usp_add_contact
GO
	CREATE PROCEDURE dbo.usp_add_contact
		@firstName NVARCHAR(100),
		@middleName NVARCHAR(100),
		@lastName NVARCHAR(100),
		@gender NVARCHAR(50),
		@mobile NVARCHAR(50)
	AS
	BEGIN
		IF NOT EXISTS (SELECT * FROM Contacts WHERE firstName = @firstName and 
		middleName = @middleName and lastName = @lastName and gender = @gender and mobile = @mobile)
			BEGIN
				SET NOCOUNT ON
				INSERT INTO dbo.Contacts 
				(
					firstName,
					middleNAme,
					lastName,
					gender,
					mobile
				)
				VALUES 
				(
					@firstName,
					@middleName,
					@lastName,
					@gender,
					@mobile
				)
			END
	END
GO