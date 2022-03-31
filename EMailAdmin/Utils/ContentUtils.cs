using System.Collections.Generic;
using System.Web.SessionState;
using EMailAdmin.BackEnd.Domain;

namespace EMailAdmin.Utils
{
    public class ContentUtils
    {
        public static IList<ContentImage> GetContentImages(HttpSessionState session, int idLanguage)
        {
            return SessionManager.GetContentImages(session) != null
                       ? (SessionManager.GetContentImages(session).ContainsKey(idLanguage)
                              ? SessionManager.GetContentImages(session)[idLanguage]
                              : new List<ContentImage>())
                       : new List<ContentImage>();
        }

        public static IList<Link> GetContentLinks(HttpSessionState session, int idLanguage)
        {
            return SessionManager.GetContentLinks(session) != null
                       ? (SessionManager.GetContentLinks(session).ContainsKey(idLanguage)
                              ? SessionManager.GetContentLinks(session)[idLanguage]
                              : new List<Link>())
                       : new List<Link>();
        }

        public static IList<VariableText> GetContentVariableTexts(HttpSessionState session, int idLanguage)
        {
            return SessionManager.GetContentVariableTexts(session) != null
                       ? (SessionManager.GetContentVariableTexts(session).ContainsKey(idLanguage)
                              ? SessionManager.GetContentVariableTexts(session)[idLanguage]
                              : new List<VariableText>())
                       : new List<VariableText>();
        }

        public static IList<SignatureType> GetContentSignatures(HttpSessionState session, int idLanguage)
        {
            return SessionManager.GetContentSignatures(session) != null
                       ? (SessionManager.GetContentSignatures(session).ContainsKey(idLanguage)
                              ? SessionManager.GetContentSignatures(session)[idLanguage]
                              : new List<SignatureType>())
                       : new List<SignatureType>();
        }

        public static IList<EMailContactType> GetContentContacts(HttpSessionState session, int idLanguage)
        {
            return SessionManager.GetContentContacts(session) != null
                       ? (SessionManager.GetContentContacts(session).ContainsKey(idLanguage)
                              ? SessionManager.GetContentContacts(session)[idLanguage]
                              : new List<EMailContactType>())
                       : new List<EMailContactType>();
        }

        public static IList<CountryVisibleTextType> GetContentCountryVisibleTexts(HttpSessionState session, int idLanguage)
        {
            return SessionManager.GetContentCountryVisibleText(session) != null
                       ? (SessionManager.GetContentCountryVisibleText(session).ContainsKey(idLanguage)
                              ? SessionManager.GetContentCountryVisibleText(session)[idLanguage]
                              : new List<CountryVisibleTextType>())
                       : new List<CountryVisibleTextType>();
        }

        public static IList<UpgradeVariableTextType> GetContentUpgradeVariableTexts(HttpSessionState session, int idLanguage)
        {
            return SessionManager.GetContentUpgradeVariableText(session) != null
                       ? (SessionManager.GetContentUpgradeVariableText(session).ContainsKey(idLanguage)
                              ? SessionManager.GetContentUpgradeVariableText(session)[idLanguage]
                              : new List<UpgradeVariableTextType>())
                       : new List<UpgradeVariableTextType>();
        }

        public static IList<EMailAdmin.BackEnd.Domain.ConditionVariableText> GetContentConditionVariableTexts(HttpSessionState session, int idLanguage)
        {
            return SessionManager.GetContentConditionVariableText(session) != null
                       ? (SessionManager.GetContentConditionVariableText(session).ContainsKey(idLanguage)
                              ? SessionManager.GetContentConditionVariableText(session)[idLanguage]
                              : new List<EMailAdmin.BackEnd.Domain.ConditionVariableText>())
                       : new List<EMailAdmin.BackEnd.Domain.ConditionVariableText>();
        }

        public static string GetContentColor(HttpSessionState session, int idLanguage)
        {
            return SessionManager.GetTemplateColor(session) != null
                       ? (SessionManager.GetTemplateColor(session).ContainsKey(idLanguage)
                              ? SessionManager.GetTemplateColor(session)[idLanguage]
                              : "")
                       : "";
        }

    }
}