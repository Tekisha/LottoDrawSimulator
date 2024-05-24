using PlayerClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlayerClient
{
    public class PlayerCallback : IPlayerServiceCallback
    {
        public static ManualResetEvent NotificationReceived = new ManualResetEvent(false);
        public void NotifyDrawnNumbers(int[] drawnNumbers, int hitCount, decimal earnings, decimal totalEarnings, int rank)
        {
            Console.WriteLine($"Drawn numbers: {string.Join(", ", drawnNumbers)}");
            Console.WriteLine($"Hit numbers: {hitCount}, Earnings: {earnings}, Total Earnings: {totalEarnings}, Rank: {rank}");
            NotificationReceived.Set();
        }
    }
}
