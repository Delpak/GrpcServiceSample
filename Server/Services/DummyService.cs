using System.Threading.Tasks;
using DummyGrpcService;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GrpcService
{
    public class DummyService : DummyResponder.DummyResponderBase
    {
        private readonly ILogger<DummyService> _logger;
        public DummyService(ILogger<DummyService> logger)
        {
            _logger = logger;
            
        }

        public override async Task<Response> Ripost(Request request, ServerCallContext context)
        {
            switch (request.Message.ToLower())
            {
                case "hello":
                    await Task.Delay(1000);
                    return new Response
                    {
                        Message = $"Hi (Connection_Id: {context.GetHttpContext().Connection.Id})"
                    };
                case "bye":
                    context.Status = new Status(StatusCode.Aborted, $"Bye (Connection_Id: {context.GetHttpContext().Connection.Id})");
                    return new Response
                    {
                        Message = "Bye"
                    };
                case "ping":
                    return new Response
                    {
                        Message = "Pong"
                    };
                case "close":
                    return new Response { Message = "Closed" };
                default:
                    context.Status = new Status(StatusCode.InvalidArgument, "Only this commands are allowed: [Hello,Bye,Ping]");
                    return new Response
                    {
                        Message = "Invalid Message"
                    };
            }

        }

        Task closeConnection(HttpContext httpContext)
        {
            return Task.Run(() =>
            {
                Task.Delay(1000).Wait();
                
                httpContext.Abort();
            });

        }
    }
}
