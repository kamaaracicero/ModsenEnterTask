CREATE TABLE [dbo].[EventChange]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[EventId] INT NOT NULL,
	[Date] DATETIME NOT NULL,
	[ParamName] NVARCHAR(100) NOT NULL,
	[OldValue] NVARCHAR(300) NULL,
	[NewValue] NVARCHAR(300) NULL,

	CONSTRAINT pk_EventChange PRIMARY KEY ([Id]),

	CONSTRAINT fk_EventChange_Event_Id FOREIGN KEY ([EventId])
		REFERENCES [dbo].[Event] ([Id])
		ON DELETE CASCADE
)
