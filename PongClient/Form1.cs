using Microsoft.AspNetCore.SignalR.Client;
using PongLibrary;
using System.Diagnostics;

namespace PongClient
{
    public partial class Form1 : Form
    {
        //private Server server = new Server();
        private HubConnection connection;
        private Partie partie;

        private int idPlayer = 1;
        private bool left = false;
        private bool right = false;

        Font arial60 = new Font("Arial", 60);

        public Form1()
        {
            InitializeComponent();

            JoinBTN.Location = new Point(pb.Width / 2 - JoinBTN.Width / 2, 400);
            HostBTN.Location = new Point(pb.Width / 2 - HostBTN.Width / 2, JoinBTN.Location.Y + 40);
            JoinInput.Location = new Point(pb.Width / 2 - JoinInput.Width / 2, JoinBTN.Location.Y - 30);

            this.ClientSize = new Size(650, 700);
        }

        private async void tmr_Tick(object sender, EventArgs e)
        {
            if (partie == null)
            {
                return;
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

            pb.Invalidate();
        }

        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                left = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                right = true;
            }
            else if (e.KeyCode== Keys.Escape)
            {
                await connection.DisposeAsync();
                //await server.StopServerAsync();

                partie = null;

                JoinInput.Visible = true;
                JoinBTN.Visible = true;
                HostBTN.Visible = true;

                JoinInput.Enabled = true;
                JoinBTN.Enabled = true;
                HostBTN.Enabled = true;

                Refresh();
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
            if (partie == null)
            {
                string texte = "Pong";
                SizeF sz = e.Graphics.MeasureString(texte, arial60);

                e.Graphics.DrawString(
                    texte,
                    arial60,
                    Brushes.White,
                    new PointF((pb.Width - sz.Width) / 2, 200)
                );

                return;
            }

            if (partie.player1 != null)
            {
                partie.player1.Draw(e.Graphics, Properties.Resources.plateforme);

                string texte = partie.player1.Score.ToString();
                SizeF sz = e.Graphics.MeasureString(texte, arial60);

                e.Graphics.DrawString(
                    texte,
                    arial60,
                    Brushes.Black,
                    new PointF((pb.Width - sz.Width) / 2, 100)
                );
            }

            if (partie.player2 != null)
            {
                partie.player2.Draw(e.Graphics, Properties.Resources.plateforme2);

                string texte = partie.player2.Score.ToString();
                SizeF sz = e.Graphics.MeasureString(texte, arial60);

                e.Graphics.DrawString(
                    texte,
                    arial60,
                    Brushes.Black,
                    new PointF((pb.Width - sz.Width) / 2, 500)
                );
            }

            if (partie.ball != null) partie.ball.Draw(e.Graphics);
        }

        private void StartServerConnection(string adresse)
        {
            if (adresse == "")
            {
                adresse = "localhost";
            }

            connection = new HubConnectionBuilder()
                .WithUrl($"http://{adresse}:5000/pong")
                .Build();

            connection.Closed += async (error) =>
            {
                //await Task.Delay(new Random().Next(0, 5) * 1000);
                //await connection.StartAsync();

                if (partie == null)
                {
                    return;
                }

                await connection.DisposeAsync();
                //await server.StopServerAsync();

                partie = null;

                JoinInput.Visible = true;
                JoinBTN.Visible = true;
                HostBTN.Visible = true;

                JoinInput.Enabled = true;
                JoinBTN.Enabled = true;
                HostBTN.Enabled = true;

                Refresh();
            };

            connection.On<int, int[], int[], int[]>("PartieJoined", async (id, player1, player2, ball) =>
            {
                partie = CreatePartie(id, player1, player2, ball);

                if (partie.player1 == null)
                {
                    partie.player1 = new Player(100, 30);
                    idPlayer = 1;
                }
                else if (partie.player2 == null)
                {
                    partie.player2 = new Player(100, 635);
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
                    if (partie.player1 == null)
                    {
                        partie.player1 = new Player(player1[0], player1[1], player1[2], player1[3]);
                    }
                    else
                    {
                        partie.player1.X = player1[0];
                        partie.player1.Y = player1[1];
                        partie.player1.Width = player1[2];
                        partie.player1.Height = player1[3];
                        partie.player1.Score = player1[4];
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
                        partie.player2.Score = player2[4];
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

            connection.On<int, int>("PartieRefreshScore", (score1, score2) =>
            {
                if (partie == null)
                {
                    return;
                }

                partie.player1.Score = score1;
                partie.player2.Score = score2;
            });

            connection.On("PartieDeleted", async () =>
            {
                if (partie == null)
                {
                    return;
                }

                partie = null;

                Invoke(new Action( () =>
                {
                    JoinInput.Visible = true;
                    JoinBTN.Visible = true;
                    HostBTN.Visible = true;

                    JoinInput.Enabled = true;
                    JoinBTN.Enabled = true;
                    HostBTN.Enabled = true;

                    Refresh();
                }));

                await connection.DisposeAsync();
            });

            connection.StartAsync();
        }

        private Partie CreatePartie(int id, int[] player1, int[] player2, int[] ball)
        {
            Partie p = new Partie(id);

            if (player1 != null)
            {
                p.player1 = new Player(player1[0], player1[1], player1[2], player1[3], player1[4]);
            }
            if (player2 != null)
            {
                p.player2 = new Player(player2[0], player2[1], player2[2], player2[3], player2[4]);
            }

            p.ball = new Ball(ball[0], ball[1], ball[2], ball[3], ball[4]);

            return p;
        }

        private async Task UpdatePartie()
        {
            try
            {
                int[] player1 = null;
                int[] player2 = null;

                if (idPlayer == 1 && partie.player1 != null)
                {
                    player1 = new int[] { partie.player1.X, partie.player1.Y, partie.player1.Width, partie.player1.Height, partie.player1.Score };
                }

                else if (idPlayer == 2 && partie.player2 != null)
                {
                    player2 = new int[] { partie.player2.X, partie.player2.Y, partie.player2.Width, partie.player2.Height, partie.player2.Score };
                }

                await connection.SendAsync("UpdatePartie", partie.Id, player1, player2);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private void HostBTN_Click(object sender, EventArgs e)
        {
            //server.StartServer();

            JoinInput.Visible = false;
            JoinBTN.Visible = false;
            HostBTN.Visible = false;

            JoinInput.Enabled = false;
            JoinBTN.Enabled = false;
            HostBTN.Enabled = false;

            StartServerConnection("localhost");
        }

        private void JoinBTN_Click(object sender, EventArgs e)
        {
            //await server.StopServerAsync();

            JoinInput.Visible = false;
            JoinBTN.Visible = false;
            HostBTN.Visible = false;

            JoinInput.Enabled = false;
            JoinBTN.Enabled = false;
            HostBTN.Enabled = false;

            StartServerConnection(JoinInput.Text);
        }
    }
}
