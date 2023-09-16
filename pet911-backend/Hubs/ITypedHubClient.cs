namespace pet911_backend.Hubs
{
    public interface ITypedHubClient
    {

        Task BroadcastMessage(Message message);


    }
}
