using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMailAdmin.BackEnd.DTO.EkitBenefits;

namespace EMailAdmin.ExternalServices.Service.Interface
{
    public interface IExternalCondicionesService
    {
        DocumentoDTO[] GetDocumentsInformation(int countryCode, string capitaCode, string planCode);
    }
}
