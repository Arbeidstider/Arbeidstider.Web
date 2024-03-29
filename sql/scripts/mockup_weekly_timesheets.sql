USE [Arbeidstider]
GO

DECLARE @Startdate date
DECLARE @Enddate date
SELECT @Startdate = N'2014-02-17'
SELECT @Enddate = DATEADD(day,6,@Startdate) 

WHILE (@Startdate != @Enddate) BEGIN
	EXEC	[dbo].[CreateTimesheet]
			@EmployeeId = 1,
			@ShiftDate = @Startdate,
			@ShiftStart = N'15:00:00',
			@ShiftEnd = N'23:00:00'
			
	SELECT @Startdate = DATEADD(day,1,@Startdate)
END 
