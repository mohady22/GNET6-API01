using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Common
{
    public record Error(string code,string Description,ErrorType Type = ErrorType.Failure)
    {
        public static Error Failure(string code = "General.Failure",string Description = "General Failure Desc")
            => new Error(code,Description,ErrorType.Failure);
        public static Error Validation(string code = "General.Validation", string Description = "General Validation Desc")
            => new Error(code, Description, ErrorType.Failure);

        public static Error NotFound(string code = "General.NotFound", string Description = "General NotFound Desc")
            => new Error(code, Description, ErrorType.Failure);

        public static Error Conflict(string code = "General.Conflict", string Description = "General Conflict Desc")
            => new Error(code, Description, ErrorType.Failure);
        public static Error UnAuthorized(string code = "General.UnAuthorized", string Description = "General UnAuthorized Desc")
            => new Error(code, Description, ErrorType.Failure);
        public static Error Forbidden(string code = "General.Forbidden", string Description = "General Forbidden Desc")
            => new Error(code, Description, ErrorType.Failure);
        public static Error InvalidCredentails(string code = "General.InvalidCredentails", string Description = "General Invalid Credentails Desc")
            => new Error(code, Description, ErrorType.Failure);




    }

}   


