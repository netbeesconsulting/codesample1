using System;

namespace Stocky.Common
{
    [Serializable]
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException() : base("UnAuthorized transaction")
        {
        }

        public UnAuthorizedException(string message) : base(message)
        {
        }
    }
    [Serializable]
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException() : base("Record Not Founded")
        {
        }

        public RecordNotFoundException(string message) : base(message)
        {
        }
    }
    [Serializable]
    public class NullObjectException : Exception
    {
        public NullObjectException() : base("null value passed")
        {
        }

        public NullObjectException(string message) : base(message)
        {
        }
    }
    [Serializable]
    public class InvaliedDomainCastException : Exception
    {
        public InvaliedDomainCastException() : base("invalied domain cast")
        {
        }

        public InvaliedDomainCastException(string message) : base(message)
        {
        }
    }
    [Serializable]
    public class EntityValidationException : Exception
    {
        public EntityValidationException() : base("validation faild")
        {
        }

        public EntityValidationException(string message) : base(message)
        {
        }

        public EntityValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    [Serializable]
    public class PrimaryKeyViolationException : Exception
    {
        public PrimaryKeyViolationException() : base("already exists")
        {
        }

        public PrimaryKeyViolationException(string message) : base(message)
        {
        }
    }
    [Serializable]
    public class ForeignKeyViolationException : Exception
    {
        public ForeignKeyViolationException() : base("FK exist")
        {
        }

        public ForeignKeyViolationException(string message) : base(message)
        {
        }
    }
    [Serializable]
    public class PermissionException : Exception
    {
        public PermissionException() : base("Access denied.please contact super admin")
        {
        }

        public PermissionException(string message) : base(message)
        {
        }
    }
    [Serializable]
    public class SameNameException : Exception
    {
        public SameNameException() : base("this name is already exist")
        {
        }

        public SameNameException(string message) : base(message)
        {
        }
    }
    [Serializable]
    public class InvalidProcessException : Exception
    {
        public InvalidProcessException() : base("cannot do this")
        {
        }

        public InvalidProcessException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class EatException : Exception
    {
        public EatException() : base("EatException")
        {
        }

        public EatException(Exception e) : base(e.Message)
        {
        }

        public EatException(string message) : base(message)
        {
        }
    }

    [Serializable]
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Forbidden transaction")
        {
        }

        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
