using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA_VEA.Core.Tools.OperationResult
{
    public static class ErrorCodes
    {
        public const int GeneralError = 1001;
        public const int SpecificError = 1002;
        public const int ValidationFailed = 1003;
        public const int NotFound = 404;
        public const int Unauthorized = 401;
        public const int Forbidden = 403;
        public const int InternalServerError = 500;
    }
}
