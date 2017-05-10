using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRacing
{
    class Vector2
    {
        private static readonly double epsilon = 1.0e-9;
        public double X;
        public double Y;
        public Vector2()
        {

        }
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }
        public double Dot(Vector2 right)
        {
            return this.X * right.X + this.Y * right.Y;
        }
        public double Norm
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }
        public static Vector2 operator /(Vector2 right, double num)
        {
            return new Vector2(right.X / num, right.Y / num);
        }
        public static Vector2 operator *(Vector2 right, double num)
        {
            return new Vector2(right.X * num, right.Y * num);
        }
        public Vector2 Normalize()
        {
            var norm = Norm;
            if (norm == 0)
            {
                return this;
            }
            return this / norm;
        }
        public Vector2 PerpendicularLeft()
        {
            return new Vector2(Y, -X);
        }
        public Vector2 PerpendicularRight()
        {
            return new Vector2(-Y, X);
        }
        public double Cross(Vector2 v)
        {
            return X * v.Y - Y * v.X;
        }
        public static Vector2 sincos(double angle)
        {
            return new Vector2(Math.Abs(Math.Cos(angle)) < epsilon ? 0 : Math.Cos(angle),
                               Math.Abs(Math.Sin(angle)) < epsilon ? 0 : Math.Sin(angle)).Normalize();
        }
    }
}
