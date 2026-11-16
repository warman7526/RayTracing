using System;
using System.Collections.Generic;
using System.Text;

namespace RT1W
{
    public class Camera
    {

        public double aspect_ratio;
        public int image_width;

        private int image_height;
        private double true_aspect, viewport_width, viewport_height, focal_length;
        private Vec3 cam_centre, viewport_u, delta_u, viewport_v, delta_v, viewport_upper_left, pix00_loc;

        public Camera(int width, double aspect)
        {
            image_width = width;
            aspect_ratio = aspect;


            //calculate image height (and make at least 1)
            image_height = (int)(image_width / aspect_ratio);
            image_height = (image_height < 1) ? 1 : image_height;
            true_aspect = (double)image_width / image_height;

            //calculate viewport dimensions
            viewport_height = 2.0;
            viewport_width = viewport_height * true_aspect;

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

        public void render(HittableList world)
        {
            //image header
            Console.Write("P3\n" + image_width + " " + image_height + "\n255\n");


            //RENDER!

            //loop over each scanline
            world.Add(new Sphere(new Vec3(0, 0, -1), 0.5));
            for (int j = 0; j < image_height; j++)
            {

                //progress bar
                int progbarlen = 72;
                int progi = j * progbarlen / image_height + 1;
                string progs = new string('#', progi) + new string(' ', progbarlen - progi);
                Console.Error.Write("\rRendering: [" + progs + "] " + (j + 1) + "/" + image_height);

                //for each pixel in the row
                for (int i = 0; i < image_width; i++)
                {
                    //create the ray associated for the pixel
                    Vec3 pixel_centre = pix00_loc + (i * delta_u) + (j * delta_v);
                    Vec3 ray_direction = pixel_centre - cam_centre;
                    Ray r = new Ray(cam_centre, ray_direction.UnitVector());

                    //raytrace bish bash bosh
                    Vec3 colour = ray_colour(r, world);

                    //write pixel rgb to stdout
                    Console.WriteLine(colour.PixelString());
                }
            }


            Console.Error.WriteLine("\nDone!");
        }

        //raytracing ooohhhhh!!!! run this method for each pixel
        private static Vec3 ray_colour(Ray ray, HittableList world)
        {

            HitRecord? hit = world.Hit(ray, new Interval(0, double.PositiveInfinity));

            if (hit is not null)
            {
                return Vec3.Colour.FromNormal(hit.normal);
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
