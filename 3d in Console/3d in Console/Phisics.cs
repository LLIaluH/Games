using System;
using System.Collections.Generic;
using System.Text;

namespace _3d_in_Console
{
    enum Axeses
    {
        Y,
        X,
        Z
    }

    struct Point
    {
        public float X;
        public float Y;
        public float Z;

        public Point(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Point(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Point(double x, double y, double z)
        {
            this.X = (float)x;
            this.Y = (float)y;
            this.Z = (float)z;
        }
    }

    struct Direction
    {
        private Point point;
        private float length;
        public Point Point
        {
            get { return point; }
            set { new Point(Math.Max(Math.Min(value.X, 1), -1), Math.Max(Math.Min(value.Y, 1), -1), Math.Max(Math.Min(value.Z, 1), -1)); }
        }

        public float Length
        {
            get { return length; }
            private set { length = value; }
        }

        public Direction(Point p)
        {
            point = p;
            length = (float)Math.Sqrt(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2) + Math.Pow(p.Z, 2));
        }
        public Direction(float x, float y, float z)
        {
            Point p = new Point(x, y, z);
            point = p;
            length = (float)Math.Sqrt(Math.Pow(p.X, 2) + Math.Pow(p.Y, 2) + Math.Pow(p.Z, 2));
        }
    }

    class Vector3 : ICloneable
    {
        public Point A;
        public Point B;
        public Direction Dir;
        public float Length;

        public float angleXY = 0;
        public float angleXZ = 0;
        public float angleZY = 0;

        public Vector3()
        {
            this.A = new Point(0, 0, 0);
            this.B = new Point(0, 0, 0);
            SetLength();
            SetDirection();
        }

        public Vector3(Point p, bool NeededSetAxisAngles = true)
        {
            this.A = new Point(0, 0, 0);
            this.B = p;
            //this.Length = 1;
            SetLength();
            SetDirection();
            if (NeededSetAxisAngles)
            {
                SetAxisAngles();
            }
        }

