using System.Drawing;
using System.Threading.Tasks.Sources;

namespace PongLibrary
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Score { get; set; }
        public string ConnectionId { get; set; }

        public Player(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Width = 120;
            this.Height = 20;
            this.Score = 0;
        }

        public Player(int x, int y, int w, int h, int score = 0) // NE PAS TOUCHE ICI
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
            this.Score = score;
        }

        public void MoveLeft(int v)
        {
            if(this.X <= 0)
            {
                this.X = 0;
            }
            else
            {
                this.X -= v;
            }
        }

        public void MoveRight(int v, int pbW)
        {
            if(this.X >= pbW - this.Width)
            {
                this.X = pbW - this.Width;
            }
            else
            {
                this.X += v;
            }
        }

        public void Draw(Graphics g, Image image)
        {
            g.DrawImage(image, new Rectangle(this.X, this.Y, Width, Height));
            //g.FillRectangle(Brushes.Blue, this.X, this.Y, Width, Height);
        }
    }
}
