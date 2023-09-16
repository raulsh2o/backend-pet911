using Microsoft.AspNetCore.SignalR;

namespace pet911_backend.Hubs
{
    public class NotifyHub: Hub<ITypedHubClient>
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
        
    }
}
