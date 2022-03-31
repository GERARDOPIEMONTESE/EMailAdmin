using System;

namespace EMailAdmin.BackEnd.Exceptions
{
    public class NonEliminatedObjectException : EMailAdminException
    {
        public NonEliminatedObjectException()
        {
        }

        public NonEliminatedObjectException(string message)
            : base(message)
        {
        }

        public NonEliminatedObjectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
