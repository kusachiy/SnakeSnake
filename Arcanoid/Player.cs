using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcanoid
{
    class Player
    {
        public Queue<Vector2> Body { get; private set; }
        public Vector2 GetTail => Body.Peek();
        public Vector2 GetHead => Body.Last();
        public int GetLength => Body.Count;

        public Apple Apple;
        
        public Player()
        {
            Body = new Queue<Vector2>();
            Body.Enqueue(new Vector2(300, 220));
            Body.Enqueue(new Vector2(320, 220));
        }

        public void Step(Vector2 vector,bool growUp)
        {
            Body.Enqueue(NextVertex(vector));
            if(!growUp)
                Body.Dequeue();           
        }

        public Vector2 NextVertex(Vector2 vector)
        {
            var newPos = Body.Last() + vector;
            if (newPos.X == Game.WIDTH * Game.TILESIZE)
                newPos.X = 0;
            else if (newPos.X == -Game.TILESIZE)
                newPos.X = (Game.WIDTH-1)*Game.TILESIZE;
            else if (newPos.Y == Game.HEIGHT * Game.TILESIZE)
                newPos.Y = 0;
            else if (newPos.Y == -Game.TILESIZE)
                newPos.Y = (Game.HEIGHT - 1) * Game.TILESIZE;
            return newPos;
        }
    }
}
