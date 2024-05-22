using PlayerClient.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerClient
{
    public class PlayerCallback : IPlayerServiceCallback
    {
        public void NotifyDrawnNumbers(int[] drawnNumbers)
        {
            Console.WriteLine($"Drawn numbers: {string.Join(", ", drawnNumbers)}");
        }
    }
}
