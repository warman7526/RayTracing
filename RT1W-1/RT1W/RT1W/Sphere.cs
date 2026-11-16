using System;
using System.Collections.Generic;
using System.Text;

namespace RT1W
{
    public class Sphere: Hittable
    {
        private Vec3 centre;
        private double radius;

        public Sphere(Vec3 centre, double radius)
        {
            this.centre = centre;
            this.radius = Math.Max(0,radius);
        }

        public override HitRecord? Hit(Ray ray, double t_min, double t_max)
        {
            // using vector eq. of a sphere: (C-P).(C-P) = r2 rearranged,
            // a quadratic can be formed where the discriminant relates to
            // the number of intersections of the ray and the sphere surface.

            //COEFFICIENTS!!!!! (btw b = -2h) cos it makes it simplify
            double a = ray.direction().length_squared();
            double h = ray.direction().Dot(this.centre - ray.origin());
            double c = (this.centre - ray.origin()).length_squared() - (this.radius * this.radius);

            //caluclate discriminant
            double delta = (h * h) - (a * c);
            if (delta < 0)
            {
                return null;// return null if there is no intersection
            }


            //otherwise return the intersection point
            double sqrtd = Math.Sqrt(delta);
            double root = (h - sqrtd) / a; // try -ve root first as this is "infront" of the +ve
            if(root <= t_min || root >= t_max)
            {
                root = (h +  sqrtd) / a;
                if (root <= t_min || root >= t_max)
                {
                    return null;
                }
            }

            //get hit point
            HitRecord res = new HitRecord();
            res.t = root;
            res.point = ray.At(root);

            //caluclate local normal
            Vec3 outNormal = (res.point - this.centre) / this.radius;
            res.SetFaceNormal(ray, outNormal);
            

            return res;
        }
    }
}
