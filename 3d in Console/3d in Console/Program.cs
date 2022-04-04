using System;
using System.Collections.Generic;
using System.Threading;

namespace _3d_in_Console
{


    class Sphere : Figure
    {
        public Point Pos;
        public float r;

        public Sphere(int x, int y, int z, float rd)
        {
            Pos.X = x;
            Pos.Y = y;
            Pos.Z = z;
            this.r = rd;
        }

        public Sphere(Point p, int rd)
        {
            this.Pos = p;
            this.r = rd;
        }

        public bool ray_intersect(Point startRayPoint, Point dir) 
        {
            //точка выстрела луча и точка его направления даны
            //необходимо узнать расстояние от точки выстрела до центра сферы
            //создать вектор из точки направления

            #region badWorking
            //Vector3 Ray = new Vector3(dir);
            ////Ray = Ray + new Vector3(startRayPoint);
            //var a1 = Ray * (float)PhisicsAndMath.GetHypotenuse(startRayPoint, Pos);
            //Vector3 L = new Vector3(Pos) - a1;// обратить внимание
            //var lengthToRay = (float)PhisicsAndMath.GetHypotenuse(Pos, L.B);
            //if (lengthToRay < r)
            //{
            //    return true;
            //}
            //return false;
            #endregion


            Vector3 L = new Vector3(Pos) - new Vector3(startRayPoint);
            float tca = L * new Vector3(dir);
            float d2 = L * L - tca * tca;
            if (d2 > r * r) return false;
            float thc = (float)Math.Sqrt(r * r - d2);
            float t0 = tca - thc;
            float t1 = tca + thc;
            if (t0 < 0) t0 = t1;
            if (t0 < 0) return false;
            return true;

        }
    }

    static class Scene
    {
        public static List<Figure> objects = new List<Figure>();

        //public Scene()
        //{
        //    objects = new List<Figure>();
        //}

        public static void Add(Figure f)
        {
            objects.Add(f);
        }

        public static void Clear()
        {
            objects.Clear();
        }
    }

    class Camera
    {
        public Point Pos = new Point(0,0,0);
        public Direction Dir;
        public float fov;

        public float angleXZ = 0;
        public float angleZY = 0;

        public int width = 150;
        public int height = 75;

        Char[,] Screen;
        float tanFovDivTwo;
        float OneOfHeight;
        public Camera(Point point, Direction vector, float fov, int x, int y)
        {
            this.Pos = point;
            this.Dir = vector;
            this.fov = fov;
            this.width = x;
            this.height = y;
            Screen = new Char[height, width];
            tanFovDivTwo = (float)Math.Tan(fov / 2);
            OneOfHeight = (1 / (float)height);
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

        private char Cast_ray(Point start, Point dir)
        {
            foreach (var obj in Scene.objects)
            {
                Sphere s = obj as Sphere;
                if(s.ray_intersect(start, dir))
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
                    //vec = Vector3.Rotate(vec, 30, Axeses.Y);
                    Screen[j, i] = Cast_ray(Pos, vec.Dir.Point);
                    //ReWriteSimbol(i, height - j -1, Cast_ray(Pos, vec.Dir.Point));
                }
            }
            WriteString(Screen, height, width);
            Print_map(); 
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

    class DirectionLigth
    {
        public float Intensity;
        public Direction Dir;

        public DirectionLigth(Direction dir, float intensity)
        {
            this.Dir = dir;
            this.Intensity = intensity;
        }
    }

    class Program
    {
        const string simbols = " .;O0@";
        static int x = 200;
        static int y = 100;
        static Camera MainCamera;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.BufferWidth = x;
            Console.BufferHeight = y;
            Console.WindowWidth = x;
            Console.WindowHeight = y;

            Scene.Add(new Sphere(new Point(-15, 0, 10), 5));
            Scene.Add(new Sphere(new Point(0, 0, 20), 5));
            Scene.Add(new Sphere(new Point(15, 0, 30), 5));
            Scene.Add(new Sphere(new Point(0, 20, 100), 5));
            Scene.Add(new Sphere(new Point(0, 20, 5), 5));
            Scene.Add(new Sphere(new Point(-50, 30, 200), 5));
            Scene.Add(new Sphere(new Point(50, 30, 300), 5));
            Vector3 TestVector3 = new Vector3(new Point(1, 0, 0), true);
            MainCamera = new Camera(new Point(0, 0, -10), new  Direction(0, 0, 10), 60, x, y);

            MainCamera.Render();

            Thread renderScene = new Thread(RenderScene);
            renderScene.Start();

            Thread control = new Thread(Control);
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
                }
            }
        }

        private static void RenderScene()
        {
            float counter = 0;
            var s1 = Scene.objects[0] as Sphere;
            float s1Y = s1.Pos.Y;
            var s2 = Scene.objects[1] as Sphere;
            float s2X = s2.Pos.X;
            var s3 = Scene.objects[2] as Sphere;
            float s3Z = s3.Pos.Z;
            while (true)
            {
                //counter += 0.1f;
                //s1.Pos.Y = (float)(s1Y + (Math.Sin(counter) * 2));
                //s2.Pos.X = (float)(s2X + (Math.Sin(counter) * 2));
                //s3.Pos.Z = (float)(s3Z + (Math.Sin(counter) * 2));
                Thread.Sleep(150);
                MainCamera.Render();
            }
        }
    }
}
