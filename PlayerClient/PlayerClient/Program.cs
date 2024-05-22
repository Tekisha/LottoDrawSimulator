using PlayerClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PlayerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            InstanceContext instanceContext = new InstanceContext(new PlayerCallback());
            PlayerServiceClient client = new PlayerServiceClient(instanceContext);

            Console.WriteLine("Welcome to the LottoDrawSimulator!");
            Console.Write("Enter your first number (0-10): ");
            int number1 = int.Parse(Console.ReadLine());
            Console.Write("Enter your second number (0-10): ");
            int number2 = int.Parse(Console.ReadLine());
            Console.Write("Enter the amount of money you want to bet: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            string playerName = client.InitPlayer(number1, number2, amount);
            Console.WriteLine($"Player registered with name: {playerName}");

            Console.WriteLine("Waiting for drawn numbers...");
            Console.ReadLine();
        }
    }
}
