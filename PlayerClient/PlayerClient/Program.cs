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

            while (true)
            {
                int number1 = UserInputValidator.GetValidNumber("Enter your first number (0-10): ");
                int number2 = UserInputValidator.GetValidNumber("Enter your second number (0-10): ");
                decimal amount = UserInputValidator.GetValidAmount("Enter the amount of money you want to bet: ");

                string result = client.InitPlayer(number1, number2, amount);
                if (result == "You have already placed a ticket. Wait for the next draw.")
                {
                    Console.WriteLine(result);
                }
                else if (result == "Invalid numbers. Numbers must be in the range of 0 to 10." || result == "Invalid amount. Amount must be greater than zero.")
                {
                    Console.WriteLine(result);
                }
                else
                {
                    Console.WriteLine($"Player registered with name: {result}");
                    Console.WriteLine("Waiting for drawn numbers...");
                    PlayerCallback.NotificationReceived.WaitOne();
                    PlayerCallback.NotificationReceived.Reset();
                }

                Console.WriteLine("Would you like to play again? (y/n)");
                string playAgain = Console.ReadLine().ToLower();
                if (playAgain != "y")
                {
                    break;
                }
            }
        }
    }
}
