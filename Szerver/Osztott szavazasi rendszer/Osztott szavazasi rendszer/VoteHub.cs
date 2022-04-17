using Microsoft.AspNetCore.SignalR;

namespace Osztott_szavazasi_rendszer
{
    public class VoteHub : Hub
    {
        public async Task SendVote(string user, string vote)
        {
            await Clients.All.SendAsync("UpdateVote", user, vote);
        }
    }
}
