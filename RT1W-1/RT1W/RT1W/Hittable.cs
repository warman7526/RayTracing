using System;
using System.Collections.Generic;
using System.Text;

namespace RT1W
{
    public class Hittable
    {
        virtual public HitRecord? Hit(Ray ray, Interval range)
        {
            return null;
        }
    }

    public class HitRecord{
        public Vec3 point;
        public Vec3 normal;
        public double t;
        public bool frontFace;

        public void SetFaceNormal(Ray ray, Vec3 outNormal)
        {
            frontFace = ray.direction().Dot(outNormal) <= 0.0;
            normal = frontFace? outNormal: -outNormal;
        }
    }
}
