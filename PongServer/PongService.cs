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
        private string HostId;

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

            for (int i = 0; i < Parties.Values.Count; i++)
            {
                Partie partie = Parties.Values.ElementAt(i);
                partie.ball.Update(650, 700);

                if (partie.player1 != null)
                {
                    Collide(partie, partie.player1);
                }

                if (partie.player2 != null)
                {
                    Collide(partie, partie.player2);
                }

                int[] ball = new int[] { partie.ball.X, partie.ball.Y, partie.ball.D, partie.ball.Vx, partie.ball.Vy };
                
                await hub.Clients.Group(partie.Id.ToString()).SendAsync("PartieRefreshBall", ball);
            }
        }

        private void Collide(Partie partie, Player player)
        {
            int collision = partie.ball.Collide(partie.player1);

            if (collision == 1)
            {
                partie.ball.Vy = -partie.ball.Vy;
                partie.ball.Y = player.Y - partie.ball.D - 1;
            }

            else if (collision == 21) // très à gauche
            {
                if (partie.ball.Vx >= -8)
                {
                    partie.ball.Vx -= 5;
                    Console.WriteLine("très à gauche -5");
                }
                partie.ball.Vy = -partie.ball.Vy;
                partie.ball.Y = player.Y + player.Height + 1;
            }
            else if (collision == 22) // gauche
            {
                if (partie.ball.Vx >= -8)
                {
                    partie.ball.Vx -= 3;
                    Console.WriteLine("gauche -3");
                }
                partie.ball.Vy = -partie.ball.Vy;
                partie.ball.Y = player.Y + player.Height + 1;
            }
            else if (collision == 23) // millieu
            {
                if (partie.ball.Vx <= -3)
                {
                    partie.ball.Vx += 4;
                    Console.WriteLine("millieu +4");
                }
                else if (partie.ball.Vx >= 3)
                {
                    partie.ball.Vx -= 4;
                    Console.WriteLine("millieu -4");
                }
                partie.ball.Vy = -partie.ball.Vy;
                partie.ball.Y = player.Y + player.Height + 1;
            }
            else if (collision == 24) // droite
            {
                if (partie.ball.Vx <= 8)
                {
                    partie.ball.Vx += 3;
                    Console.WriteLine("droite +3");
                }
                partie.ball.Vy = -partie.ball.Vy;
                partie.ball.Y = player.Y + player.Height + 1;
            }
            else if (collision == 25) // très à droite
            {
                if (partie.ball.Vx <= -8)
                {
                    partie.ball.Vx += 5;
                    Console.WriteLine("très à droite +5");
                }
                partie.ball.Vy = -partie.ball.Vy;
                partie.ball.Y = player.Y + player.Height + 1;
            }

            else if (collision == 3)
            {
                partie.ball.Vx = -partie.ball.Vx;
                partie.ball.X = player.X - partie.ball.D - 1;
            }
            else if (collision == 4)
            {
                partie.ball.Vx = -partie.ball.Vx;
                partie.ball.X = player.X + player.Width + 1;
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

        public void UpdatePartie(int id, int[] player1, int[] player2, string connectionId)
        {
            if (player1 != null)
            {
                if (Parties[id].player1 == null)
                {
                    Parties[id].player1 = new Player(player1[0], player1[1], player1[2], player1[3], player1[4]);
                    Parties[id].player1.ConnectionId = connectionId;
                }
                else
                {
                    Parties[id].player1.X = player1[0];
                    Parties[id].player1.Y = player1[1];
                    Parties[id].player1.Width = player1[2];
                    Parties[id].player1.Height = player1[3];
                    Parties[id].player1.Score = player1[4];
                }
            }

            if (player2 != null)
            {
                if (Parties[id].player2 == null)
                {
                    Parties[id].player2 = new Player(player2[0], player2[1], player2[2], player2[3], player2[4]);
                    Parties[id].player2.ConnectionId = connectionId;
                }
                else
                {
                    Parties[id].player2.X = player2[0];
                    Parties[id].player2.Y = player2[1];
                    Parties[id].player2.Width = player2[2];
                    Parties[id].player2.Height = player2[3];
                    Parties[id].player2.Score = player2[4];
                }
            }
        }

        public void DeletePartie(int id)
        {
            Parties.Remove(id);
        }

        public Partie GetPartie(int id)
        {
            return Parties.ContainsKey(id) ? Parties[id] : null;
        }

        public Partie FindParty()
        {
            for (int i = 0; i < Parties.Values.Count; i++)
            {
                Partie partie = Parties.Values.ElementAt(i);

                if (partie.player1 == null || partie.player2 == null)
                {
                    return partie;
                }
            }

            return CreatePartie();
        }

        public int RemovePlayerFromPartie(string connectionId)
        {
            for (int i = 0; i < Parties.Values.Count; i++)
            {
                Partie partie = Parties.Values.ElementAt(i);

                if (partie.player1 != null && partie.player1.ConnectionId == connectionId)
                {
                    partie.player1 = null;
                    return partie.Id;
                }

                if (partie.player2 != null && partie.player2.ConnectionId == connectionId)
                {
                    partie.player2 = null;
                    return partie.Id;
                }
            }

            return -1;
        }

        public void SetHostId(string id)
        {
            HostId = id;
        }

        public string GetHostId()
        {
            return HostId;
        }

        public void RemovePartie(int partieId)
        {
            Parties.Remove(partieId);
        }
    }
}
