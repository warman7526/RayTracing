namespace RT1W
{
    public class Program
    {

        public static Random random = new Random();
        public static void Main(string[] args)
        {


            //SETUP
            Camera cam;
            if (args.Contains<string>("-d"))
            {
                 cam = new Camera(200, 1, 30,10);//quick for tests
            }
            else
            {
                 cam = new Camera(720, 16.0 / 9.0, 200,50);//detailed render
            }

            //define materials
            Material ground = new Lambertian(new Vec3(0.4, 0.8, 0));
            Material center = new Lambertian(Vec3.Colour.blue);
            Material left = new Metal(Vec3.Colour.white * 0.8);
            Material right = new Metal(Vec3.Colour.magenta * 0.7);

            //Define the world (muahahahahahaha!)
            HittableList world = new HittableList();
            world.Add(new Sphere(new Vec3(0, 0, -1.2), 0.5,center));
            world.Add(new Sphere(new Vec3(-1, 0, -1.2), 0.5,left));
            world.Add(new Sphere(new Vec3(1, 0, -1.2), 0.5,right));
            world.Add(new Sphere(new Vec3(0, -10000.5, -1), 10000,ground));

            cam.Render(world);
        }

        public static double deg_to_rad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        public static double Random(double min, double max) { 
            return random.NextDouble() * (max - min) + min;
        }
        public static double Random()
        {
            return random.NextDouble();
        }
        
    }
}