using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
using CapaNegocioDatos.CapaDatos;
using EMailAdmin.BackEnd.Data.Interfaces;
using EMailAdmin.BackEnd.Domain;
using FrameworkDAC.Dato;
using FrameworkDAC.Negocio;
using FrameworkDAC.Parametro;

namespace EMailAdmin.BackEnd.Data
{
    public class DAOContent : DAOObjetoNegocio<Content>, IDAOContent
    {
        #region IDAOContent Members

        public IList<Content> Find(int idTemplate)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplate", idTemplate);

            return Buscar(new Filtro(parameters, "dbo.Content_Tx_IdTemplate"));
        }

        public Content Get(int id)
        {
            return Obtener(id);
        }

        public void DeleteAll(int idTemplate, TransactionScope ts)
        {
            var parameters = new Parametros();
            parameters.AgregarParametro("IdTemplate", idTemplate);

            Ejecutar(new Filtro(parameters, "dbo.Content_E_All"), ts);
        }

        #endregion

        protected override string NombreConnectionString()
        {
            return "EMailAdmin";
        }

        protected override Parametros ParametrosCrear(Content objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("Subject", objetoNegocio.Subject);
            parameters.AgregarParametro("Body", objetoNegocio.Body);
            parameters.AgregarParametro("IdHeader", objetoNegocio.Header != null ? objetoNegocio.Header.Id : -1);
            parameters.AgregarParametro("IdFooter", objetoNegocio.Footer != null ? objetoNegocio.Footer.Id : -1);
            parameters.AgregarParametro("IdHeaderPDF", objetoNegocio.HeaderPDF != null ? objetoNegocio.HeaderPDF.Id : -1);
            parameters.AgregarParametro("IdFooterPDF", objetoNegocio.FooterPDF != null ? objetoNegocio.FooterPDF.Id : -1);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);
            parameters.AgregarParametro("Color", objetoNegocio.Color);
            parameters.AgregarParametro("EVoucherName", objetoNegocio.EVoucherName);

