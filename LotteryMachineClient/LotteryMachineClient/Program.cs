using LotteryMachineClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
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

            NumberDrawer numberDrawer = new NumberDrawer(proxy);

            Console.WriteLine("Starting the Lottery Machine...");
            Console.WriteLine("First draw will be in 1 minute.");

            // Start drawing numbers every minute
            var timer = new System.Timers.Timer(60000);
            timer.Elapsed += (sender, e) => numberDrawer.DrawAndSendNumbers();
            timer.Start();

            Console.WriteLine("Lottery Machine is running. Press [Enter] to exit.");
            Console.ReadLine();

            timer.Stop();
            ((IClientChannel)proxy).Close();
            factory.Close();
        }
    }
}
