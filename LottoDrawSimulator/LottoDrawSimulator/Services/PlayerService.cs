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
        private static Dictionary<string, IPlayerCallback> playerCallbacks = new Dictionary<string, IPlayerCallback>();

        public string InitPlayer(int number1, int number2, decimal amount)
        {
            if (number1 < 0 || number1 > 10 || number2 < 0 || number2 > 10)
                return "Invalid numbers. Numbers must be in the range of 0 to 10.";
            if (amount <= 0)
                return "Invalid amount. Amount must be greater than zero.";

            string playerName = OperationContext.Current.SessionId;
            if (players.ContainsKey(playerName))
                return "You have already registered.";

            players[playerName] = (number1, number2, amount);
            playerEarnings[playerName] = 0;

            IPlayerCallback callback = OperationContext.Current.GetCallbackChannel<IPlayerCallback>();
            if (!playerCallbacks.ContainsKey(playerName))
            {
                playerCallbacks[playerName] = callback;
            }

            return playerName;
        }

        public static void NotifyPlayers(int[] drawnNumbers)
        {
            var playerResults = new List<(string playerName, int hitCount, decimal earnings)>();

            foreach (var player in players)
            {
                int hitCount = 0;
                var guessedNumbers = new List<int> { player.Value.Item1, player.Value.Item2 };

                var tempDrawnNumbers = new List<int>(drawnNumbers);

                foreach (var number in guessedNumbers)
                {
                    if (tempDrawnNumbers.Contains(number))
                    {
                        hitCount++;
                        tempDrawnNumbers.Remove(number);
                    }
                }

                decimal earnings = 0;
                if (hitCount == 1)
                    earnings = player.Value.Item3;
                else if (hitCount == 2)
                    earnings = player.Value.Item3 * 5;

                playerEarnings[player.Key] += earnings;

                playerResults.Add((player.Key, hitCount, earnings));
            }

            var rankedPlayers = playerEarnings.OrderByDescending(e => e.Value).Select((e, index) => new { e.Key, Rank = index + 1 }).ToDictionary(p => p.Key, p => p.Rank);

            foreach (var result in playerResults)
            {
                int rank = rankedPlayers[result.playerName];
                if (playerCallbacks.ContainsKey(result.playerName))
                {
                    var callback = playerCallbacks[result.playerName];
                    callback.NotifyDrawnNumbers(drawnNumbers, result.hitCount, result.earnings, rank);
                }
            }

            foreach (var result in playerResults)
            {
                int rank = rankedPlayers[result.playerName];
                Console.WriteLine($"Player {result.playerName}: Drawn numbers: {string.Join(", ", drawnNumbers)}");
                Console.WriteLine($"Hit numbers: {result.hitCount}, Earnings: {result.earnings}, Total earnings: {playerEarnings[result.playerName]}, Rank: {rank}");
            }
        }
    }
}
