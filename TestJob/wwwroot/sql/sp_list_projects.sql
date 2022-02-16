-- =============================================
-- Author:		admin
-- Create date: 29.01.2022
-- Description:	List for Project
-- =============================================
Create PROCEDURE [sp_list_projects] @id int, @res varchar(1000) out
AS
BEGIN
	SET NOCOUNT ON;

	declare @buf table(
		[key] int identity(1,1),
		id uniqueidentifier,
		projectName varchar(100),
		createDate datetime,
		updateDate datetime,
		[disabled] varchar(10) default '',
		lineThrough char(12) default ''
	);

	insert into @buf(id, projectName, updateDate,createDate)
	select Id, ProjectName, UpdateDate, CreateDate from Projects;
	
	update @buf set [disabled] = 'disabled' where [key] = @id;
	update @buf set lineThrough = 'lineThrough' where updateDate is not null;

	set @res = (select * from @buf for json path);

END
