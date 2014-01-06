﻿using Arbeidstider.Interfaces;
using Arbeidstider.Web.Framework.Session;

namespace Arbeidstider.Web.Framework.DTO
{
    public class EmployeeDTO
    {
        public EmployeeDTO(EmployeeSession currentUser)
        {
        }
        public EmployeeDTO(IEmployee domain)
        {
            Username = domain.Username;
        }
        public string Username { get; set; }
    }
}