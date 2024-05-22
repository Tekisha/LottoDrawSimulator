using LotteryMachineClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LotteryMachineClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ILotteryMachineService> factory = new ChannelFactory<ILotteryMachineService>(new WSHttpBinding(), new EndpointAddress("http://localhost:8080/LotteryMachineService"));
            ILotteryMachineService proxy = factory.CreateChannel();

            Console.WriteLine("Starting the Lottery Machine...");
            proxy.StartDrawing();

            Console.WriteLine("Lottery Machine is running. Press [Enter] to exit.");
            Console.ReadLine();
        }
    }
}