            return parameters;
        }

        protected override Parametros ParametrosModificar(Content objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("Subject", objetoNegocio.Subject);
            parameters.AgregarParametro("Body", objetoNegocio.Body);
            parameters.AgregarParametro("IdHeader", objetoNegocio.Header != null ? objetoNegocio.Header.Id : -1);
            parameters.AgregarParametro("IdFooter", objetoNegocio.Footer != null ? objetoNegocio.Footer.Id : -1);
            parameters.AgregarParametro("IdHeaderPDF", objetoNegocio.HeaderPDF != null ? objetoNegocio.HeaderPDF.Id : -1);
            parameters.AgregarParametro("IdFooterPDF", objetoNegocio.FooterPDF != null ? objetoNegocio.FooterPDF.Id : -1);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);
            parameters.AgregarParametro("Color", objetoNegocio.Color);
            parameters.AgregarParametro("EVoucherName", objetoNegocio.EVoucherName);

            return parameters;
        }

        protected override Parametros ParametrosEliminar(Content objetoNegocio)
        {
            objetoNegocio.IdEstado = objetoNegocio.ObtenerEliminado();
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);

            return parameters;
        }

        protected override Parametros ParametrosGrabarLog(Content objetoNegocio)
        {
            var parameters = new Parametros();

            parameters.AgregarParametro("IdContent", objetoNegocio.Id);
            parameters.AgregarParametro("IdTemplate", objetoNegocio.IdTemplate);
            parameters.AgregarParametro("IdLanguage", objetoNegocio.Language.Id);
            parameters.AgregarParametro("Subject", objetoNegocio.Subject);
            parameters.AgregarParametro("Body", objetoNegocio.Body);
            parameters.AgregarParametro("IdHeader", objetoNegocio.Header != null ? objetoNegocio.Header.Id : -1);
            parameters.AgregarParametro("IdFooter", objetoNegocio.Footer != null ? objetoNegocio.Footer.Id : -1);
            parameters.AgregarParametro("IdHeaderPDF", objetoNegocio.HeaderPDF != null ? objetoNegocio.HeaderPDF.Id : -1);
            parameters.AgregarParametro("IdFooterPDF", objetoNegocio.FooterPDF != null ? objetoNegocio.FooterPDF.Id : -1);
            parameters.AgregarParametro("IdUser", objetoNegocio.IdUsuario);
            parameters.AgregarParametro("IdStatus", objetoNegocio.IdEstado);
            parameters.AgregarParametro("Color", objetoNegocio.Color);
            parameters.AgregarParametro("EVoucherName", objetoNegocio.EVoucherName);

            return parameters;
        }

        protected override void Completar(Content objetoPersistido, SqlDataReader dr)
        {
            objetoPersistido.Id = Convert.ToInt32(dr["IdContent"]);
            objetoPersistido.IdTemplate = Convert.ToInt32(dr["IdTemplate"]);
            objetoPersistido.Language = DAOIdioma.Instancia().Obtener(
                Convert.ToInt32(dr["IdLanguage"]));
            objetoPersistido.Subject = dr["Subject"].ToString();
            objetoPersistido.Body = dr["Body"].ToString();
            objetoPersistido.IdHeader = Convert.ToInt32(dr["IdHeader"]);
            objetoPersistido.IdFooter = Convert.ToInt32(dr["IdFooter"]);
            objetoPersistido.IdHeaderPDF = Convert.ToInt32(dr["IdHeaderPDF"]);
            objetoPersistido.IdFooterPDF = Convert.ToInt32(dr["IdFooterPDF"]);
            objetoPersistido.IdUsuario = Convert.ToInt32(dr["IdUser"]);
            objetoPersistido.IdEstado = Convert.ToInt32(dr["IdStatus"]);
            objetoPersistido.Color = dr["Color"].ToString();
            objetoPersistido.EVoucherName = dr["EVoucherName"].ToString();
        }

        protected override void ModificarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            CrearComposicion(objetoNegocio, ts);
        }

        protected override void CrearComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var content = (Content) objetoNegocio;

            #region Images

            int count = 0;
            //DAOLocator.Instance().GetDaoContent_R_Image().DeleteByIdContent(content.Id, content.IdUsuario);
            foreach (ContentImage contentImage in content.Images)
            {
                count++;
                var rContentImage = new Content_R_ContentImage
                                        {
                                            ContentImage = contentImage,
                                            Order = count,
                                            IdContent = content.Id,
                                            IdUsuario = content.IdUsuario,
                                            IdEstado = ObjetoNegocio.Creado()
                                        };
                DAOLocator.Instance().GetDaoContent_R_Image().Persistir(rContentImage, ts);
            }

            #endregion Images

            #region Link

            count = 0;
            //DAOLocator.Instance().GetDaoContent_R_ContentLink().DeleteByIdContent(content.Id, content.IdUsuario);
            foreach (Link link in content.Links)
            {
                count++;
                var rContentLink = new Content_R_ContentLink
                                       {
                                           Link = link,
                                           Order = count,
                                           IdContent = content.Id,
                                           IdUsuario = content.IdUsuario,
                                           IdEstado = ObjetoNegocio.Creado()
                                       };
                DAOLocator.Instance().GetDaoContent_R_ContentLink().Persistir(rContentLink, ts);
            }

            #endregion Link

            #region Variable Text

            //DAOLocator.Instance().GetDaoContent_R_ContentVariableText().DeleteByIdContent(content.Id, content.IdUsuario);
            foreach (VariableText variableText in content.VarableTexts)
            {
                count++;
                var rContentVariableText = new Content_R_ContentVariableText
                                               {
                                                   VariableText = variableText,
                                                   IdContent = content.Id,
                                                   IdUsuario = content.IdUsuario,
                                                   IdEstado = ObjetoNegocio.Creado()
                                               };
                DAOLocator.Instance().GetDaoContent_R_ContentVariableText().Persistir(rContentVariableText, ts);
            }

            #endregion Variable Text

            #region Signature Type

            //DAOLocator.Instance().GetDaoContent_R_ContentSignatureType().DeleteByIdContent(content.Id, content.IdUsuario);
            foreach (SignatureType signatureType in content.Signatures)
            {
                count++;
                var contentSignatureType = new Content_R_ContentSignatureType
                                               {
                                                   SignatureType = signatureType,
                                                   IdContent = content.Id,
                                                   IdUsuario = content.IdUsuario,
                                                   IdEstado = ObjetoNegocio.Creado()
                                               };
                DAOLocator.Instance().GetDaoContent_R_ContentSignatureType().Persistir(contentSignatureType, ts);
            }

            #endregion Signature Type

            #region Email Contact Type

            //DAOLocator.Instance().GetDaoContent_R_ContentEmailContactType().DeleteByIdContent(content.Id, content.IdUsuario);
            foreach (EMailContactType eMailContactType in content.Contacts)
            {
                count++;
                var contentSignatureType = new Content_R_ContentEmailContactType
                                               {
                                                   EMailContactType = eMailContactType,
                                                   IdContent = content.Id,
                                                   IdUsuario = content.IdUsuario,
                                                   IdEstado = ObjetoNegocio.Creado()
                                               };
                DAOLocator.Instance().GetDaoContent_R_ContentEmailContactType().Persistir(contentSignatureType, ts);
            }

            #endregion Email Contact Type

            #region Country Variable Text Type

            //DAOLocator.Instance().GetDaoContent_R_ContentCountryVisibleTextType().DeleteByIdContent(content.Id, content.IdUsuario);
            foreach (var countryVisibleTextType in content.CountryVisibleTexts)
            {
                count++;
                var contentSignatureType = new Content_R_ContentCountryVisibleTextType
                {
                    CountryVisibleTextType = countryVisibleTextType,
                    IdContent = content.Id,
                    IdUsuario = content.IdUsuario,
                    IdEstado = ObjetoNegocio.Creado()
                };
                DAOLocator.Instance().GetDaoContent_R_ContentCountryVisibleTextType().Persistir(contentSignatureType, ts);
            }

            #endregion Country Variable Text Type

            #region Upgrade Variable Text Type

            //DAOLocator.Instance().GetDaoContent_R_ContentUpgradeVariableTextType().DeleteByIdContent(content.Id, content.IdUsuario);
            foreach (var upgradeVariableTextType in content.UpgradeVariableTexts)
            {
                count++;
                var contentSignatureType = new Content_R_ContentUpgradeVariableTextType
                {
                    UpgradeVariableTextType = upgradeVariableTextType,
                    IdContent = content.Id,
                    IdUsuario = content.IdUsuario,
                    IdEstado = ObjetoNegocio.Creado()
                };
                DAOLocator.Instance().GetDaoContent_R_ContentUpgradeVariableTextType().Persistir(contentSignatureType, ts);
            }

            #endregion Upgrade Variable Text Type
        }

        protected override void ModificarComposicionPredecesor(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            CrearComposicionPredecesor(objetoNegocio, ts);
        }

        protected override void CrearComposicionPredecesor(ObjetoNegocio objetoNegocio, TransactionScope ts)
        {
            var content = (Content) objetoNegocio;

            #region Images

            foreach (ContentImage contentImage in content.Images)
            {
                if (contentImage.Id == 0)
                    DAOLocator.Instance().GetDaoContentImage().Persistir(contentImage, ts);
            }

            #endregion Images

            #region Links

            foreach (Link link in content.Links)
            {
                if (link.Id == 0)
                    DAOLocator.Instance().GetDaoLink().Persistir(link, ts);
            }

            #endregion Links

            #region Headers & Footers

            if (content.Header != null && content.Header.Content != null)
            {
                DAOLocator.Instance().GetDaoContentImage().Persistir(content.Header, ts);
            }
            if (content.HeaderPDF != null && content.HeaderPDF.Content != null)
            {
                DAOLocator.Instance().GetDaoContentImage().Persistir(content.HeaderPDF, ts);
            }
            if (content.Footer != null && content.Footer.Content != null)
            {
                DAOLocator.Instance().GetDaoContentImage().Persistir(content.Footer, ts);
            }
            if (content.FooterPDF != null && content.FooterPDF.Content != null)
            {
                DAOLocator.Instance().GetDaoContentImage().Persistir(content.FooterPDF, ts);
            }

            #endregion Headers & Footers
        }

        protected override void EliminarComposicionPredecesor(ObjetoNegocio ObjetoNegocio, TransactionScope ts)
        {            
        //protected override void EliminarComposicion(ObjetoNegocio objetoNegocio, TransactionScope ts)
        
            var content = (Content) ObjetoNegocio;
            DAOLocator.Instance().GetDaoContent_R_Image().DeleteByIdContent(content.Id,content.IdUsuario,ts);
            DAOLocator.Instance().GetDaoContent_R_ContentLink().DeleteByIdContent(content.Id, content.IdUsuario, ts);
            DAOLocator.Instance().GetDaoContent_R_ContentVariableText().DeleteByIdContent(content.Id, content.IdUsuario, ts);
            DAOLocator.Instance().GetDaoContent_R_ContentSignatureType().DeleteByIdContent(content.Id, content.IdUsuario, ts);
            DAOLocator.Instance().GetDaoContent_R_ContentEmailContactType().DeleteByIdContent(content.Id, content.IdUsuario, ts);
            DAOLocator.Instance().GetDaoContent_R_ContentCountryVisibleTextType().DeleteByIdContent(content.Id, content.IdUsuario, ts);
            DAOLocator.Instance().GetDaoContent_R_ContentUpgradeVariableTextType().DeleteByIdContent(content.Id, content.IdUsuario, ts);
        }

        protected override void CompletarComposicion(Content objetoPersistido)
        {
            objetoPersistido.Header = DAOLocator.Instance().GetDaoContentImage().Get(objetoPersistido.IdHeader);
            objetoPersistido.Footer = DAOLocator.Instance().GetDaoContentImage().Get(objetoPersistido.IdFooter);
            objetoPersistido.HeaderPDF = DAOLocator.Instance().GetDaoContentImage().Get(objetoPersistido.IdHeaderPDF);
            objetoPersistido.FooterPDF = DAOLocator.Instance().GetDaoContentImage().Get(objetoPersistido.IdFooterPDF);
        }
    }
}