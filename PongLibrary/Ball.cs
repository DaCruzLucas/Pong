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

                // Collision verticale (haut)
                if (distHaut < distBas && distHaut < distGauche && distHaut < distDroite)
                {
                    return 1; // 1 pour haut
                }
                // Collision verticale (bas)
                else if (distBas < distHaut && distBas < distGauche && distBas < distDroite)
                {
                    // Détermine la section de la collision en bas
                    int section = p.Width / 5;

                    if (this.X < p.X + section) // Très à gauche
                    {
                        //Console.WriteLine("Très à gauche");
                        return 21; // 21 pour bas très à gauche
                    }
                    else if (this.X < p.X + 2 * section) // Gauche
                    {
                        //Console.WriteLine("Gauche");
                        return 22; // 22 pour bas gauche
                    }
                    else if (this.X < p.X + 3 * section) // Milieu
                    {
                        //Console.WriteLine("Milieu");
                        return 23; // 23 pour bas milieu
                    }
                    else if (this.X < p.X + 4 * section) // Droite
                    {
                        //Console.WriteLine("Droite");
                        return 24; // 24 pour bas droite
                    }
                    else // Très à droite
                    {
                        //Console.WriteLine("Très à droite");
                        return 25; // 25 pour bas très à droite
                    }
                }
                // Collision horizontale (gauche)
                else if (distGauche < distDroite)
                {
                    return 3; // 3 pour gauche
                }
                // Collision horizontale (droite)
                else
                {
                    return 4; // 4 pour droite
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
