USE [AdventureWorks2019]
GO
/****** Object:  StoredProcedure [dbo].[TablesConnection]    Script Date: 18.12.2020 19:19:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[TablesConnection] AS
BEGIN
	SELECT Person.Person.FirstName FROM Person.BusinessEntity
	,Person.BusinessEntityAddress
	,Person.Password
	,Person.EmailAddress
	,Person.Person

	WHERE Person.BusinessEntity.BusinessEntityID=1
	AND  Person.BusinessEntity.BusinessEntityID=Person.BusinessEntityAddress.BusinessEntityID
	AND Person.BusinessEntity.BusinessEntityID=Person.Password.BusinessEntityID
	AND Person.BusinessEntity.BusinessEntityID=Person.EmailAddress.BusinessEntityID
	AND Person.BusinessEntity.BusinessEntityID=Person.Person.BusinessEntityID

END;

EXECUTE TablesConnection