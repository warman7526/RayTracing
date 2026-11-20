using System;
using System.Collections.Generic;
using System.Text;

namespace RT1W
{
    public class Material
    {
        public Material() { }

        public virtual (Ray,Vec3)? Scatter(Ray ray, HitRecord hit)
        {
            return null;
        }
    }

    public class Lambertian: Material
    {

        private Vec3 albedo;
        public Lambertian(Vec3 albedo) {
            this.albedo = albedo;
        }

        public override (Ray, Vec3)? Scatter(Ray ray, HitRecord hit)
        {
            Vec3 new_direction = hit.normal + Vec3.RandomUnitVector();
            if (new_direction.IsNearZero()) new_direction = hit.normal;

            Ray out_ray = new Ray(hit.point,new_direction);
            return (out_ray,albedo);
        }
    }

    public class Metal : Material
    {

        private Vec3 albedo;
        public Metal(Vec3 albedo)
        {
            this.albedo = albedo;
        }

        public override (Ray, Vec3)? Scatter(Ray ray, HitRecord hit)
        {
            Vec3 new_direction = ray.direction().Reflect(hit.normal);

            Ray out_ray = new Ray(hit.point, new_direction);
            return (out_ray, albedo);
        }
    }
}
