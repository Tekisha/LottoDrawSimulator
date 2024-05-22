using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LottoDrawSimulator.Contracts
{
    [ServiceContract]
    public interface ILotteryMachineService
    {
        [OperationContract]
        void RegisterPlayer(string playerName);

        [OperationContract]
        void StartDrawing();
    }
}
