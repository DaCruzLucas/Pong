using PongLibrary;
using System.Windows.Forms;

namespace PongClient
{
    public partial class Form1 : Form
    {
        private Player player1;
        private Player player2;

        private int idPlayer = 1;

        List<Ball> balls = new List<Ball>();

        private bool left = false;
        private bool right = false;
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new Size(650, 700);

            player1 = new Player(100, 30, 100, 30);
            player2 = new Player(100, 635, 100, 30);

            balls.Add(new Ball(250, 250, 30, 10, 7));
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            if(idPlayer == 1)
            {
                if (left)
                {
                    player1.MoveLeft(10);
                }
                else if (right)
                {
                    player1.MoveRight(10, pb.Width);
                }
            }else if (idPlayer == 2)
            {
                if (left)
                {
                    player2.MoveLeft(10);
                }
                else if (right)
                {
                    player2.MoveRight(10, pb.Width);
                }
            }

            foreach (var b in balls)
            {
                b.Update(pb.Width, pb.Height);
            }

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
            player1.Draw(e.Graphics);
            player2.Draw(e.Graphics);

            foreach (var b in balls)
            {
                b.Draw(e.Graphics);
            }
        }

        
    }
}
