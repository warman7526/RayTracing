namespace RT1W
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //SETUP

            Camera cam = new Camera(1280, 16.0 / 9.0);

            //Define the world (muahahahahahaha!)
            HittableList world = new HittableList();
            world.Add(new Sphere(new Vec3(0, 0, -1), 0.5));
            world.Add(new Sphere(new Vec3(3.5, 3, -4.7), 0.35));
            world.Add(new Sphere(new Vec3(-8, 3, -6), 0.75));
            world.Add(new Sphere(new Vec3(8, 0.4, -12), 1.5));
            world.Add(new Sphere(new Vec3(0, -100000000.5, -1), 100000000));

            cam.render(world);
        }

        
        
    }
}