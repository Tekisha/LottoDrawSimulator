using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LottoDrawSimulator.Contracts
{
    [ServiceContract(CallbackContract = typeof(IPlayerCallback))]
    public interface IPlayerService
    {
        [OperationContract]
        string InitPlayer(int number1, int number2, decimal amount);
    }
}
