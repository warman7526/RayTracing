namespace RT1W
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //SETUP

            //output paramaters
            double aspect_ratio = 16.0 / 9.0;
            int image_width = 720;

            //calculate image height (and make at least 1)
            int image_height = (int)(image_width/aspect_ratio);
            image_height = (image_height < 1) ? 1 : image_height;
            double true_aspect = (double)image_width / image_height;

            //calculate viewport dimensions
            double viewport_height = 2.0;
            double viewport_width = viewport_height * true_aspect;

            //camera parameters
            double focal_length = 1.0;
            Vec3 cam_centre = Vec3.zero;

            //calculate the viewport edge vectors (and deltas)
            Vec3 viewport_u = Vec3.right * viewport_width;
            Vec3 viewport_v = Vec3.down * viewport_height;
            Vec3 delta_u = viewport_u / image_width;
            Vec3 delta_v = viewport_v / image_height;

            //calculate the coords of upperleft pixel(0,0)
            Vec3 viewport_upper_left = cam_centre + (Vec3.back * focal_length) - (viewport_u / 2) - (viewport_v / 2);
            Vec3 pix00_loc = viewport_upper_left + (delta_u + delta_v) / 2;

            //image header
            Console.Write("P3\n" + image_width + " " + image_height + "\n255\n");




            //RENDER!

            //loop over each scanline
            for (int j = 0; j < image_height; j++) {

                //progress bar
                int progbarlen = 36;
                int progi = j * progbarlen / image_height+1;
                string progs = new string('#', progi) + new string('_', progbarlen - progi);
                Console.Error.Write("\rRendering: "+progs+" "+(j+1)+"/"+image_height);

                //for each pixel in the row
                for (int i = 0; i < image_width; i++)
                {
                    //create the ray associated for the pixel
                    Vec3 pixel_centre = pix00_loc + (i * delta_u) + (j * delta_v);
                    Vec3 ray_direction = pixel_centre - cam_centre;
                    Ray r = new Ray(cam_centre, ray_direction.UnitVector());

                    //raytrace bish bash bosh
                    Vec3 colour = ray_colour(r);

                    //write pixel rgb to stdout
                    Console.WriteLine(colour.PixelString());
                }
            }


            Console.Error.WriteLine("\nDone!");
        }

        private static Vec3 ray_colour(Ray ray)
        {
            double a = (ray.direction().y()+1.0) * 0.5;
            return Vec3.Lerp(Vec3.Colour.sky, Vec3.Colour.white, a);
        }
    }
}