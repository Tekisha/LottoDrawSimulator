﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LotteryMachineClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.ILotteryMachineService")]
    public interface ILotteryMachineService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILotteryMachineService/RegisterPlayer", ReplyAction="http://tempuri.org/ILotteryMachineService/RegisterPlayerResponse")]
        void RegisterPlayer(string playerName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILotteryMachineService/RegisterPlayer", ReplyAction="http://tempuri.org/ILotteryMachineService/RegisterPlayerResponse")]
        System.Threading.Tasks.Task RegisterPlayerAsync(string playerName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILotteryMachineService/StartDrawing", ReplyAction="http://tempuri.org/ILotteryMachineService/StartDrawingResponse")]
        void StartDrawing();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ILotteryMachineService/StartDrawing", ReplyAction="http://tempuri.org/ILotteryMachineService/StartDrawingResponse")]
        System.Threading.Tasks.Task StartDrawingAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ILotteryMachineServiceChannel : LotteryMachineClient.ServiceReference1.ILotteryMachineService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class LotteryMachineServiceClient : System.ServiceModel.ClientBase<LotteryMachineClient.ServiceReference1.ILotteryMachineService>, LotteryMachineClient.ServiceReference1.ILotteryMachineService {
        
        public LotteryMachineServiceClient() {
        }
        
        public LotteryMachineServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public LotteryMachineServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LotteryMachineServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public LotteryMachineServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void RegisterPlayer(string playerName) {
            base.Channel.RegisterPlayer(playerName);
        }
        
        public System.Threading.Tasks.Task RegisterPlayerAsync(string playerName) {
            return base.Channel.RegisterPlayerAsync(playerName);
        }
        
        public void StartDrawing() {
            base.Channel.StartDrawing();
        }
        
        public System.Threading.Tasks.Task StartDrawingAsync() {
            return base.Channel.StartDrawingAsync();
        }
    }
}