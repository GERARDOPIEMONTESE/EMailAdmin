﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMailAdmin.BackEnd.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("${")]
        public string VariableInitTag {
            get {
                return ((string)(this["VariableInitTag"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("}$")]
        public string VariableEndTag {
            get {
                return ((string)(this["VariableEndTag"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(",")]
        public string VariableSeparator {
            get {
                return ((string)(this["VariableSeparator"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("S")]
        public string ClauseSeparator {
            get {
                return ((string)(this["ClauseSeparator"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("||")]
        public string OrClause {
            get {
                return ((string)(this["OrClause"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("&&")]
        public string AndClause {
            get {
                return ((string)(this["AndClause"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.assist-card.net/ws/services/AssistCardService")]
        public string EMailAdmin_BackEnd_AssistCardService_AssistCardServiceService {
            get {
                return ((string)(this["EMailAdmin_BackEnd_AssistCardService_AssistCardServiceService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.assist-card.net/ws/services/AssistCardDaysAcquisitionService")]
        public string EMailAdmin_BackEnd_ServiceBoxPax_AssistCardDaysAcquisitionServiceService {
            get {
                return ((string)(this["EMailAdmin_BackEnd_ServiceBoxPax_AssistCardDaysAcquisitionServiceService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://serviciocondiciones.assist-card.com/ServicioClausulasWS.asmx")]
        public string EMailAdmin_BackEnd_wsServiciocondiciones_ServicioClausulasWS {
            get {
                return ((string)(this["EMailAdmin_BackEnd_wsServiciocondiciones_ServicioClausulasWS"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://acixamweb05.assist-card.com.ar/eAAvs/WebServices/WsMail/WSMail.svc/soap")]
        public string EMailAdmin_BackEnd_wsXAM_MailService {
            get {
                return ((string)(this["EMailAdmin_BackEnd_wsXAM_MailService"]));
            }
        }
    }
}