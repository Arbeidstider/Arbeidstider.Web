using System;

namespace Arbeidstider.Web.Framework.ViewModels.Error
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public Exception Exception { get; set; }
    }
}
