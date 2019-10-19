using System.Threading.Tasks;

namespace EchantionCodeChallenge.Client
{
    interface IClient
    {
        Task<IResponseMessage> SendAsync(IRequestMessage requestMessage);
    }


}
