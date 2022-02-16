namespace Connect4.Network
{
    public interface INetwork
    {
        string Receive();
        void Send(string message);
        string Stop();
    }
}