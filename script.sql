USE [Youtube]
GO
/****** Object:  Table [dbo].[History]    Script Date: 10/22/2019 00:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[searchHistory] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Table_1_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[phoneNumber] [nvarchar](50) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[permissions] [bit] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoData]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[videoUrl] [nvarchar](max) NOT NULL,
	[duration] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Table_1_2] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_History_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_History_User]
GO
/****** Object:  StoredProcedure [dbo].[GetAllUsers]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

INSERT INTO [dbo].[User]
           ([name]
           ,[lastName]
           ,[phoneNumber]
           ,[userName]
           ,[password]
           ,[permissions])
     VALUES
           ('Admin'
           ,'Admin'
           ,'0507945245'
           ,'admin'
           ,'admin'
           ,'TRUE')
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllUsers]
	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	From [dbo].[User] 


END
GO
/****** Object:  StoredProcedure [dbo].[GetHistory]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetHistory]
	-- Add the parameters for the stored procedure here
	@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select*
	from [dbo].[History]
	where  userId=@userId
END
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[GetUser]
	@password nvarchar(50),
	@userName nvarchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
  

	Select *
	From [dbo].[User]
	Where userName = @userName and password=@password



END
GO
/****** Object:  StoredProcedure [dbo].[GetVideoData]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetVideoData]
	@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
	from [dbo].[VideoData]
	Where userId=@userId
END
GO
/****** Object:  StoredProcedure [dbo].[InsertHistory]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertHistory]
	-- Add the parameters for the stored procedure here
	@userId int,
	@searchHistory nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert INTO [dbo].[History]
    VALUES (@userId, @searchHistory);
	
END
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertUser]
	@name nvarchar(50),
	@lastName nvarchar(50),
	@phoneNumber nvarchar(50),
	@userName nvarchar(50),
	@password nvarchar(50),
	@isSucsess int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @existingId int=0;

	Select @existingId = Id 
	From [dbo].[User]
	Where userName=@userName

	if @existingId>0
	begin  
	select @isSucsess=0
	return
	end


    -- Insert statements for procedure here
	Insert INTO [dbo].[User] 
    VALUES (@name, @lastName, @phoneNumber,@userName, @password );
	select @isSucsess=1
END
GO
/****** Object:  StoredProcedure [dbo].[InsertVideoDetails]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertVideoDetails]
	-- Add the parameters for the stored procedure here
	@userId int,
	@videoUrl nvarchar(max)
	--@duration time(7)=NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert INTO [dbo].[VideoData]
    VALUES (@userId, @videoUrl,NULL);
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateVideoWatchDuration]    Script Date: 10/22/2019 00:33:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateVideoWatchDuration]
	-- Add the parameters for the stored procedure here
	@userId int,
	@videoUrl nvarchar(max),
	@duration int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE [dbo].[VideoData]
   SET duration = @duration
   WHERE userId=@userId and videoUrl=@videoUrl;
END
GO
