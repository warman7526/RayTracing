namespace RT1W
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int image_width = 512;
            int image_height = 512;

            Console.Write("P3\n" + image_width + " " + image_height + "\n255\n");

            for (int j = 0; j < image_height; j++) {
                int progbarlen = 36;
                int progi = j * progbarlen / image_height+1;
                string progs = new string('#', progi) + new string('_', progbarlen - progi);
                Console.Error.Write("\rRendering: "+progs+" "+(j+1)+"/"+image_height);
                for (int i = 0; i < image_width; i++)
                {
                    double b = (double)i / (image_width - 1);
                    double g = (double)j / (image_height - 1);
                    double r = 1-((double)(i+j) / (image_height - 1) * 0.5);

                    Vec3 colour = new Vec3(r, g, b);

                    Console.WriteLine(colour.PixelString());
                }
            }
            Console.Error.WriteLine("\nDone!");
        }
    }
}