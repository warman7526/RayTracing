namespace RT1W
{
    class Program
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
                    double r = (double)i / (image_width - 1);
                    double g = (double)j / (image_height - 1);
                    double b = 1-((double)(i+j) / (image_height - 1) * 0.5);

                    int ir = (int)(255.999 * r);
                    int ig = (int)(255.999 * g);
                    int ib = (int)(255.999 * b);

                    Console.WriteLine(ir + " " + ig + " " + ib);
                }
            }
            Console.Error.WriteLine("Done!");


        }
    }
}