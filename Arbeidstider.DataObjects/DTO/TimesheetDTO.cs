﻿using System.Runtime.Serialization;
using Arbeidstider.DataInterfaces;
using Arbeidstider.DataObjects.DAO;

namespace Arbeidstider.DataObjects.DTO
{
    [DataContract]
    public class TimesheetDTO
    {
        public TimesheetDTO(ITimesheet domain)
        {
            EmployeeId = domain.EmployeeId;
            ShiftDate = domain.ShiftDate.ToString();
            ShiftEnd = domain.ShiftEnd.ToString();
            ShiftStart = domain.ShiftStart.ToString();
            Id = domain.Id;
        }

        [DataMember(Order = 1)]
        public int Id { get; set; }
        [DataMember(Order = 2)]
        public int EmployeeId { get; private set; }
        [DataMember(Order = 3)]
        public string ShiftDate { get; set; }
        [DataMember(Order = 4)]
        public string ShiftStart { get; private set; }
        [DataMember(Order = 5)]
        public string ShiftEnd { get; private set; }

        [DataMember(Order = 6)]
        public bool IsTiny { get; set; }
    }
}