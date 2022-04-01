using System;
using System.Collections.Generic;

namespace Pacman
{
    public enum Type
    {
        Player,
        Enemy,
        Wall,
        Point,
        Empty
    }

    public enum Direction
    {
        Up,
        Rigth,
        Down,
        Left,
        Null
    }


    class Program
    {
        static void Main(string[] args)
        {
            Map.InitGrounds();

            int numberMap = 0;
            do
            {
                try
                {
                    Console.WriteLine("Выберите и введите номер карты (от 1 до " + Map.Grounds.Length.ToString() + ")");
                    numberMap = Convert.ToInt32(Console.ReadLine());
                    if (numberMap > Map.Grounds.Length || numberMap < 1)
                    {
                        throw new Exception("Введите корректный номер карты");
                    }
                }
                catch ( Exception ex ){ 
                    Console.Clear();
                    Console.WriteLine(ex.Message.ToString());
                    numberMap = 0;
                }
            } while (numberMap < 1);
            
            Game gameMain = new Game();
            gameMain.GameStart(Convert.ToInt16(numberMap - 1));
        }        
    }
}
