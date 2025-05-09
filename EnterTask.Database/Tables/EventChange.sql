CREATE TABLE [dbo].[EventChange]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[EventId] INT NOT NULL,
	[Message] NVARCHAR(MAX) NOT NULL,

	CONSTRAINT pk_EventChange PRIMARY KEY ([Id]),

	CONSTRAINT fk_EventChange_Event_Id FOREIGN KEY ([Id])
		REFERENCES [dbo].[Event] ([Id])
		ON DELETE CASCADE
)
