using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMailAdmin.BackEnd.Exceptions
{
    public class EMailAdminException : Exception
    {
        public EMailAdminException()
        {
        }

        public EMailAdminException(string message)
            : base(message)
        {
        }

        public EMailAdminException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
