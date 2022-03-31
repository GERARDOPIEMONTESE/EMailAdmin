﻿//------------------------------------------------------------------------------
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

namespace EMailAdmin.EMailSendTesting {
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
        
        private System.Threading.SendOrPostCallback SendMailEkitOperationCompleted;
        
        private System.Threading.SendOrPostCallback SendMultipleMailEkitOperationCompleted;
        
        private System.Threading.SendOrPostCallback InitEMailProcessOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public EMailSenderService() {
            this.Url = global::EMailAdmin.Properties.Settings.Default.EMailAdmin_EMailSendTesting_EMailSenderService;
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
        public event SendMailEkitCompletedEventHandler SendMailEkitCompleted;
        
        /// <remarks/>
        public event SendMultipleMailEkitCompletedEventHandler SendMultipleMailEkitCompleted;
        
        /// <remarks/>
        public event InitEMailProcessCompletedEventHandler InitEMailProcessCompleted;
        
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
        public void SendMailEkitAsync(int countryCode, string voucherCode, string moduleCode, string user, string password) {
            this.SendMailEkitAsync(countryCode, voucherCode, moduleCode, user, password, null);
        }
        
        /// <remarks/>
        public void SendMailEkitAsync(int countryCode, string voucherCode, string moduleCode, string user, string password, object userState) {
            if ((this.SendMailEkitOperationCompleted == null)) {
                this.SendMailEkitOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMailEkitOperationCompleted);
            }
            this.InvokeAsync("SendMailEkit", new object[] {
                        countryCode,
                        voucherCode,
                        moduleCode,
                        user,
                        password}, this.SendMailEkitOperationCompleted, userState);
        }
        
        private void OnSendMailEkitOperationCompleted(object arg) {
            if ((this.SendMailEkitCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMailEkitCompleted(this, new SendMailEkitCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
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
        public void SendMultipleMailEkitAsync(int countryCode, string voucherCodes, string moduleCode, string user, string password) {
            this.SendMultipleMailEkitAsync(countryCode, voucherCodes, moduleCode, user, password, null);
        }
        
        /// <remarks/>
        public void SendMultipleMailEkitAsync(int countryCode, string voucherCodes, string moduleCode, string user, string password, object userState) {
            if ((this.SendMultipleMailEkitOperationCompleted == null)) {
                this.SendMultipleMailEkitOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMultipleMailEkitOperationCompleted);
            }
            this.InvokeAsync("SendMultipleMailEkit", new object[] {
                        countryCode,
                        voucherCodes,
                        moduleCode,
                        user,
                        password}, this.SendMultipleMailEkitOperationCompleted, userState);
        }
        
        private void OnSendMultipleMailEkitOperationCompleted(object arg) {
            if ((this.SendMultipleMailEkitCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMultipleMailEkitCompleted(this, new SendMultipleMailEkitCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InitEMailProcess", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void InitEMailProcess(string user, string password) {
            this.Invoke("InitEMailProcess", new object[] {
                        user,
                        password});
        }
        
        /// <remarks/>
        public void InitEMailProcessAsync(string user, string password) {
            this.InitEMailProcessAsync(user, password, null);
        }
        
        /// <remarks/>
        public void InitEMailProcessAsync(string user, string password, object userState) {
            if ((this.InitEMailProcessOperationCompleted == null)) {
                this.InitEMailProcessOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInitEMailProcessOperationCompleted);
            }
            this.InvokeAsync("InitEMailProcess", new object[] {
                        user,
                        password}, this.InitEMailProcessOperationCompleted, userState);
        }
        
        private void OnInitEMailProcessOperationCompleted(object arg) {
            if ((this.InitEMailProcessCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InitEMailProcessCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void SendMailEkitCompletedEventHandler(object sender, SendMailEkitCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMailEkitCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendMailEkitCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void SendMultipleMailEkitCompletedEventHandler(object sender, SendMultipleMailEkitCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMultipleMailEkitCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SendMultipleMailEkitCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void InitEMailProcessCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591