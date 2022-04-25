using Microsoft.AspNetCore.SignalR;
using Osztott_szavazasi_rendszer.Models;

namespace Osztott_szavazasi_rendszer
{
    public class VoteHub : Hub
    {
        public async Task SendVote(SubmittedForm submittedForm)
        {
            await Clients.All.SendAsync("UpdateVote", submittedForm, submittedForm.FormId);
        }
    }
}
