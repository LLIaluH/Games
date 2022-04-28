using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace _3d_in_Console
{
    class Camera
    {
        public Point Pos = new Point(0, 0, 0);
        public Point Dir;
        public float fov;

        public float angleXZ = 0;
        public float angleZY = 0;

        public int width = 150;
        public int height = 75;

        Char[,] Screen;
        float tanFovDivTwo;
        float OneOfHeight;

        Thread threadRender;
        public Camera(Point point, Point vector, float fov, int x, int y)
        {
            this.Pos = point;
            this.Dir = vector;
            this.fov = fov;
            this.width = x;
            this.height = y;
            Screen = new Char[height, width];
            tanFovDivTwo = (float)Math.Tan(fov / 2);
            OneOfHeight = (1 / (float)height);
            threadRender = new Thread(RenderScene);
            threadRender.Priority = ThreadPriority.Highest;
            threadRender.Start();
        }

        private void RenderScene()
        {
            while (true)
            {
                Thread.Sleep(400);
                this.Render();
            }
        }

        private void Print_map()
        {
            List<Point> pointsOnMap = new List<Point>();
            foreach (var obj in Scene.objects)
            {
                Sphere s = obj as Sphere;
                pointsOnMap.Add(s.Pos);
            }
            ConsoleColor colorLast = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var p in pointsOnMap)
            {
                ReWriteSimbol(Convert.ToInt32(p.X / 7f + width / 2), Convert.ToInt32(-1 * p.Z / 7f + height / 2), 'o');
            }
            Console.ForegroundColor = ConsoleColor.Green;
            ReWriteSimbol(Convert.ToInt32(Pos.X / 7f + width / 2), Convert.ToInt32(-1 * Pos.Z / 7f + height / 2), 'C');

            Console.ForegroundColor = colorLast;
        }

        private void PrintAngles()
        {
            ConsoleColor colorLast = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintString(angleXZ.ToString(), 1, 0);
            PrintString(angleZY.ToString(), 1, 2);
            Console.ForegroundColor = colorLast;
        }

        private void PrintString(string s, int x, int y)
        {
            for (int i = 0; i < s.Length; i++)
            {
                ReWriteSimbol(x + i, y, s[i]);
            }
        }

        private char Cast_ray(Point start, Point dir)
        {
            foreach (var obj in Scene.objects)
            {
                Sphere s = obj as Sphere;
                if (s.ray_intersect(start, dir))
                {
                    return '@';
                }
            }
            return ' ';
        }

        public void Render()
        {
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    float x = (float)((2 * (i + 0.5) / (float)width - 1) * tanFovDivTwo * width * OneOfHeight);
                    float y = (float)(-(2 * (j + 0.5) / (float)height - 1) * tanFovDivTwo);
                    Vector3 vec = new Vector3(new Point(x, y, -fov / 2));
                    vec.Rotate(angleXZ, Axeses.Y);
                    vec.Rotate(angleZY, Axeses.X);
                    //vec = Vector3.Rotate(vec, angleXZ, Axeses.Y);
                    Screen[j, i] = Cast_ray(Pos, vec.Dir.Point);
                }
            }
            WriteString(Screen, height, width);
            Print_map();
            PrintAngles();
        }

        private static void WriteString(Char[,] chars, int height, int width)
        {
            String s = "";
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    s += chars[j, i];
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(s);
        }

        private void ReWriteSimbol(int x, int y, char simbol)
        {
            if (!(x < 0 || x >= width || y < 0 || y >= height))
            {
                Console.SetCursorPosition(Convert.ToInt32(x), Convert.ToInt32(y));
                Console.Write(simbol);
            }
        }
    }
}
