using System.Timers;
using Microsoft.AspNetCore.SignalR;
using PongLibrary;

namespace PongServer
{
    public class PongService
    {
        private readonly IHubContext<PongHub> hub;
        private readonly System.Timers.Timer timer = new System.Timers.Timer(20);

        private Dictionary<int, Partie> Parties = new Dictionary<int, Partie>();

        private int id = 0;

        public PongService(IHubContext<PongHub> hub)
        {
            this.hub = hub;

            timer.Elapsed += Tick;
            timer.Start();
        }

        private async void Tick(object sender, ElapsedEventArgs e)
        {
            //Console.WriteLine("Tick! The time is {0:HH:mm:ss.fff}", e.SignalTime);

            foreach (Partie partie in Parties.Values)
            {
                partie.ball.Update(650, 700);

                if (partie.player1 != null)
                {
                    if (partie.ball.Collide(partie.player1))
                    {
                        //partie.ball.Vx = -partie.ball.Vx;
                        partie.ball.Vy = -partie.ball.Vy;
                    }
                }

                if (partie.player2 != null)
                {
                    if (partie.ball.Collide(partie.player2))
                    {
                        //partie.ball.Vx = -partie.ball.Vx;
                        partie.ball.Vy = -partie.ball.Vy;
                    }
                }

                int[] ball = new int[] { partie.ball.X, partie.ball.Y, partie.ball.D, partie.ball.Vx, partie.ball.Vy };

                await hub.Clients.Group(id.ToString()).SendAsync("PartieRefreshBall", ball);
            }   
        }

        private int NextId()
        {
            return ++id;
        }

        public Partie CreatePartie()
        {
            var partie = new Partie(NextId());
            Parties.Add(partie.Id, partie);
            return partie;
        }

        public void UpdatePartie(int id, int[] player1, int[] player2)
        {
            if (player1 != null)
            {
                if (Parties[id].player1 == null)
                {
                    Parties[id].player1 = new Player(player1[0], player1[1], player1[2], player1[3]);
                }
                else
                {
                    Parties[id].player1.X = player1[0];
                    Parties[id].player1.Y = player1[1];
                    Parties[id].player1.Width = player1[2];
                    Parties[id].player1.Height = player1[3];
                }
            }

            if (player2 != null)
            {
                if (Parties[id].player2 == null)
                {
                    Parties[id].player2 = new Player(player2[0], player2[1], player2[2], player2[3]);
                }
                else
                {
                    Parties[id].player2.X = player2[0];
                    Parties[id].player2.Y = player2[1];
                    Parties[id].player2.Width = player2[2];
                    Parties[id].player2.Height = player2[3];
                }
            }
        }

        public void DeletePartie(int id)
        {
            Parties.Remove(id);
        }

        public Partie GetPartie(int id)
        {
            return Parties[id];
        }

        public Partie FindParty()
        {
            foreach (Partie partie in Parties.Values)
            {
                if (partie.player1 == null || partie.player2 == null)
                {
                    return partie;
                }
            }

            return CreatePartie();
        }
    }
}
