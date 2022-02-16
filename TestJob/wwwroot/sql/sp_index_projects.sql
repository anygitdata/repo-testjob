-- =============================================
-- Author:		admin
-- Create date: 30.01.2022
-- Description:	numKey for projectMenu in client
-- =============================================
CREATE PROCEDURE [sp_index_projects] @id char(36), @res int out
AS
BEGIN
	SET NOCOUNT ON;

	declare @buf table(
		[key] int identity(1,1),
		id uniqueidentifier		
	);

	insert into @buf(id)
	select Id from Projects;
	
	set @res = (select [key] from @buf where id = @id);

END
