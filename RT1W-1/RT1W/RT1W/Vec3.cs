using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RT1W
{
    public class Vec3
    {

        public static Vec3 zero = new Vec3();
        public double[] values;

        public Vec3()
        {
            values = new double[] { 0, 0, 0 };
        }
        public Vec3(double[] e) {
            this.values = e;
        }
        public Vec3(double x, double y, double z)
        {
            values = new double[] { x, y, z };
        }

        public double x() { return values[0]; }
        public void x(double x) { values[0] = x; }
        public double y() { return values[1]; }
        public void y(double y) { values[0] = y; }

        public double z() { return values[2]; }
        public void z(double z) { values[0] = z; }


        public static Vec3 operator -(Vec3 v) {
            return new Vec3(-v.values[0], -v.values[1], -v.values[2]);
        }

        public static Vec3 operator +(Vec3 v1, Vec3 v2) {
            return new Vec3(
                v1.values[0] + v2.values[0],
                v2.values[1] + v1.values[1],
                v1.values[2] + v2.values[2]
                );
        }

        public void operator +=(Vec3 v1)
        {
            values = (this + v1).values;
        }

        public static Vec3 operator -(Vec3 v1, Vec3 v2)
        {
            return v1 + (-v2);
        }
        public void operator -=(Vec3 v1)
        {
            values = (this - v1).values;
        }

        public override string ToString() {
            return $"Vec3({this.x()},{this.y()},{this.z()})";
         }

        public static Vec3 operator *(Vec3 v, double t)
        {
            return new Vec3(
                v.values[0] * t,
                v.values[1] * t,
                v.values[2] * t
                );
        }
        public static Vec3 operator *(double t, Vec3 v) { return v * t; }
        public static Vec3 operator *(Vec3 v1, Vec3 v2)
        {
            return new Vec3(
                v1.y() * v2.z() - v2.y() * v1.z(),
                v1.z() * v2.x() - v2.z() * v1.x(),
                v1.x() * v2.y() - v2.x() * v1.y()
                );
        }

        public void operator *=(double t)
        {
            values = (this * t).values;
        }
        public void operator *=(Vec3 v)
        {
            values = (this * v).values;
        }

        public static Vec3 operator /(Vec3 v, double t)
        {
            return new Vec3(
                v.x() / t,
                v.y() / t,
                v.z() / t
                );
        }
        public void operator /=(double t)
        {
            values = (this / t).values;
        }

        public double length_squared()
        {
            return x() * x() + y() * y() + z() * z();
        }

        public double length()
        {
            return Math.Sqrt(length_squared());
        }

        public double Dot(Vec3 other)
        {
            return this.x() * other.x() + this.y() * other.y() + this.z() * other.z();
        }

        public Vec3 ScaleComponents(Vec3 v)
        {
            return new Vec3(x() * v.x(), y() * v.y(), z() * v.z());
        } 

        public Vec3 UnitVector()
        {
            return this / this.length();
        }

        public string PixelString()
        {
            int ir = (int)(255.999 * x());
            int ig = (int)(255.999 * y());
            int ib = (int)(255.999 * z());

            return $"{ir} {ig} {ib}";
        }

    }

   
}