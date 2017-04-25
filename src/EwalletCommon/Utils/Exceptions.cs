using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EwalletCommon.Utils
{
    public class DuplicateUsernameException : Exception { }

    public class ObjectAlreadyExists : Exception { }

    public class NotUniqueException : Exception { }

    public class NotFoundException : Exception { }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }
    }
}
