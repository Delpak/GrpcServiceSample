namespace EchantionCodeChallenge.Client
{
    public interface IResponseMessage
    {
        string Message { get; set; }
    }
    public class ResponseMessage : IResponseMessage
    {
        public string Message { get; set; }
    }
}