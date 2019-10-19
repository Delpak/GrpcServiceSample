using Grpc.Net.Client;
using System.Threading.Tasks;
using DummyGrpcService;
using Grpc.Core;
using System;

namespace EchantionCodeChallenge.Client
{
    public class Client : IClient , IDisposable
    {
        DummyResponder.DummyResponderClient dummyResponderClient;
        GrpcChannel channel;
        public Client()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            
            dummyResponderClient = new DummyResponder.DummyResponderClient(channel);
        }

        public void Dispose()
        {
            channel.Dispose();
        }

        public async Task<IResponseMessage> SendAsync(IRequestMessage requestMessage)
        {
            try
            {
                var response = await dummyResponderClient.RipostAsync(new Request() { Message = requestMessage.Message }, null);
                
                return new ResponseMessage() { Message = response.Message };
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.InvalidArgument)
            {
                return new ResponseMessage() { Message = ex.Status.Detail };
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Aborted)
            {
                channel.Dispose();
                channel = GrpcChannel.ForAddress("https://localhost:5001");

                dummyResponderClient = new DummyResponder.DummyResponderClient(channel);

                return new ResponseMessage() { Message = ex.Status.Detail };
            }
        }

    }


}
