﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO;

namespace EMailAdmin.BackEnd.Data.Interfaces
{
    public interface IDAOTemplateDTO
    {
        IList<TemplateDTO> Find(int idTemplateType, string name);
    }
}
