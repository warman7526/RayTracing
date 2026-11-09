namespace RT1W
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int image_width = 256;
            int image_height = 256;

            Console.Write("P3\n" + image_width + " " + image_height + "\n255\n");

            for (int j = 0; j < image_height; j++) {
                Console.Error.WriteLine("Scanlines remaining: "+ (image_height-j)+"/"+image_height);
                for (int i = 0; i < image_width; i++)
                {
                    double b = (double)i / (image_width - 1);
                    double g = (double)j / (image_height - 1);
                    double r = 1-((double)(i+j) / (image_height - 1) * 0.5);

                    Vec3 colour = new Vec3(r, g, b);

                    Console.WriteLine(colour.PixelString());
                }
            }
            Console.Error.WriteLine("Done!");


        }
    }
}