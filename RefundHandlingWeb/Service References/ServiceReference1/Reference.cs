﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RefundHandlingWeb.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IExtendedSearchService")]
    public interface IExtendedSearchService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExtendedSearchService/SearchInRedis", ReplyAction="http://tempuri.org/IExtendedSearchService/SearchInRedisResponse")]
        string[] SearchInRedis([System.ServiceModel.MessageParameterAttribute(Name="SearchInRedis")] string SearchInRedis1);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IExtendedSearchService/SearchInRedis", ReplyAction="http://tempuri.org/IExtendedSearchService/SearchInRedisResponse")]
        System.Threading.Tasks.Task<string[]> SearchInRedisAsync(string SearchInRedis);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IExtendedSearchServiceChannel : RefundHandlingWeb.ServiceReference1.IExtendedSearchService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ExtendedSearchServiceClient : System.ServiceModel.ClientBase<RefundHandlingWeb.ServiceReference1.IExtendedSearchService>, RefundHandlingWeb.ServiceReference1.IExtendedSearchService {
        
        public ExtendedSearchServiceClient() {
        }
        
        public ExtendedSearchServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ExtendedSearchServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExtendedSearchServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ExtendedSearchServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[] SearchInRedis(string SearchInRedis1) {
            return base.Channel.SearchInRedis(SearchInRedis1);
        }
        
        public System.Threading.Tasks.Task<string[]> SearchInRedisAsync(string SearchInRedis) {
            return base.Channel.SearchInRedisAsync(SearchInRedis);
        }
    }
}