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

        public int Collide(Player p)
        {
            if (this.X + this.D >= p.X && this.X <= p.X + p.Width &&
                this.Y + this.D >= p.Y && this.Y <= p.Y + p.Height)
                {
                    int distHaut = Math.Abs((this.Y + this.D) - p.Y);
                    int distBas = Math.Abs(this.Y - (p.Y + p.Height));
                    int distGauche = Math.Abs((this.X + this.D) - p.X);
                    int distDroite = Math.Abs(this.X - (p.X + p.Width));

                    // Collision verticale (haut ou bas)
                    if ((distHaut < distGauche && distHaut < distDroite) ||
                        (distBas < distGauche && distBas < distDroite))
                    {
                        return (distHaut < distBas) ? 1 : 2; // 1 pour haut, 2 pour bas
                    }
                    // Collision horizontale (gauche ou droite)
                    else
                    {
                        return (distGauche < distDroite) ? 3 : 4; // 3 pour gauche, 4 pour droite
                    }
                }

            return 0;
        }

        public void Draw(Graphics e)
        {
            e.FillEllipse(Brushes.Red, X, Y, D, D);
        }
    }
}
