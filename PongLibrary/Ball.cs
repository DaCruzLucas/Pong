using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PongLibrary
{
    public class Ball
    {
        static Random rnd = new Random();
        Color Color;
        public float X { get; set; }
        public float Y { get; set; }
        public float D { get; set; }
        public Brush Brush { get; set; }
        public float Vx { get; set; }
        public float Vy { get; set; }

        public Ball(float x, float y, float d, float vx, float vy)
        {
            this.X = x;
            this.Y = y;
            this.D = d;
            Color = Color.Red;
            this.Brush = new SolidBrush(Color);
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
            e.FillEllipse(Brush, X, Y, D, D);
        }
    }
}
