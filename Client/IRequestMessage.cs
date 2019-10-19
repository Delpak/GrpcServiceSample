namespace EchantionCodeChallenge.Client
{
    public interface IRequestMessage
    {
        string Message { get; set; }
    }

    public class RequestMessage : IRequestMessage
    {
       public string Message { get ; set; }
    }
}