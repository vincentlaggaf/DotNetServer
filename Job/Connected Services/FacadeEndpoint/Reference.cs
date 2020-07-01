﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Job.FacadeEndpoint {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://facade.receptionfacade.appjee.com/", ConfigurationName="FacadeEndpoint.FacadeEndpoint")]
    public interface FacadeEndpoint {
        
        // CODEGEN : Le paramètre 'receivedOrder' nécessite des informations de schéma supplémentaires qui ne peuvent pas être capturées en utilisant le mode du paramètre. L'attribut spécifique est 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="http://facade.receptionfacade.appjee.com/FacadeEndpoint/receiveDecipherOrderReque" +
            "st", ReplyAction="http://facade.receptionfacade.appjee.com/FacadeEndpoint/receiveDecipherOrderRespo" +
            "nse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(receiveDecipherOrderResponse))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(receiveDecipherOrder))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="receivedOrder")]
        Job.FacadeEndpoint.receiveDecipherOrderResponse1 receiveDecipherOrder(Job.FacadeEndpoint.receiveDecipherOrderRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://facade.receptionfacade.appjee.com/FacadeEndpoint/receiveDecipherOrderReque" +
            "st", ReplyAction="http://facade.receptionfacade.appjee.com/FacadeEndpoint/receiveDecipherOrderRespo" +
            "nse")]
        System.Threading.Tasks.Task<Job.FacadeEndpoint.receiveDecipherOrderResponse1> receiveDecipherOrderAsync(Job.FacadeEndpoint.receiveDecipherOrderRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://facade.receptionfacade.appjee.com/")]
    public partial class soapMessage : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string appVersionField;
        
        private object[] dataField;
        
        private string infoField;
        
        private string operationNameField;
        
        private string operationVersionField;
        
        private statutOp statutOpField;
        
        private bool statutOpFieldSpecified;
        
        private string tokenAppField;
        
        private string tokenUserField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string appVersion {
            get {
                return this.appVersionField;
            }
            set {
                this.appVersionField = value;
                this.RaisePropertyChanged("appVersion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("data", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true, Order=1)]
        public object[] data {
            get {
                return this.dataField;
            }
            set {
                this.dataField = value;
                this.RaisePropertyChanged("data");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string info {
            get {
                return this.infoField;
            }
            set {
                this.infoField = value;
                this.RaisePropertyChanged("info");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string operationName {
            get {
                return this.operationNameField;
            }
            set {
                this.operationNameField = value;
                this.RaisePropertyChanged("operationName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string operationVersion {
            get {
                return this.operationVersionField;
            }
            set {
                this.operationVersionField = value;
                this.RaisePropertyChanged("operationVersion");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public statutOp statutOp {
            get {
                return this.statutOpField;
            }
            set {
                this.statutOpField = value;
                this.RaisePropertyChanged("statutOp");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool statutOpSpecified {
            get {
                return this.statutOpFieldSpecified;
            }
            set {
                this.statutOpFieldSpecified = value;
                this.RaisePropertyChanged("statutOpSpecified");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string tokenApp {
            get {
                return this.tokenAppField;
            }
            set {
                this.tokenAppField = value;
                this.RaisePropertyChanged("tokenApp");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string tokenUser {
            get {
                return this.tokenUserField;
            }
            set {
                this.tokenUserField = value;
                this.RaisePropertyChanged("tokenUser");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://facade.receptionfacade.appjee.com/")]
    public partial class receiveDecipherOrderResponse : object, System.ComponentModel.INotifyPropertyChanged {
        
        private soapMessage receivedOrderField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public soapMessage receivedOrder {
            get {
                return this.receivedOrderField;
            }
            set {
                this.receivedOrderField = value;
                this.RaisePropertyChanged("receivedOrder");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://facade.receptionfacade.appjee.com/")]
    public partial class receiveDecipherOrder : object, System.ComponentModel.INotifyPropertyChanged {
        
        private soapMessage soapMessageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public soapMessage soapMessage {
            get {
                return this.soapMessageField;
            }
            set {
                this.soapMessageField = value;
                this.RaisePropertyChanged("soapMessage");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.3761.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://facade.receptionfacade.appjee.com/")]
    public enum statutOp {
        
        /// <remarks/>
        Waiting,
        
        /// <remarks/>
        Working,
        
        /// <remarks/>
        Finished,
        
        /// <remarks/>
        Sent,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="receiveDecipherOrder", WrapperNamespace="http://facade.receptionfacade.appjee.com/", IsWrapped=true)]
    public partial class receiveDecipherOrderRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://facade.receptionfacade.appjee.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Job.FacadeEndpoint.soapMessage soapMessage;
        
        public receiveDecipherOrderRequest() {
        }
        
        public receiveDecipherOrderRequest(Job.FacadeEndpoint.soapMessage soapMessage) {
            this.soapMessage = soapMessage;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="receiveDecipherOrderResponse", WrapperNamespace="http://facade.receptionfacade.appjee.com/", IsWrapped=true)]
    public partial class receiveDecipherOrderResponse1 {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://facade.receptionfacade.appjee.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Job.FacadeEndpoint.soapMessage receivedOrder;
        
        public receiveDecipherOrderResponse1() {
        }
        
        public receiveDecipherOrderResponse1(Job.FacadeEndpoint.soapMessage receivedOrder) {
            this.receivedOrder = receivedOrder;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface FacadeEndpointChannel : Job.FacadeEndpoint.FacadeEndpoint, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FacadeEndpointClient : System.ServiceModel.ClientBase<Job.FacadeEndpoint.FacadeEndpoint>, Job.FacadeEndpoint.FacadeEndpoint {
        
        public FacadeEndpointClient() {
        }
        
        public FacadeEndpointClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FacadeEndpointClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FacadeEndpointClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FacadeEndpointClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Job.FacadeEndpoint.receiveDecipherOrderResponse1 Job.FacadeEndpoint.FacadeEndpoint.receiveDecipherOrder(Job.FacadeEndpoint.receiveDecipherOrderRequest request) {
            return base.Channel.receiveDecipherOrder(request);
        }
        
        public Job.FacadeEndpoint.soapMessage receiveDecipherOrder(Job.FacadeEndpoint.soapMessage soapMessage) {
            Job.FacadeEndpoint.receiveDecipherOrderRequest inValue = new Job.FacadeEndpoint.receiveDecipherOrderRequest();
            inValue.soapMessage = soapMessage;
            Job.FacadeEndpoint.receiveDecipherOrderResponse1 retVal = ((Job.FacadeEndpoint.FacadeEndpoint)(this)).receiveDecipherOrder(inValue);
            return retVal.receivedOrder;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Job.FacadeEndpoint.receiveDecipherOrderResponse1> Job.FacadeEndpoint.FacadeEndpoint.receiveDecipherOrderAsync(Job.FacadeEndpoint.receiveDecipherOrderRequest request) {
            return base.Channel.receiveDecipherOrderAsync(request);
        }
        
        public System.Threading.Tasks.Task<Job.FacadeEndpoint.receiveDecipherOrderResponse1> receiveDecipherOrderAsync(Job.FacadeEndpoint.soapMessage soapMessage) {
            Job.FacadeEndpoint.receiveDecipherOrderRequest inValue = new Job.FacadeEndpoint.receiveDecipherOrderRequest();
            inValue.soapMessage = soapMessage;
            return ((Job.FacadeEndpoint.FacadeEndpoint)(this)).receiveDecipherOrderAsync(inValue);
        }
    }
}
