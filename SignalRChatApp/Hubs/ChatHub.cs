using Microsoft.AspNetCore.SignalR;
using SignalRChatApp.Data;
using SignalRChatApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task GetUserId(string userId)
        {
            Client client = new Client()
            {
                ConnectionId = Context.ConnectionId,
                UserId = userId
            };
            ClientSource.Clients.Add(client);
            await Clients.Others.SendAsync("clientJoined", userId);
        }

        public async Task SendMessageAsync(string message, string userId)
        {
            Client client = ClientSource.Clients.FirstOrDefault(x => x.UserId == userId);

            //await Clients.All.SendAsync("receiveMessage", message);//bütün clientlar
            //await Clients.Others.SendAsync("receiveMessage", message);//Diğer clientlar

            await Clients.Clients(client.ConnectionId).SendAsync("receiveMessage", message, client.UserId);//hedef clienttaki ilgili fonksiyon
        }
    }
}
