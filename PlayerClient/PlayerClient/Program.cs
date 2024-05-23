using PlayerClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using static PlayerClient.UserInputValidator;

namespace PlayerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            InstanceContext instanceContext = new InstanceContext(new PlayerCallback());
            PlayerServiceClient client = new PlayerServiceClient(instanceContext);

            Console.WriteLine("Welcome to the LottoDrawSimulator!");

            int number1 = GetValidNumber("Enter your first number (0-10): ");
            int number2 = GetValidNumber("Enter your second number (0-10): ");
            decimal amount = GetValidAmount("Enter the amount of money you want to bet: ");

            string playerName = client.InitPlayer(number1, number2, amount);
            if (string.IsNullOrEmpty(playerName))
            {
                Console.WriteLine("Player registration failed. Please try again.");
                return;
            }
            Console.WriteLine($"Player registered with name: {playerName}");

            Console.WriteLine("Waiting for drawn numbers...");
            Console.ReadLine(); // Keep the client running
        }
    }
}
