namespace Connect4.Interfaces
{
    public interface INetwork
    {
        string IP { get; set; }

        string Receive();
        void Send(string message);
        string Start();
        string Stop();
    }
}