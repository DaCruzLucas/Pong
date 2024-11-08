using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
