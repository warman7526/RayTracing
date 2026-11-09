using System;
using System.Collections.Generic;
using System.Text;

namespace RT1W
{
    public class Ray
    {
        private Vec3 orig;
        private Vec3 dir;


        public Vec3 origin()
        {
            return orig;
        }

        public Vec3 direction()
        {
            return dir;
        }

        public Vec3 At(double t)
        {
            return orig + (t * dir);
        }

        public Ray(Vec3 orig, Vec3 dir)
        {
            this.orig = orig;
            this.dir = dir;
        }

        public Ray(Vec3 dir)
        {
            this.orig = Vec3.zero;
            this.dir = dir;
        }
    }
}
