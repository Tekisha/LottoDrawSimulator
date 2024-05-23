using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerClient
{
    public static class UserInputValidator
    {
        public static int GetValidNumber(string prompt)
        {
            int number;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out number) && number >= 0 && number <= 10)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 10.");
                }
            }
            return number;
        }

        public static decimal GetValidAmount(string prompt)
        {
            decimal amount;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out amount) && amount > 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive amount.");
                }
            }
            return amount;
        }
    }
}
