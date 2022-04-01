using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pacman
{
    
    class Game
    {
        short numberMap;
        static Dictionary<char, Type> Chars = new Dictionary<char, Type> {
            { 'O', Type.Wall},
            { '.', Type.Point},
            { '+', Type.Enemy},
            { ' ', Type.Empty},
            { '☺', Type.Player}
        };
        bool Stop = false;
        const short x = 29;
        const short y = 16;
        Cell[,] Cells = new Cell[y, x];
        Cell[,] CellsNew = new Cell[y, x];
        const short GAMESTEP = 200;
        Cell Player;

        Thread PlayerControl;
        Thread GameStep;
        public void GameStart(short numberMap)
        {
            this.numberMap = numberMap;
            Console.CursorVisible = false;
            InitGame();
            PrintGame();
            SendCellsInCellsNew();
            PlayerControl = new Thread(SwichDirectionPlayer);
            PlayerControl.Start();
            GameStep = new Thread(GamePlay);
            GameStep.Start();
        }

        private void SwichDirectionPlayer()
        {
            while (!Stop)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Player.desiredDir = Direction.Left;
                        break;
                    case ConsoleKey.UpArrow:
                        Player.desiredDir = Direction.Up;
                        break;
                    case ConsoleKey.RightArrow:
                        Player.desiredDir = Direction.Rigth;
                        break;
                    case ConsoleKey.DownArrow:
                        Player.desiredDir = Direction.Down;
                        break;
                }
            }
        }

        private void GamePlay()
        {
            while (!Stop)
            {
                Thread.Sleep(GAMESTEP);
                for (int i = 1; i < y - 1; i++)
                {
                    for (int j = 1; j < x - 1; j++)
                    {
                        Move(ref CellsNew[i, j]);
                    }
                }
                CheckNewStateCells();
                SendCellsInCellsNew();
                CheckWin();
                ClearCellsWasStep();
            }
        }

        private void ClearCellsWasStep()
        {
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    CellsNew[i, j].WasMovedInLastStep = false;
                }
            }
        }

        private bool Move(ref Cell c)
        {
            c.SwichDir();//for enemy
            if (c.dir == Direction.Null)
            {
                return false;
            }
            Cell NextC;
            Direction lastDir = c.dir;
            if (c.dir != c.desiredDir && c.dir != Direction.Null && c.desiredDir != Direction.Null)
            {
                c.dir = c.desiredDir;
            }
            bool desired = CheckDirection(ref c, out NextC);
            if (!desired)
            {
                c.dir = lastDir;
                desired = CheckDirection(ref c, out NextC);              
            }
            if (desired)
            {
                StepCell(ref NextC, c);
                if (c.WasPlayer)
                {
                    c.type = Type.Empty;
                }
                else
                {
                    c.type = Type.Point;
                }
                c.dir = Direction.Null;
                c.dir = Direction.Null;
                return true;
            }
            return false;
        }

        private bool CheckDirection(ref Cell c, out Cell next)
        {
            Direction chDir = c.dir;
            next = null;
            if (chDir != Direction.Null && !c.WasMovedInLastStep)
            {                
                if (chDir == Direction.Up)
                {
                    Cell NextC = CellsNew[c.Y - 1, c.X];
                    if (NextC.type != Type.Wall)
                    {
                        next = NextC;
                        return true;
                    }
                }
                else if (chDir == Direction.Rigth)
                {
                    Cell NextC = CellsNew[c.Y, c.X + 1];
                    if (NextC.type != Type.Wall)
                    {
                        next = NextC;
                        return true;
                    }
                }
                else if (chDir == Direction.Down)
                {
                    Cell NextC = CellsNew[c.Y + 1, c.X];
                    if (NextC.type != Type.Wall)
                    {
                        next = NextC;
                        return true;
                    }
                }
                else if (chDir == Direction.Left)
                {
                    Cell NextC = CellsNew[c.Y, c.X - 1];
                    if (NextC.type != Type.Wall)
                    {
                        next = NextC;
                        return true;
                    }
                }
            }            
            return false;
        }

        private bool StepCell(ref Cell NextC, Cell c)
        {
            if (NextC.type != Type.Wall)
            {
                NextC.type = c.type;
                NextC.dir = c.dir;
                NextC.desiredDir = c.desiredDir;
                NextC.WasMovedInLastStep = true;
                if (c.type == Type.Player)
                {
                    Player = NextC;
                    NextC.WasPlayer = true;
                }
                return true;
            }
            return false;
        }

        private void InitGame()
        {
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    char c = Map.Grounds[numberMap][i * x + j];
                    Direction dir;
                    Type cT = CkeckTypeOnSymbol(c, out dir);
                    CellsNew[i, j] = new Cell(Convert.ToInt16(j), Convert.ToInt16(i), cT, dir);
                    if (cT == Type.Player)
                    {
                        Player = CellsNew[i, j];
                        Player.WasPlayer = true;
                    }
                }
            }
        }

        private void PrintGame()
        {
            Console.Clear();
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {                    
                    ReWriteSimbol(CellsNew[i, j]);
                }
            }
        }

        public static Type CkeckTypeOnSymbol(char c, out Direction dir)
        {
            Type resultType = Chars[c];
            if (resultType == Type.Empty)
            {
                resultType = Type.Point;
            }
            dir = Direction.Null;
            if (resultType == Type.Enemy || resultType == Type.Player)
            {
                dir = GetRandomDirection();
            }
            return resultType;
        }

        public static Type CkeckTypeOnSymbol(char c)
        {
            return Chars[c];
        }

        public static char CkeckSimbolOnType(Type t)
        {
            return Chars.FirstOrDefault(x => x.Value == t).Key;
        }

        public static ConsoleColor CkeckColorOnType(Type t)
        {
            ConsoleColor result = ConsoleColor.Black;

            switch (t)
            {
                case Type.Wall:
                    result = ConsoleColor.DarkGray;
                    break;
                case Type.Player:
                    result = ConsoleColor.Yellow;
                    break;
                case Type.Point:
                    result = ConsoleColor.Green;
                    break;
                case Type.Empty:
                    result = ConsoleColor.Gray;
                    break;
                case Type.Enemy:
                    result = ConsoleColor.Red;
                    break;
            }
            return result;
        }

        private static Direction GetRandomDirection()
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
                default: result = Direction.Null;
                    break;
            }
            return result;
        }

        private void CheckNewStateCells()
        {
            for (int i = 1; i < y - 1; i++)
            {
                for (int j = 1; j < x - 1; j++)
                {
                    Type t1 = Cells[i, j].type;
                    Type t2 = CellsNew[i, j].type;
                    if (t1 == Type.Player && t2 == Type.Enemy || t1 == Type.Enemy && t2 == Type.Player)
                    {
                        Loose();
                    }
                    else if (t1 != t2)
                    {
                        ReWriteSimbol(CellsNew[i, j]);
                    }
                }
            }
        }

        private void ReWriteSimbol(Cell c)
        {
            Console.SetCursorPosition(c.X, c.Y);
            Console.ForegroundColor = CkeckColorOnType(c.type);
            Console.Write(CkeckSimbolOnType(c.type));
        }

        private void SendCellsInCellsNew()
        {
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Cells[i, j] = new Cell(CellsNew[i, j]);
                }
            }
        }

        private void CheckWin()
        {
            int countPoint = 0;
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (!Cells[i, j].WasPlayer)
                    {
                        countPoint++;
                    }
                }
            }
            if (countPoint == 0)
            {
                Win();
            }
        }

        private void Win()
        {
            Stop = true;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Победа!");
            Console.ReadKey();
        }
        
        private void Loose()
        {
            Stop = true;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Поражение!");
            Console.ReadKey();
        }
    }
}
