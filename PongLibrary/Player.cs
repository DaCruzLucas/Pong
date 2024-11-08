using System.Drawing;

namespace PongLibrary
{
    public class Player
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Player(int x, int y, int w, int h)
        {
            this.X = x;
            this.Y = y;
            this.Width = w;
            this.Height = h;
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

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.Blue, this.X, this.Y, Width, Height);
        }
    }
}
