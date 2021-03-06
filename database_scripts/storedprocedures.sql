USE [FamousFolks]
GO
/****** Object:  StoredProcedure [dbo].[GetInformationSchemaTables]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInformationSchemaTables]
	@TableName [nvarchar](255) = null
AS


  SELECT t.*
	from Information_Schema.Tables t
where (t.Table_Name = @TableName or @TableName is null)
and t.Table_Name <> 'dtproperties'
and t.Table_Name <> 'sysdiagrams'
GO
/****** Object:  StoredProcedure [dbo].[GetInformationSchemaTableConstraints]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInformationSchemaTableConstraints]
	@TableName [nvarchar](255) = null
AS



   select tc.*
	from Information_Schema.Tables t
	inner join Information_Schema.Table_Constraints tc
	on t.Table_Name = tc.Table_Name
	where (t.Table_Name = @TableName or @TableName is null)
	and t.Table_Type IN ('BASE TABLE') 
	and t.Table_Name <> 'dtproperties'
    and t.Table_Name <> 'sysdiagrams'

	Order by t.Table_Name
GO
/****** Object:  StoredProcedure [dbo].[GetInformationSchemaColumnUsage]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInformationSchemaColumnUsage]
	@TableName [nvarchar](255) = null
AS
select ccu.*
from Information_Schema.Tables t
inner join Information_Schema.Constraint_Column_Usage ccu
on ccu.Table_Name = t.Table_Name
where (t.Table_Name = @TableName or @TableName is null)
	and t.Table_Type IN ('BASE TABLE') 
	and t.Table_Name <> 'dtproperties'
    and t.Table_Name <> 'sysdiagrams'
GO
/****** Object:  StoredProcedure [dbo].[GetInformationSchemaColumns]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInformationSchemaColumns]
	@TableName [nvarchar](255) = null
AS


  
	SELECT c.*
	FROM Information_Schema.Tables t
	inner join Information_Schema.Columns c
	on c.Table_Name = t.Table_Name
	--WHERE t.TABLE_TYPE IN ('BASE TABLE', 'VIEW') 
	WHERE (t.Table_Name = @TableName or @TableName is null)
	and t.TABLE_TYPE IN ('BASE TABLE') 
	and t.Table_Name <> 'dtproperties'
    and t.Table_Name <> 'sysdiagrams'

	ORDER BY t.TABLE_NAME
GO
/****** Object:  StoredProcedure [dbo].[IsIdentity]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[IsIdentity]
	@TableName [nvarchar](255) = null,
	@ColumnName [nvarchar](255) = null,
	@IsIdentity [bit] = 0 OUTPUT
AS
if exists(select *
		from information_schema.columns 
		where 
		table_schema = 'dbo' 
		and columnproperty(object_id(@TableName), @ColumnName,'IsIdentity') = 1 
		)
			set @IsIdentity = 1
else
			set @IsIdentity = 0
GO
/****** Object:  StoredProcedure [dbo].[UpdateFolksByPrimaryKey]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[UpdateFolksByPrimaryKey]
	@ID [int],
	@FirstName [varchar](30),
	@LastName [varchar](30),
	@BirthLocation [varchar](80),
	@Bio [varchar](8000)
AS
Update Folks 
Set FirstName = @FirstName, 
LastName = @LastName, 
BirthLocation = @BirthLocation, 
Bio = @Bio 
Where  ID = @ID
GO
/****** Object:  StoredProcedure [dbo].[InsertFolks]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[InsertFolks]
	@ID [int] OUTPUT,
	@FirstName [varchar](30),
	@LastName [varchar](30),
	@BirthLocation [varchar](80),
	@Bio [varchar](8000)
AS
Insert Into Folks 
( FirstName, LastName, BirthLocation, Bio)
Values ( @FirstName, @LastName, @BirthLocation, @Bio) 
Set @ID = IDENT_CURRENT('Folks')
GO
/****** Object:  StoredProcedure [dbo].[GetInformationSchema]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetInformationSchema]
	@TableName [nvarchar](255) = null
AS


if (@TableName is null)
	begin
		Exec GetInformationSchemaTables
		Exec GetInformationSchemaTableConstraints
		Exec GetInformationSchemaColumns
		Exec GetInformationSchemaColumnUsage
   end

if(@TableName is not null)
	begin
		Exec GetInformationSchemaTables @TableName
		Exec GetInformationSchemaTableConstraints @TableName
		Exec GetInformationSchemaColumns @TableName
		Exec GetInformationSchemaColumnUsage @TableName
    end
GO
/****** Object:  StoredProcedure [dbo].[GetFolksByPrimaryKey]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetFolksByPrimaryKey]
	@ID [int]
AS
Select ID, FirstName, LastName, BirthLocation, Bio
From Folks 
Where  ID = @ID
GO
/****** Object:  StoredProcedure [dbo].[GetFolksByCriteriaFuzzy]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetFolksByCriteriaFuzzy]
	@ID [int] = null,
	@FirstName [varchar](30) = null,
	@LastName [varchar](30) = null,
	@BirthLocation [varchar](80) = null,
	@Bio [varchar](8000) = null
AS
Select ID, FirstName, LastName, BirthLocation, Bio
From Folks 
Where ( ID = @ID Or @ID = null ) 
And ( FirstName Like @FirstName + '%' Or @FirstName = null ) 
And ( LastName Like @LastName + '%' Or @LastName = null ) 
And ( BirthLocation Like @BirthLocation + '%' Or @BirthLocation = null ) 
And ( Bio Like @Bio + '%' Or @Bio = null )
GO
/****** Object:  StoredProcedure [dbo].[GetFolksByCriteriaExact]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetFolksByCriteriaExact]
	@ID [int] = null,
	@FirstName [varchar](30) = null,
	@LastName [varchar](30) = null,
	@BirthLocation [varchar](80) = null,
	@Bio [varchar](8000) = null
AS
Select ID, FirstName, LastName, BirthLocation, Bio
From Folks 
Where ( ID = @ID Or @ID = null ) 
And ( FirstName = @FirstName Or @FirstName = null ) 
And ( LastName = @LastName Or @LastName = null ) 
And ( BirthLocation = @BirthLocation Or @BirthLocation = null ) 
And ( Bio = @Bio Or @Bio = null )
GO
/****** Object:  StoredProcedure [dbo].[GetFolks]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[GetFolks]
AS
Select ID, FirstName, LastName, BirthLocation, Bio
From Folks
GO
/****** Object:  StoredProcedure [dbo].[DeleteFolksByPrimaryKey]    Script Date: 01/15/2012 16:42:16 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[DeleteFolksByPrimaryKey]
	@ID [int]
AS
Delete From Folks 
Where  ID = @ID
GO
