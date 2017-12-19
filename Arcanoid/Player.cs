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
        
        public Player(Vector2 center)
        {
            Body = new Queue<Vector2>();
            Body.Enqueue(center-Vector2.Left);
            Body.Enqueue(center);
        }

        public void Step(Vector2 direction,bool growUp)
        {
            Body.Enqueue(NextVertex(direction));
            if(!growUp)
                Body.Dequeue();//Если не растёт удаляем последний элемент очереди           
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
