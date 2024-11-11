using Microsoft.AspNetCore.SignalR.Client;
using PongLibrary;
using System.Diagnostics;

namespace PongClient
{
    public partial class Form1 : Form
    {
        HubConnection connection;

        private Partie partie;

        private int idPlayer = 1;

        private bool left = false;
        private bool right = false;
        public Form1()
        {
            InitializeComponent();

            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/pong")
                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };



            connection.On<int, int[], int[], int[]>("PartieJoined", async (id, player1, player2, ball) =>
            {
                partie = CreatePartie(id, player1, player2, ball);

                if (partie.player1 == null)
                {
                    partie.player1 = new Player(100, 30, 100, 30);
                    idPlayer = 1;
                }
                else if (partie.player2 == null)
                {
                    partie.player2 = new Player(100, 635, 100, 30);
                    idPlayer = 2;
                }

                await UpdatePartie();
            });

            connection.On<int[], int[]>("PartieRefresh", (player1, player2) =>
            {
                if (partie == null)
                {
                    return;
                }

                if (player1 != null)
                {
                    if (player1 == null)
                    {
                        partie.player1 = new Player(player1[0], player1[1], player1[2], player1[3]);
                    }
                    else
                    {
                        partie.player1.X = player1[0];
                        partie.player1.Y = player1[1];
                        partie.player1.Width = player1[2];
                        partie.player1.Height = player1[3];
                    }
                }

                if (player2 != null)
                {
                    if (partie.player2 == null)
                    {
                        partie.player2 = new Player(player2[0], player2[1], player2[2], player2[3]);
                    }
                    else
                    {
                        partie.player2.X = player2[0];
                        partie.player2.Y = player2[1];
                        partie.player2.Width = player2[2];
                        partie.player2.Height = player2[3];
                    }
                }
            });

            connection.On<int[]>("PartieRefreshBall", (ball) =>
            {
                if (partie == null)
                {
                    return;
                }

                partie.ball.X = ball[0];
                partie.ball.Y = ball[1];
                partie.ball.D = ball[2];
                partie.ball.Vx = ball[3];
                partie.ball.Vy = ball[4];
            });

            connection.StartAsync();

            this.ClientSize = new Size(650, 700);

            //partie.player1 = new Player(100, 30, 100, 30);
            //partie.player2 = new Player(100, 635, 100, 30);

            //partie.ball = new Ball(250, 250, 30, 10, 7);
        }

        private async void tmr_Tick(object sender, EventArgs e)
        {
            if (partie == null)
            {
                return;
            }
            else
            {
                //await connection.SendAsync("GetPartie", partie.Id);
            }

            if (idPlayer == 1)
            {
                if (left)
                {
                    partie.player1.MoveLeft(10);
                    await UpdatePartie();
                }
                else if (right)
                {
                    partie.player1.MoveRight(10, pb.Width);
                    await UpdatePartie();
                }
            }
            else if (idPlayer == 2)
            {
                if (left)
                {
                    partie.player2.MoveLeft(10);
                    await UpdatePartie();
                }
                else if (right)
                {
                    partie.player2.MoveRight(10, pb.Width);
                    await UpdatePartie();
                }
            }

            //partie.ball.Update(pb.Width, pb.Height);

            pb.Invalidate();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                left = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                right = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                left = false;
            }
            else if (e.KeyCode == Keys.D)
            {
                right = false;
            }
        }

        private void pb_Paint(object sender, PaintEventArgs e)
        {
            if (partie == null) return;

            if (partie.player1 != null) partie.player1.Draw(e.Graphics);

            if (partie.player2 != null) partie.player2.Draw(e.Graphics);

            if (partie.ball != null) partie.ball.Draw(e.Graphics);
        }

        private Partie CreatePartie(int id, int[] player1, int[] player2, int[] ball)
        {
            Partie p = new Partie(id);

            if (player1 != null)
            {
                p.player1 = new Player(player1[0], player1[1], player1[2], player1[3]);
            }
            if (player2 != null)
            {
                p.player2 = new Player(player2[0], player2[1], player2[2], player2[3]);
            }

            p.ball = new Ball(ball[0], ball[1], ball[2], ball[3], ball[4]);

            return p;
        }

        private async Task UpdatePartie()
        {
            int[] player1 = null;
            int[] player2 = null;

            if (idPlayer == 1 && partie.player1 != null)
            {
                player1 = new int[] { partie.player1.X, partie.player1.Y, partie.player1.Width, partie.player1.Height };
            }

            else if (idPlayer == 2 && partie.player2 != null)
            {
                player2 = new int[] { partie.player2.X, partie.player2.Y, partie.player2.Width, partie.player2.Height };
            }

            await connection.SendAsync("UpdatePartie", partie.Id, player1, player2);
        }

        private void HostBTN_Click(object sender, EventArgs e)
        {

        }

        private void JoinBTN_Click(object sender, EventArgs e)
        {

        }
    }
}
