using System.Collections.Generic;
using Arbeidstider.Business.Logic.Enums;
using Arbeidstider.Web.Framework.DTO;

namespace Arbeidstider.Web.Framework.Parameters
{
    public class EmployeeParameters 
    {
        private readonly EmployeeDTO _dto;
        private readonly RepositoryAction _action;

        public EmployeeParameters(EmployeeDTO dto, RepositoryAction action)
        {
            _action = action;
            _dto = dto;
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
                        new KeyValuePair<string, object>("@EmployeeID", _dto.EmployeeID),
                        new KeyValuePair<string, object>("@Mobile", _dto.Mobile),
                        new KeyValuePair<string, object>("@Username", _dto.Username),
                        new KeyValuePair<string, object>("@EmployeeGroupID", (int)(EmployeeGroup) _dto.Group),
                        new KeyValuePair<string, object>("@WorkplaceID", _dto.WorkplaceID),
                        new KeyValuePair<string, object>("@UserID", _dto.UserID),

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