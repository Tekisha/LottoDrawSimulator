using LottoDrawSimulator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LottoDrawSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost playerServiceHost = new ServiceHost(typeof(PlayerService)))
            {
                playerServiceHost.Open();
                Console.WriteLine("PlayerService is running...");

                using (ServiceHost lotteryMachineServiceHost = new ServiceHost(typeof(LotteryMachineService)))
                {
                    lotteryMachineServiceHost.Open();
                    Console.WriteLine("LotteryMachineService is running...");

                    Console.WriteLine("Services are running. Press [Enter] to exit.");
                    Console.ReadLine();

                    lotteryMachineServiceHost.Close();
                }

                playerServiceHost.Close();
            }
        }
    }
}
