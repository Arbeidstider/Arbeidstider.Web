using System;
using System.Collections.Generic;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Parameters
{
    public class EmployeeParameters 
    {
        private readonly EmployeeDTO _dto;
        private readonly RepositoryAction _action;
        private readonly string _mobile;
        private readonly string _username;
        private readonly int _employeeGroupID;
        private readonly int _workplaceID;
        private readonly Guid _userID;

        public EmployeeParameters(EmployeeDTO dto, RepositoryAction action)
        {
            _action = action;
            _dto = dto;
            if (!string.IsNullOrEmpty(dto.Mobile)) _mobile = dto.Mobile;
            if (!string.IsNullOrEmpty(dto.Username)) _mobile = dto.Username;
            if (dto.Group != 0) _employeeGroupID = (int) dto.Group;
            if (dto.WorkplaceID != 0) _workplaceID = (int) dto.WorkplaceID;
            if (dto.UserID != Guid.Empty) _userID = dto.UserID;
            Create();
        }

        public void Create()
        {
            switch (_action)
            {
                case RepositoryAction.Update:
                {
                    Parameters = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@EmployeeID", _dto.EmployeeID),
                        new KeyValuePair<string, object>("@UserID", _dto.UserID),
                        new KeyValuePair<string, object>("@Firstname", _dto.UserID),
                        new KeyValuePair<string, object>("@Lastname", _dto.UserID),
                        new KeyValuePair<string, object>("@Mobile", _dto.UserID)
                    };
                    break;
                }
                case RepositoryAction.GetAll:
                {
                    Parameters = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@WorkplaceID", _dto.WorkplaceID),
                    };
                    break;
                }
                case RepositoryAction.Get:
                {
                    Parameters = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@Username", _dto.Username),
                    };
                    break;
                }
                case RepositoryAction.Create:
                {
                    Parameters = new List<KeyValuePair<string, object>>()
                    {
                        new KeyValuePair<string, object>("@Mobile", _mobile),
                        new KeyValuePair<string, object>("@Username", _username),
                        new KeyValuePair<string, object>("@EmployeeGroupID", _employeeGroupID),
                        new KeyValuePair<string, object>("@WorkplaceID", _workplaceID),
                        new KeyValuePair<string, object>("@UserID", _userID),
                    };

                    break;
                }
            }
        }

        public List<KeyValuePair<string, object>> Parameters { get; set; }

        public void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}