using LottoDrawSimulator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LottoDrawSimulator.Services
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class PlayerService : IPlayerService
    {
        private static Dictionary<string, (int, int, decimal)> players = new Dictionary<string, (int, int, decimal)>();
        private static Dictionary<string, decimal> playerEarnings = new Dictionary<string, decimal>();
        private static List<IPlayerCallback> callbacks = new List<IPlayerCallback>();

        public string InitPlayer(int number1, int number2, decimal amount)
        {
            if (number1 < 0 || number1 > 10 || number2 < 0 || number2 > 10)
                return "Numbers must be in the range of 0 to 10.";
            if (amount <= 0)
                return "Amount must be greater than zero.";

            string playerName = Guid.NewGuid().ToString();
            players[playerName] = (number1, number2, amount);
            playerEarnings[playerName] = 0;

            // Register the callback
            IPlayerCallback callback = OperationContext.Current.GetCallbackChannel<IPlayerCallback>();
            if (!callbacks.Contains(callback))
            {
                callbacks.Add(callback);
            }

            return playerName;
        }

        public static void DrawNumbers()
        {
            Random random = new Random();
            int[] drawnNumbers = new int[] { random.Next(0, 11), random.Next(0, 11) };

            foreach (var callback in callbacks)
            {
                callback.NotifyDrawnNumbers(drawnNumbers);
            }

            foreach (var player in players)
            {
                int hitCount = 0;
                if (drawnNumbers.Contains(player.Value.Item1)) hitCount++;
                if (drawnNumbers.Contains(player.Value.Item2)) hitCount++;

                decimal earnings = 0;
                if (hitCount == 1)
                    earnings = player.Value.Item3;
                else if (hitCount == 2)
                    earnings = player.Value.Item3 * 5;

                playerEarnings[player.Key] += earnings;

                Console.WriteLine($"Player {player.Key}: Drawn numbers: {string.Join(", ", drawnNumbers)}");
                Console.WriteLine($"Hit numbers: {hitCount}, Earnings: {earnings}, Total earnings: {playerEarnings[player.Key]}");
            }
        }
    }
}
