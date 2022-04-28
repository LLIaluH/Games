using System;
using System.Collections.Generic;
using System.Text;

namespace _3d_in_Console
{
    class Sphere : IFigure
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
}
