using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LottoDrawSimulator.Contracts
{
    public interface IPlayerCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyDrawnNumbers(int[] drawnNumbers, int hitCount, decimal earnings, decimal totalEarnings, int rank);
    }
}
