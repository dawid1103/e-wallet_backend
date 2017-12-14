using System;

namespace EwalletCommon.Utils
{
    public class DuplicateUsernameException : Exception { }

    public class ObjectAlreadyExists : Exception { }

    public class NotUniqueException : Exception { }

    public class NotFoundException : Exception { }

    public class BadRequestException : Exception { }
}
