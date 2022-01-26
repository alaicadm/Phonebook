IF EXISTS (SELECT * FROM SYS.PROCEDURES WHERE NAME = 'usp_add_contact')
	DROP PROCEDURE dbo.usp_add_contact
GO
	CREATE PROCEDURE dbo.usp_add_contact
		@firstName NVARCHAR(100) = NULL,
		@middleName NVARCHAR(100) = NULL,
		@lastName NVARCHAR(100) = NULL,
		@gender NVARCHAR(50) = NULL,
		@mobile NVARCHAR(50) = NULL
	AS 
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
GO