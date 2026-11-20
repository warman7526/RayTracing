using System;
using System.Collections.Generic;
using System.Text;

namespace RT1W
{
    public class Camera
    {

        public double aspect_ratio;
        public int image_height, samples_per_pix, max_depth;

        private int image_width;
        private double true_aspect, viewport_width, viewport_height, focal_length;
        private Vec3 cam_centre, viewport_u, delta_u, viewport_v, delta_v, viewport_upper_left, pix00_loc;

        public Camera(int height, double aspect, int samples, int max_depth)
        {
            image_height = height;
            aspect_ratio = aspect;
            samples_per_pix = samples;
            this.max_depth = max_depth;

            

            //calculate image height (and make at least 1)
            image_width = (int)(image_height * aspect_ratio);
            image_width = (image_width < 1) ? 1 : image_width;
            true_aspect = (double)image_height / image_width;

            //calculate viewport dimensions
            viewport_width = 3.5;
            viewport_height = viewport_width * true_aspect;

            //camera parameters
            focal_length = 1.0;
            cam_centre = Vec3.zero;

            //calculate the viewport edge vectors (and deltas)
            viewport_u = Vec3.right * viewport_width;
            viewport_v = Vec3.down * viewport_height;
            delta_u = viewport_u / image_width;
            delta_v = viewport_v / image_height;

            //calculate the coords of upperleft pixel(0,0)
            viewport_upper_left = cam_centre + (Vec3.back * focal_length) - (viewport_u / 2) - (viewport_v / 2);
            pix00_loc = viewport_upper_left + (delta_u + delta_v) / 2;
        }

        public void Render(HittableList world)
        {
            //image header
            Console.Write("P3\n" + image_width + " " + image_height + "\n255\n");


            //RENDER!

            //loop over each scanline

            Vec3 colour;
            int progbarlen = 72;
            DateTime start = DateTime.UtcNow;

            for (int j = 0; j < image_height; j++)
            {

                //progress bar
                DateTime end = DateTime.UtcNow;
                TimeSpan elapsed = end - start;
                TimeSpan lineavg = elapsed / Math.Max(j, 1);
                TimeSpan remaining = lineavg * (image_height-j)
                ;
                int progi = j * progbarlen / image_height+1;
                string progs = new string('#', progi) + new string(' ', progbarlen - progi);
                Console.Error.Write("\rRendering: [" + progs + "] " + (j + 1) + "/" + image_height+" ETA:+"+remaining.ToString("g")+"s   ");

                //for each pixel in the row
                for (int i = 0; i < image_width; i++)
                {
                    colour = new Vec3();
                    for (int s = 0; s < samples_per_pix; s++)
                    {
                        //create the ray associated for the pixel
                        Ray r = GetRay(i, j);

                        //raytrace bish bash bosh
                        colour += RayColour(r, world, max_depth);
                    }
                    //write pixel rgb to stdout
                    Console.WriteLine((colour /  samples_per_pix).PixelString(Transform.LinearToGamma));
                }
            }


            Console.Error.WriteLine("\nDone!");
        }

        private Ray GetRay(int i, int j)
        {


            Vec3 offset = new Vec3(Program.Random() - 0.6, Program.Random() - 0.6, 0)*1.2;
            Vec3 pixel_centre = pix00_loc + ((i+ offset.x()) * delta_u) + ((j + offset.y()) * delta_v);

            Vec3 ray_direction = (pixel_centre) - cam_centre;

            Ray r = new Ray(cam_centre, ray_direction.UnitVector());
            return r;
        }

        //raytracing ooohhhhh!!!! run this method for each pixel
        private Vec3 RayColour(Ray ray, HittableList world, int depth)
        {
            if (depth == 0) return Vec3.Colour.black;
            HitRecord? hit = world.Hit(ray, new Interval(1e-4, double.PositiveInfinity));

            if (hit is not null)
            {
                (Ray, Vec3)? res = hit.material.Scatter(ray, hit);
                if( res is not null)
                {
                    Vec3 attenuation = (((Ray, Vec3))res).Item2;
                    Ray new_ray = (((Ray, Vec3))res).Item1;
                    return attenuation.ScaleComponents(RayColour(new_ray, world, depth - 1));          
                }
                else
                {
                    return Vec3.Colour.black;
                }
     
            }



            return SkyColour(ray);
        }

        //skycolour for if they ray dint hit owt
        public static Vec3 SkyColour(Ray ray)
        {
            double a = (ray.direction().y() + 1.0) * 0.5;
            return Vec3.Lerp(Vec3.Colour.sky, Vec3.Colour.white, a);
        }
    }
}
