using System;
using System.Collections.Generic;
using System.Text;

namespace Pacman
{
    class Cell
    {
        public short X;
        public short Y;
        public Type type;     
        public Direction dir;
        public Direction desiredDir;
        public bool WasPlayer = false;        
        public bool WasMovedInLastStep = false;        

        public Cell(short X, short Y, Type T = Type.Empty, Direction D = Direction.Null)
        {
            this.X = X;
            this.Y = Y;
            this.type = T;
            this.dir = D;
            this.desiredDir = D;
        }

        public Cell(Cell c)
        {
            this.X = c.X;
            this.Y = c.Y;
            this.type = c.type;
            this.dir = c.dir;
            this.desiredDir = c.desiredDir;
            this.WasPlayer = c.WasPlayer;
            this.WasMovedInLastStep = c.WasMovedInLastStep;
        }

        public void SwichDir()
        {
            if (this.dir != Direction.Null && this.type != Type.Player)
            {
                Random r = new Random();
                int temp = r.Next(0, 100);
                if (temp > 60)
                {
                    this.dir = GetRandomDirection();
                }
            }
        }

        private Direction GetRandomDirection()
        {
            Direction result;
            Random r = new Random();
            int randNum = r.Next(0, 4);
            switch (randNum)
            {
                case 0:
                    result = Direction.Down;
                    break;
                case 1:
                    result = Direction.Left;
                    break;
                case 2:
                    result = Direction.Rigth;
                    break;
                case 3:
                    result = Direction.Up;
                    break;
                default:
                    result = Direction.Null;
                    break;
            }
            return result;
        }
    }
}
