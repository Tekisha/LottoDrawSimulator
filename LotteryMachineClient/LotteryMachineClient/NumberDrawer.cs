using LotteryMachineClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LotteryMachineClient
{
    public class NumberDrawer
    {
        private readonly ILotteryMachineService _proxy;

        public NumberDrawer(ILotteryMachineService proxy)
        {
            _proxy = proxy;
        }

        public void DrawAndSendNumbers()
        {
            int[] drawnNumbers = new int[2];
            byte[] randomNumber = new byte[1];
            using (var rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 2; i++)
                {
                    rng.GetBytes(randomNumber);
                    drawnNumbers[i] = randomNumber[0] % 11;
                }
            }

            Console.WriteLine($"Drawn numbers: {string.Join(", ", drawnNumbers)}");
            _proxy.ReceiveDrawnNumbers(drawnNumbers);
        }
    }
}
