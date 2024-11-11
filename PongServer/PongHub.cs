using Microsoft.AspNetCore.SignalR;
using PongLibrary;

namespace PongServer
{
    public class PongHub : Hub
    {
        private readonly PongService ps;

        public PongHub(PongService drawingService)
        {
            ps = drawingService;
        }

        public override async Task OnConnectedAsync()
        {
            Partie partie = ps.FindParty();

            await Groups.AddToGroupAsync(Context.ConnectionId, partie.Id.ToString());

            Console.WriteLine($"Connection {Context.ConnectionId} joined partie {partie.Id}");

            int[] player1 = null;
            int[] player2 = null;

            if (partie.player1 != null)
            {
                player1 = new int[] { partie.player1.X, partie.player1.Y, partie.player1.Width, partie.player1.Height };
            }

            if (partie.player2 != null)
            {
                player2 = new int[] { partie.player2.X, partie.player2.Y, partie.player2.Width, partie.player2.Height };
            }
            
            int[] ball = new int[] { partie.ball.X, partie.ball.Y, partie.ball.D, partie.ball.Vx, partie.ball.Vy };

            await Clients.Caller.SendAsync("PartieJoined", partie.Id, player1, player2, ball);

            await base.OnConnectedAsync();
        }

        public async Task UpdatePartie(int id, int[] player1, int[] player2)
        {
            //Console.WriteLine($"Connection {Context.ConnectionId} updated partie {id}")

            ps.UpdatePartie(id, player1, player2);

            Partie partie = ps.GetPartie(id);

            int[] ball = new int[] { partie.ball.X, partie.ball.Y, partie.ball.D, partie.ball.Vx, partie.ball.Vy };

            await Clients.GroupExcept(id.ToString(), Context.ConnectionId).SendAsync("PartieRefresh", player1, player2, ball);
        }

        public async Task GetPartie(int id)
        {
            Partie partie = ps.GetPartie(id);

            int[] player1 = null;
            int[] player2 = null;

            if (partie.player1 != null)
            {
                player1 = new int[] { partie.player1.X, partie.player1.Y, partie.player1.Width, partie.player1.Height };
            }

            if (partie.player2 != null)
            {
                player2 = new int[] { partie.player2.X, partie.player2.Y, partie.player2.Width, partie.player2.Height };
            }

            int[] ball = new int[] { partie.ball.X, partie.ball.Y, partie.ball.D, partie.ball.Vx, partie.ball.Vy };

            await Clients.Caller.SendAsync("PartieRefresh", player1, player2, ball);
        }
    }
}
