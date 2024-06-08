using Microsoft.AspNetCore.SignalR;

namespace ProjetASI.Hubs
{
    public class ProjectHub : Hub
    {
        public async Task TableUpdate()
        {
            await Clients.All.SendAsync("updateTableIndex");
        }

        public async Task CommandeUpdate()
        {
            await Clients.All.SendAsync("updateCommandeIndex");
        }

        public async Task FactureUpdate()
        {
            await Clients.All.SendAsync("updateFactureIndex");
        }

        public async Task EncaissementUpdate()
        {
            await Clients.All.SendAsync("updateEncaissementIndex");

        }
    }
}
