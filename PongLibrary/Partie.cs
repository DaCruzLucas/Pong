using System.Timers;

namespace PongLibrary
{
    public class Partie
    {
        public int Id;

        public Player player1;
        public Player player2;

        public Ball ball;

        public Partie(int id)
        {
            Id = id;
            ball = new Ball(250, 250);
        }
    }
}
