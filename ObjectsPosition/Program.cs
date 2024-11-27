namespace Objects_Position;

class Program
{
    static double calculateDistance(double pos, double vel, double accel, double dT) {
        return (pos + (vel*dT) + (0.5*accel*Math.Pow(dT, 2)));
    }
    static void Main(string[] args)
    {   
        #pragma warning disable CS0219;
        double initPos, vel, accel, dT, finalDistance;
        bool running = true;
        char runtimeCont; 

        Console.WriteLine("This program will calcuate and object's final position\n");

        while (running) {
            while (true) {
                Console.WriteLine("Enter The Object's Initial Position");
                try {
                    initPos = Convert.ToDouble(Console.ReadLine());
                    break;
                }catch(Exception error) {
                    Console.WriteLine("Try Again Bozo");
                }
            }

            while (true) {
                Console.WriteLine("Enter The Object's Initial Velocity");
                try {
                    vel = Convert.ToDouble(Console.ReadLine());
                    break;
                }catch(Exception error) {
                    Console.WriteLine("Try Again Bozo");
                }
            }

            while (true) {
                Console.WriteLine("Enter The Object's Acceleration");
                try {
                    accel = Convert.ToDouble(Console.ReadLine());
                    break;
                }catch(Exception error) {
                    Console.WriteLine("Try Again Bozo");
                }
            }

            while (true) {
                Console.WriteLine("Enter The Object's Elapsed Time");
                try {
                    dT = Convert.ToDouble(Console.ReadLine());
                    break;
                }catch(Exception error) {
                    Console.WriteLine("Try Again Bozo");
                }
            }


            finalDistance = calculateDistance(initPos, vel, accel, dT);

            Console.WriteLine("The Final Distance Is " + finalDistance + "\n");
            Console.WriteLine("Would you like to continue? (y/n)");
            
            runtimeCont = Convert.ToChar(Console.ReadLine().ToLower());
            if (runtimeCont != 'y') {
                running = false;
            } 
        }
    }


}
