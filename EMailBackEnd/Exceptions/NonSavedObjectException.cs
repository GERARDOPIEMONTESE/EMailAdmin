using System;

namespace EMailAdmin.BackEnd.Exceptions
{
    public class NonSavedObjectException : EMailAdminException
    {
        public NonSavedObjectException()
        {
        }

        public NonSavedObjectException(string message)
            : base(message)
        {
        }

        public NonSavedObjectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}