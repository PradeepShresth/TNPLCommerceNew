USE [master]
GO
/****** Object:  Database [TNPLCommerce]    Script Date: 3/8/2021 6:46:36 PM ******/
CREATE DATABASE [TNPLCommerce]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TNPLCommerce', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TNPLCommerce.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TNPLCommerce_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\TNPLCommerce_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [TNPLCommerce] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TNPLCommerce].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TNPLCommerce] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TNPLCommerce] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TNPLCommerce] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TNPLCommerce] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TNPLCommerce] SET ARITHABORT OFF 
GO
ALTER DATABASE [TNPLCommerce] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TNPLCommerce] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TNPLCommerce] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TNPLCommerce] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TNPLCommerce] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TNPLCommerce] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TNPLCommerce] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TNPLCommerce] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TNPLCommerce] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TNPLCommerce] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TNPLCommerce] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TNPLCommerce] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TNPLCommerce] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TNPLCommerce] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TNPLCommerce] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TNPLCommerce] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TNPLCommerce] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TNPLCommerce] SET RECOVERY FULL 
GO
ALTER DATABASE [TNPLCommerce] SET  MULTI_USER 
GO
ALTER DATABASE [TNPLCommerce] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TNPLCommerce] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TNPLCommerce] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TNPLCommerce] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TNPLCommerce] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TNPLCommerce] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'TNPLCommerce', N'ON'
GO
ALTER DATABASE [TNPLCommerce] SET QUERY_STORE = OFF
GO
USE [TNPLCommerce]
GO
/****** Object:  Table [dbo].[Register]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Register](
	[Id] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Register] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[UserRole] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [nvarchar](50) NOT NULL,
	[UserRole] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckIfAdmin]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CheckIfAdmin]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50), 
	@IsValid BIT OUT
	
AS
BEGIN
SET @IsValid=(SELECT COUNT(1) FROM Register WHERE Email=@Email AND Role='Admin')

END
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckIfAlreadyExists]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CheckIfAlreadyExists]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50), 
	@IsValid BIT OUT
	
AS
BEGIN
SET @IsValid=(SELECT COUNT(1) FROM Register WHERE Email=@Email)

END
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckUserExist]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_CheckUserExist]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50), 
	@Password nvarchar(50),
	@Role nvarchar(50),
	@IsValid BIT OUT
	
AS
BEGIN
SET @IsValid=(SELECT COUNT(1) FROM Register WHERE Email=@Email AND Password=@Password AND Role=@Role)

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GettingUserRoles]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[usp_GettingUserRoles]
(
	@SPType varchar(20),
	@Email nvarchar(50) = NULL,
	@Password nvarchar(50) = NULL
	--@Role Nvarchar(50) = NULL
)-- Add the parameters for the stored procedure here
AS
BEGIN
	SET NOCOUNT ON;
	if @SPType='login'
	begin 
		if exists (select '1' from Register(nolock) r where r.Email=@Email and r.Password=@Password)
		begin
			select Role from Register (nolock) R where R.Email=@Email
			return
		end
		else
		begin
			select Id from Register (nolock)
			return 
		end
	end
END

	--if @SPType='checkRole'
	--begin
	--	if exists (select 'a' from Register(nolock) R where R.Email=@Email and R.Role=@Role)
	--	Begin
	--		select 1
	--		return
	--	end
	--	else
	--		select 0
	--		return
	--end
	--if @SPType='select'
	--begin 
	--	return
	--end

	--if @SPType='Add'
	--begin
	--	select 'Inserted' ASS
	--	return
	--end
	--select 1
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUser]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUser]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50), 
	@Password nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserId]
		,[Username]
      ,[Email]
      ,[Password]
	  ,[UserRole]
	FROM [dbo].[User]
	WHERE [Email] = @Email AND [Password] = @Password
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserByEmail]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUserByEmail] 
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserId]
		,[Username]
      ,[Email]
      ,[Password]
	  ,[UserRole]
	FROM [dbo].[User]
	WHERE [Email] = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetUserById]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_GetUserById]
	-- Add the parameters for the stored procedure here
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [UserId]
		,[Username]
      ,[Email]
      ,[Password]
	  ,[UserRole]
	FROM [dbo].[User]
	WHERE [UserId] = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[usp_Register]    Script Date: 3/8/2021 6:46:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[usp_Register] 
	-- Add the parameters for the stored procedure here
	@Email nvarchar(50), 
	@Username nvarchar(50),
	@Password nvarchar(50),
	@UserRole nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[User]
           ([Username]
		   ,[Email]
           ,[Password]
		   ,[UserRole])
    VALUES
        (@Username
		,@Email
        ,@Password
		,@UserRole)
END
GO
USE [master]
GO
ALTER DATABASE [TNPLCommerce] SET  READ_WRITE 
GO
