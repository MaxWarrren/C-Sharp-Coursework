namespace C__Basics_Challenge;

class Program
{
    static void Main(string[] args)
    {
       #pragma warning disable CS0219 //hide warnings for unused variables
       
       const byte sample1 = 0x3A; 
       byte sample2 = 58; 
       int heartRate = 85, choice = 2;
       double deposits = 135002796, distance = 129.763001;
       const float acceleration = 9.800f;
       float mass = 14.6f, force;
       bool lost = true, expensive = true;
       const char integral = '\u222B';
       const string greeting = "Hello";
       string name = "Karen", result;




       result = (sample1 == sample2) ? "The samples are equal" : "The samples are not equal";
       Console.WriteLine(result);

       result = (40 <= heartRate && 80 >= heartRate) ? "Heart rate is normal" : "Heart rate is not normal";
       Console.WriteLine(result);

       result = (deposits >= 100000000) ? "You are exceedingly wealthy." : "Sorry you are so poor.";
       Console.WriteLine(result);

       force = mass * acceleration;
       Console.WriteLine("Force = " + force);

       Console.WriteLine(distance + " is the distance");

       //lost is assumed to be true for both outcomes so we will just check expensive
       result = (expensive) ? "I am really sorry! I will get the manager." : "Here is coupon for 10% off.";
       Console.WriteLine(result);

       switch (choice) {
            case 1:
                Console.WriteLine("You chose 1");
                break;
            case 2:
                Console.WriteLine("You chose 2");
                break;
            case 3:
                Console.WriteLine("You chose 3");
                break;
            default:
                Console.WriteLine("You made an unknown choice");
                break;
       }


        //on my computer, it is not outputting the integral symbol, might be something with the encoding format
       Console.WriteLine(integral + " is an integral");

       for (int i=5; i<=10; i++) {
            Console.WriteLine("i = " + i);
       }

       int age = 0;
       while (age < 6) {
            Console.WriteLine("age = " + age);
            age++;
       }
    }
}
