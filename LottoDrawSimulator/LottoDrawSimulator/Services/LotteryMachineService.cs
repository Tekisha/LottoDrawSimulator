﻿using LottoDrawSimulator.Contracts;
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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LotteryMachineService : ILotteryMachineService
    {
        public void ReceiveDrawnNumbers(int[] drawnNumbers)
        {
            Console.WriteLine($"Received drawn numbers: {string.Join(", ", drawnNumbers)}");
            PlayerService.NotifyPlayers(drawnNumbers);
        }
    }
}
