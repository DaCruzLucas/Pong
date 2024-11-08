using System.Drawing;

namespace PongLibrary
{
    public class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int D { get; set; }
        public int Vx { get; set; }
        public int Vy { get; set; }

        public Ball(int x, int y, int d, int vx, int vy)
        {
            this.X = x;
            this.Y = y;
            this.D = d;
            this.Vx = vx;
            this.Vy = vx;
        }

        public void Update(int w, int h)
        {
            this.X += this.Vx;
            this.Y += this.Vy;

            if (this.X >= w - this.D)
            {
                this.X = w - this.D;
                this.Vx *= -1;
            }
            if (this.X <= 0)
            {
                this.X = 0;
                this.Vx *= -1;
            }
            if (this.Y >= h - this.D)
            {
                this.Y = h - this.D;
                this.Vy *= -1;
            }
            if (this.Y <= 0)
            {
                this.Y = 0;
                this.Vy *= -1;
            }
        }
        public void Draw(Graphics e)
        {
            e.FillEllipse(Brushes.Red, X, Y, D, D);
        }
    }
}
