using System;
using System.Collections.Generic;
using System.Threading;

namespace _3d_in_Console
{
    class Program
    {
        static int x = 200;
        static int y = 100;
        static Camera MainCamera;

        static void Main(string[] args)
        {            
            Console.WriteLine(TextFileWorker.GetReadMeFile("ReadMe.txt"));
            Console.ReadLine();
            Console.CursorVisible = false;
            Console.BufferWidth = x;
            Console.BufferHeight = y;
            //Console.WindowWidth = x;
            //Console.WindowHeight = y;
            Scene.Add(new Sphere(new Point(-15, 0, 10), 5));
            Scene.Add(new Sphere(new Point(0, 0, 20), 5));
            Scene.Add(new Sphere(new Point(15, 0, 30), 5));
            Scene.Add(new Sphere(new Point(0, 20, 100), 5));
            Scene.Add(new Sphere(new Point(0, 20, 5), 5));
            Scene.Add(new Sphere(new Point(-50, 30, 200), 5));
            Scene.Add(new Sphere(new Point(50, 30, 300), 5));
            Vector3 TestVector3 = new Vector3(new Point(1, 0, 0), true);
            MainCamera = new Camera(new Point(0, 0, -10), new Point(0, 0, 1), 60, x, y);

            Thread control = new Thread(Control);
            control.IsBackground = true;
            control.Start();
        }

        private static void Control()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        MainCamera.Pos.X = MainCamera.Pos.X - 1;
                        break;
                    case ConsoleKey.UpArrow:
                        MainCamera.Pos.Z = MainCamera.Pos.Z + 1;
                        break;
                    case ConsoleKey.RightArrow:
                        MainCamera.Pos.X = MainCamera.Pos.X + 1;
                        break;
                    case ConsoleKey.DownArrow:
                        MainCamera.Pos.Z = MainCamera.Pos.Z - 1;
                        break;
                    case ConsoleKey.W:
                        MainCamera.Pos.Y = MainCamera.Pos.Y + 1;
                        break;
                    case ConsoleKey.S:
                        MainCamera.Pos.Y = MainCamera.Pos.Y - 1;
                        break;
                    case ConsoleKey.A:
                        MainCamera.angleXZ -= 2f;
                        break;
                    case ConsoleKey.D:
                        MainCamera.angleXZ += 2f;
                        break;
                    case ConsoleKey.R:
                        MainCamera.angleZY -= 2f;
                        break;
                    case ConsoleKey.F:
                        MainCamera.angleZY += 2f;
                        break;
                }
            }
        }
    }
}
