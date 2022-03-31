using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Exceptions
{
    public class NonValidValueException : EMailAdminException
    {
        public NonValidValueException()
        {
        }

        public NonValidValueException(string message)
            : base(message)
        {
        }

        public NonValidValueException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
