using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMailAdmin.BackEnd.DTO
{
    public class FiltersAttachsDTO
    {
        public string AttachName { get; set; }
        public string GroupAttachName { get; set; }
        public Nullable<bool> attachMerge { get; set; }

        public FiltersAttachsDTO()
        {
            AttachName = "";
            GroupAttachName = "";
            attachMerge = false;
        }
    }
}
