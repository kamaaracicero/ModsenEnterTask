CREATE TABLE [dbo].[Event]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(300) NOT NULL,
	[Description] NVARCHAR(2000) NOT NULL,
	[Start] DATETIME NOT NULL,
	[Place] NVARCHAR(150) NOT NULL,
	[Category] NVARCHAR(100) NOT NULL,
	[MaxPeopleCount] INT NOT NULL,

	CONSTRAINT pk_Event PRIMARY KEY ([Id]),

	CONSTRAINT ck_Event_MaxPeopleCount_Positive CHECK ([MaxPeopleCount] >= 0)
)
