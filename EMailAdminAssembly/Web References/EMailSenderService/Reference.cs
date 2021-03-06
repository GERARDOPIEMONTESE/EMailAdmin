//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.269.
// 
#pragma warning disable 1591

namespace EMailAdminAssembly.EMailSenderService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="EMailSenderServiceSoap", Namespace="http://tempuri.org/")]
    public partial class EMailSenderService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public EMailSenderService() {
            this.Url = "http://mailservice.dev.assist-card.com/EMailSenderService.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendMailEkit", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SendMailEkit(int countryCode, string voucherCode, string moduleCode, string user, string password) {
            object[] results = this.Invoke("SendMailEkit", new object[] {
                        countryCode,
                        voucherCode,
                        moduleCode,
                        user,
                        password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSendMailEkit(int countryCode, string voucherCode, string moduleCode, string user, string password, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendMailEkit", new object[] {
                        countryCode,
                        voucherCode,
                        moduleCode,
                        user,
                        password}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSendMailEkit(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SendMultipleMailEkit", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SendMultipleMailEkit(int countryCode, string voucherCodes, string moduleCode, string user, string password) {
            object[] results = this.Invoke("SendMultipleMailEkit", new object[] {
                        countryCode,
                        voucherCodes,
                        moduleCode,
                        user,
                        password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginSendMultipleMailEkit(int countryCode, string voucherCodes, string moduleCode, string user, string password, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendMultipleMailEkit", new object[] {
                        countryCode,
                        voucherCodes,
                        moduleCode,
                        user,
                        password}, callback, asyncState);
        }
        
        /// <remarks/>
        public string EndSendMultipleMailEkit(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InitEMailProcess", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void InitEMailProcess(string user, string password) {
            this.Invoke("InitEMailProcess", new object[] {
                        user,
                        password});
        }
        
        /// <remarks/>
        public System.IAsyncResult BeginInitEMailProcess(string user, string password, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("InitEMailProcess", new object[] {
                        user,
                        password}, callback, asyncState);
        }
        
        /// <remarks/>
        public void EndInitEMailProcess(System.IAsyncResult asyncResult) {
            this.EndInvoke(asyncResult);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
}

#pragma warning restore 1591