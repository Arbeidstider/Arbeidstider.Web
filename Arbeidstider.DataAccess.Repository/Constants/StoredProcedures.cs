namespace Arbeidstider.DataAccess.Repository.Constants
{
    internal class StoredProcedures
    {
        internal static readonly string GET_ALL_TIMESHEETS = "GetAllTimesheets";
        internal static readonly string GET_ALL_EMPLOYEES = "GetAllEmployees";
        internal static readonly string GET_WEEKLY_TIMESHEETS = "GetWeeklyTimesheets";
        internal static readonly string GET_WORKPLACE_TIMESHEETS = "GetWorkplaceTimesheets";
        internal static readonly string CREATE_TIMESHEET = "CreateTimesheet";
        internal static readonly string EMPLOYEE_EXISTS = "EmployeeExists";
        internal static readonly string GET_EMPLOYEE = "GetEmployee";
        internal static readonly string GET_USERID = "GetUserID";
        internal static readonly string CREATE_EMPLOYEE = "CreateEmployee";
        internal static readonly string UPDATE_EMPLOYEE = "UpdateEmployee";
        internal static readonly string GET_TIMESHEET = "GetTimesheet";
        internal static readonly string UPDATE_TIMESHEET = "UpdateTimesheet";
        internal static readonly string UPDATE_EMPLOYEE_SCHEDULEVENT = "UpdateEmployeeSchduleEvent";
        internal static readonly string DELETE_TIMESHEET = "DeleteTimesheet";
    }
}