        public Vector3(Point p, Point p2, bool NeededSetAxisAngles = true)
        {
            this.A = p;
            this.B = p2;
            SetLength();
            SetDirection();
            if (NeededSetAxisAngles)
            {
                SetAxisAngles();
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public void SetAxisAngles()
        {
            return;
            //Vector3 OX = new Vector3(new Point(1, 0, 0), false);
            //Vector3 v1 = new Vector3(this.Dir.Point, false);
            //v1.B.Z = 0;
            //angleXY = GetAngle(OX, v1);

            //v1 = this;
            //v1.B.X = this.B.Z;
            //v1.B.Z = 0;
            //angleZY = GetAngle(OX, v1);

            //v1 = this;
            //v1.B.Y = this.B.Z;
            //v1.B.Z = 0;
            //angleXZ = GetAngle(OX, v1);

            Vector3 OX = new Vector3(new Point(1, 0, 0), false);
            Vector3 OZ = new Vector3(new Point(0, 0, 1), false);

            Vector3 v1 = new Vector3(this.Dir.Point, false);
            v1.B.Z = 0;
            v1.SetDirection();
            if (!IsNullVector(v1))
            {
                angleXY = GetAngle(OX, v1);
            }

            v1 = this;
            v1.B.X = 0;
            v1.SetDirection();
            if (!IsNullVector(v1))
            {
                angleZY = GetAngle(OZ, v1);
            }

            v1 = this;
            v1.B.Y = 0;
            v1.SetDirection();
            if (!IsNullVector(v1))
            {
                angleXZ = GetAngle(OX, v1);
            }
        }

        private static bool IsNullVector(Vector3 v)
        {
            return v.B.X == 0 && v.B.Y == 0 && v.B.Z == 0;
        }

        public static Vector3 ToNullPointStart(Vector3 v)
        {
            return new Vector3(new Point(v.B.X - v.A.X, v.B.Y - v.A.Y, v.B.Z - v.A.Z));
        }

        private void SetLength()
        {
            //this.Length = (float)Math.Sqrt(Math.Pow((B.X - A.X), 2) + Math.Pow((B.Y - A.Y), 2) + Math.Pow((B.Z - A.Z), 2));
            this.Length = (float)PhisicsAndMath.GetHypotenuseLength(A, B);
        }

        public void SetDirection()
        {
            this.Dir = new Direction(new Point(
                (B.X - A.X) / Length,
                (B.Y - A.Y) / Length,
                (B.Z - A.Z) / Length
                ));
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(new Point(a.A.X, a.A.Y, a.A.Z), new Point(b.B.X, b.B.Y, b.B.Z));
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            Vector3 a_Old = a;
            //Vector3 b_Old = b;
            a = Vector3.ToNullPointStart(a);
            b = Vector3.ToNullPointStart(b);


            //Point p = new Point(b.B.X - a.B.X + a_Old.A.X, b.B.Y - a.B.Y  + a_Old.A.Y, b.B.Z - a.B.Z + a_Old.A.Z);
            //return new Vector3(a.A, p);


            return new Vector3(new Point(b.B.X - a.B.X, b.B.Y - a.B.Y, b.B.Z - a.B.Z));
        }

        public static float operator *(Vector3 a, Vector3 b)
        {
            a = Vector3.ToNullPointStart(a);
            b = Vector3.ToNullPointStart(b);
            return a.B.X * b.B.X + a.B.Y * b.B.Y + a.B.Z * b.B.Z;

            //Point p = new Point();
            //var A = a.Dir.Point;
            //var B = b.Dir.Point;
            //p.X = A.Y * B.Z - A.Z * B.Y;
            //p.Y = A.Z * B.X - A.X * B.Z;
            //p.Z = A.X * B.Y - A.Y * B.X;
            //return new Vector3(p);
        }

        public static Vector3 operator *(Vector3 a, float b)
        {
            Point p = new Point(a.B.X * b, a.B.Y * b, a.B.Z * b);
            //Vector3 res = a;
            //a.B = p;
            return new Vector3(a.A, p);
        }

        public static float GetCos(Vector3 a, Vector3 b)
        {
            return a.Dir.Point.X * b.Dir.Point.X + a.Dir.Point.Y * b.Dir.Point.Y + a.Dir.Point.Z * b.Dir.Point.Z;
        }

        public static float GetAngle(Vector3 a, Vector3 b)
        {
            return (float)((float)Math.Acos(GetCos(a, b)) * 180 / Math.PI);
        }

        public static void ChangeStartPoint(ref Vector3 v, Point NewStartPoint)
        {
            Point p1 = new Point(v.A.X + NewStartPoint.X, v.A.Y + NewStartPoint.Y, v.A.Z + NewStartPoint.Z);
            Point p2 = new Point(v.B.X + NewStartPoint.X, v.B.Y + NewStartPoint.Y, v.B.Z + NewStartPoint.Z);
            v = new Vector3(p1, p2);
        }

        public void Rotate(float angle, Axeses axes)
        {
            angle = (float)(angle * Consts.PiDiv180);
            double[,] matrix ;
            switch (axes)
            {
                case Axeses.X:
                    matrix = new double[3, 3] { { 1, 0, 0 }, { 0, Math.Cos(angle), - Math.Sin(angle) }, { 0, Math.Sin(angle), Math.Cos(angle) } };
                    break;
                case Axeses.Y:
                    matrix = new double[3, 3] { { Math.Cos(angle), 0, Math.Sin(angle) }, { 0, 1, 0}, { - Math.Sin(angle), 0, Math.Cos(angle) } };//готово
                    break;
                case Axeses.Z:
                    matrix = new double[3, 3] { { Math.Cos(angle), -Math.Sin(angle), 0 }, { Math.Sin(angle), Math.Cos(angle), 0 }, { 0, 0, 1 } };//готово
                    break;
                default:
                    matrix = new double[3, 3] { { Math.Cos(angle), 0, Math.Sin(angle) }, { 0, 1, 0 }, { -Math.Sin(angle), 0, Math.Cos(angle) } };//готово
                    break;
            }
            PhisicsAndMath.MultyplyMatrixOnVector(this, matrix);
            SetDirection();
        }

        public static Vector3 Rotate(Vector3 v, float angle, Axeses axes)
        {
            angle = (float)(angle * Consts.PiDiv180);
            double[,] matrix;
            switch (axes)
            {
                case Axeses.X:
                    matrix = new double[3, 3] { { 1, 0, 0 }, { 0, Math.Cos(angle), -Math.Sin(angle) }, { 0, Math.Sin(angle), Math.Cos(angle) } };
                    break;
                case Axeses.Y:
                    matrix = new double[3, 3] { { Math.Cos(angle), 0, Math.Sin(angle) }, { 0, 1, 0 }, { -Math.Sin(angle), 0, Math.Cos(angle) } };//готово
                    break;
                case Axeses.Z:
                    matrix = new double[3, 3] { { Math.Cos(angle), -Math.Sin(angle), 0 }, { Math.Sin(angle), Math.Cos(angle), 0 }, { 0, 0, 1 } };//готово
                    break;
                default:
                    matrix = new double[3, 3] { { Math.Cos(angle), 0, Math.Sin(angle) }, { 0, 1, 0 }, { -Math.Sin(angle), 0, Math.Cos(angle) } };//готово
                    break;
            }
            PhisicsAndMath.MultyplyMatrixOnVector(v, matrix);
            v.SetDirection();
            return v;

            #region МожетБытьЯГений
            //float newX;
            //float newY;
            //Vector3 vN = Vector3.ToNullPointStart(v);//N - значит приведённый к нормальному виду (стартовая точка в {0,0,0})
            //Vector3 result = new Vector3(new Point(0, 0, 0));
            //switch (axes)
            //{
            //    case Axeses.Y:
            //        newX = (float)Math.Cos(v.angleXZ + angle) * v.Length;
            //        newY = (float)Math.Sin(v.angleXZ + angle) * v.Length;
            //        result = new Vector3(new Point(newX, vN.B.Y, newY));
            //        ChangeStartPoint(ref result, v.A);
            //        break;                                    
            //    case Axeses.X:
            //        newX = (float)Math.Cos(v.angleZY + angle) * v.Length;
            //        newY = (float)Math.Sin(v.angleZY + angle) * v.Length;
            //        result = new Vector3(new Point(vN.B.X, newY, newX));
            //        ChangeStartPoint(ref result, v.A);
            //        break;                                    
            //    case Axeses.Z:
            //        newX = (float)Math.Cos(v.angleXY + angle) * v.Length;
            //        newY = (float)Math.Sin(v.angleXY + angle) * v.Length;
            //        result = new Vector3(new Point(newX, newY, vN.B.Z));
            //        ChangeStartPoint(ref result, v.A);
            //        break;
            //}
            //return result; 
            #endregion
        }
    }
}
