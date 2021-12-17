CREATE DATABASE Logging;

GO

USE Logging;

GO

CREATE SCHEMA [Cg];

GO

CREATE TABLE [Cg].[Log] (
	[LogId] [uniqueidentifier] NOT NULL,
	[HostName] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](400) NOT NULL,
	[Level] [nvarchar](20) NOT NULL,
	[EventId] [nvarchar](400) NULL,
	[ActivityId] [nvarchar](36) NULL,
	[UserId] [nvarchar](36) NULL,
	[LoginName] [nvarchar](100) NULL,
	[ActionId] [nvarchar](36) NULL,
	[ActionName] [nvarchar](400) NULL,
	[RequestId] [nvarchar](36) NULL,
	[RequestPath] [nvarchar](400) NULL,
	[Sql] [nvarchar](4000) NULL,
	[Parameters] [nvarchar](4000) NULL,
	[StateText] [nvarchar](200) NULL,
	[StateProperties] [nvarchar](4000) NULL,
	[ScopeText] [nvarchar](200) NULL,
	[ScopeProperties] [nvarchar](4000) NULL,
	[Text] [nvarchar](max) NULL,
	[Exception] [nvarchar](max) NULL,
	[FilePath] [nvarchar](4000) NULL,
	[Time] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
	(
		[LogId] ASC
	) ON [PRIMARY]
) ON [PRIMARY]

GO