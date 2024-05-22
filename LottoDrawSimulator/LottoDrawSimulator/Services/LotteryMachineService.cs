using LottoDrawSimulator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace LottoDrawSimulator.Services
{
    public class LotteryMachineService : ILotteryMachineService
    {
        private static List<string> registeredPlayers = new List<string>();
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private Timer _timer;

        public void RegisterPlayer(string playerName)
        {
            if (!registeredPlayers.Contains(playerName))
            {
                registeredPlayers.Add(playerName);
            }
        }


        public void StartDrawing()
        {
            _timer = new Timer(60000); // 1 minute interval
            _timer.Elapsed += (sender, e) => PlayerService.DrawNumbers();
            _timer.Start();
            Console.WriteLine("Started drawing numbers every minute.");
        }

        private void DrawNumbers(object state)
        {
            int[] drawnNumbers = new int[2];
            byte[] randomNumber = new byte[1];

            for (int i = 0; i < 2; i++)
            {
                rng.GetBytes(randomNumber);
                drawnNumbers[i] = randomNumber[0] % 11;
            }

            PlayerService.DrawNumbers();
        }
    }
}
