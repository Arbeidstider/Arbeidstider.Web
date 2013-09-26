﻿using System;
using Arbeidstider.Business.Interfaces.Domain;
using Arbeidstider.Business.Logic.Domain;

namespace Arbeidstider.Business.Domain
{
    public class Timesheet : ITimesheet
    {
        public int EmployeeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SelectedDay { get; set; }
        public Employee ShiftWorker { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan ShiftStart { get; set; }
        public TimeSpan ShiftEnd { get; set; }
    }
}