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
            if (!IsValidNumber(number1) || !IsValidNumber(number2))
                return "Invalid numbers. Numbers must be in the range of 0 to 10.";
            if (amount <= 0)
                return "Invalid amount. Amount must be greater than zero.";

            string playerName = OperationContext.Current.SessionId;

            IPlayerCallback callback = OperationContext.Current.GetCallbackChannel<IPlayerCallback>();
            playerCallbacks[playerName] = callback;

            players[playerName] = (number1, number2, amount);
            if (!playerEarnings.ContainsKey(playerName))
            {
                playerEarnings[playerName] = 0;
            }

            return playerName;
        }

        private bool IsValidNumber(int number) => number >= 0 && number <= 10;

        public static void NotifyPlayers(int[] drawnNumbers)
        {
            var playerResults = CalculatePlayerResults(drawnNumbers);
            var rankedPlayers = CalculatePlayerRanks();

            NotifyPlayerCallbacks(playerResults, drawnNumbers, rankedPlayers);
            LogPlayerResults(playerResults, rankedPlayers);
            ClearPlayers();
        }

        private static List<(string playerName, int hitCount, decimal earnings, decimal totalEarnings)> CalculatePlayerResults(int[] drawnNumbers)
        {
            var playerResults = new List<(string playerName, int hitCount, decimal earnings, decimal totalEarnings)>();

            foreach (var player in players)
            {
                int hitCount = CalculateHitCount(player.Value.Item1, player.Value.Item2, drawnNumbers);
                decimal earnings = CalculateEarnings(hitCount, player.Value.Item3);
                playerEarnings[player.Key] += earnings;
                playerResults.Add((player.Key, hitCount, earnings, playerEarnings[player.Key]));
            }

            return playerResults;
        }

        private static int CalculateHitCount(int number1, int number2, int[] drawnNumbers)
        {
            var guessedNumbers = new List<int> { number1, number2 };
            var tempDrawnNumbers = new List<int>(drawnNumbers);
            int hitCount = 0;

            foreach (var number in guessedNumbers)
            {
                if (tempDrawnNumbers.Contains(number))
                {
                    hitCount++;
                    tempDrawnNumbers.Remove(number);
                }
            }

            return hitCount;
        }

        private static decimal CalculateEarnings(int hitCount, decimal betAmount)
        {
            if (hitCount == 0)
                return -betAmount;
            if (hitCount == 2)
                return betAmount * 5;
            return 0;
        }

        private static Dictionary<string, int> CalculatePlayerRanks()
        {
            return playerEarnings
                .OrderByDescending(e => e.Value)
                .Select((e, index) => new { e.Key, Rank = index + 1 })
                .ToDictionary(p => p.Key, p => p.Rank);
        }

        private static void NotifyPlayerCallbacks(
            List<(string playerName, int hitCount, decimal earnings, decimal totalEarnings)> playerResults,
            int[] drawnNumbers,
            Dictionary<string, int> rankedPlayers)
        {
            foreach (var result in playerResults)
            {
                if (playerCallbacks.ContainsKey(result.playerName))
                {
                    var callback = playerCallbacks[result.playerName];
                    callback.NotifyDrawnNumbers(drawnNumbers, result.hitCount, result.earnings, result.totalEarnings, rankedPlayers[result.playerName]);
                }
            }
        }

        private static void LogPlayerResults(
            List<(string playerName, int hitCount, decimal earnings, decimal totalEarnings)> playerResults,
            Dictionary<string, int> rankedPlayers)
        {
            foreach (var result in playerResults)
            {
                Console.WriteLine($"Player {result.playerName}: Drawn numbers: {string.Join(", ", result.hitCount)}");
                Console.WriteLine($"Hit numbers: {result.hitCount}, Earnings: {result.earnings}, Total earnings: {result.totalEarnings}, Rank: {rankedPlayers[result.playerName]}");
            }
        }

        private static void ClearPlayers()
        {
            players.Clear();
        }
    }
}
