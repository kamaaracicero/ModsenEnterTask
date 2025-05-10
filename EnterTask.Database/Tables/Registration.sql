CREATE TABLE [dbo].[Registration]
(
	[ParticipantId] INT NOT NULL,
	[EventId] INT NOT NULL,
	[Date] DATE NOT NULL,

	CONSTRAINT pk_Registration PRIMARY KEY ([ParticipantId], [EventId]),
	CONSTRAINT un_Registration UNIQUE ([ParticipantId], [EventId]),

	CONSTRAINT fk_Registration_Participant_Id FOREIGN KEY ([ParticipantId])
		REFERENCES [dbo].[Participant]([Id])
		ON DELETE CASCADE,

	CONSTRAINT fk_Registration_Event_Id FOREIGN KEY ([EventId])
		REFERENCES [dbo].[Event]([Id])
		ON DELETE CASCADE
)
