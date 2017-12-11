using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arcanoid
{
    public struct Vector2
    { 

        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int v1, int v2)
        {
            X = v1;
            Y = v2;
        }
        public static Vector2 Left => new Vector2(-1, 0);
        public static Vector2 Right => new Vector2(1, 0);
        public static Vector2 Up => new Vector2(0, -1);
        public static Vector2 Down => new Vector2(0, 1);
        public static Vector2 Zero => new Vector2(0, 0);

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }        

        public static bool operator ==(Vector2 v1, Vector2 v2) => Equals(v1, v2);

        public static bool operator !=(Vector2 v1, Vector2 v2) => !Equals(v1, v2);


        public static Vector2 operator *(Vector2 v1, int size)
        {
            return new Vector2(v1.X *size, v1.Y *size);
        }
       
        public Rectangle ToRectangle() => new Rectangle(X, Y, Game.TILESIZE, Game.TILESIZE);

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
            {
                return false;
            }

            var vector = (Vector2)obj;
            return X == vector.X &&
                   Y == vector.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
    class Game
    {
        public const int WIDTH = 32;
        public const int HEIGHT = 24;
        public const int TILESIZE = 20;
        public const int SPEED = 300;


        public Player Player { get; private set; }
        public Apple Apple { get; private set; }
        public Vector2 Direction { get { return _direction; }
            set
            {
                if (value == Vector2.Zero - _oldDirection)
                    return;
                else
                    _direction = value;
            }
        }

        private Vector2 _direction;
        private Vector2 _oldDirection;

        public Game()
        {
            Player = new Player();
            Apple = new Apple(new Vector2(100, 60));
            Direction = Vector2.Right;
        }
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(SPEED);                
                var vertex = Player.NextVertex(Direction * TILESIZE);
                if (vertex == Apple.Position)
                {
                    Player.Step(Direction * TILESIZE, true);
                    Apple = new Apple(GetRandom());
                }
                else
                    Player.Step(Direction * TILESIZE, false);
                if (PlayerIsCrossed())
                {
                    EndGame();
                    return;
                }
                _oldDirection = Direction;
                ClearBoard();
                DrawPalyer(Player);
                DrawApple(Apple);
            }
        }

        private bool PlayerIsCrossed()
        {
            var head = Player.GetHead;
            for (int i = 0; i < Player.Body.Count-1; i++)
            {
                if (Player.Body.ElementAt(i) == head)
                    return true;
            }
            return false;
        }

        private bool PlayerIntersectApple()
        {
            var head = Player.GetHead;
            return head == Apple.Position;
        }

        public static Vector2 GetRandom()
        {
            Random rnd = new Random();
            return new Vector2(rnd.Next(0,WIDTH)*TILESIZE, rnd.Next(0,HEIGHT)*TILESIZE);
        }

        public event Action ClearBoard;

        public event Action EndGame;


        public event Action<Player> DrawPalyer;

        public event Action<Apple> DrawApple;


    }
}
