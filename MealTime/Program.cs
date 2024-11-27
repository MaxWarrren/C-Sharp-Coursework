namespace Meal_Time;

class Program
{   
    public static float convertTime(string time) {
        string[] splitTime = time.Split(":");
        float hour = float.Parse(splitTime[0]);
        float minute = float.Parse(splitTime[1]);
        
    
        float finalTime = (float)Math.Round(hour + (minute/60), 2);

        return finalTime;
    }
    static void Main(string[] args)
    {   
        Console.WriteLine("What Time Is It?");
        string userTime = Console.ReadLine();
        float fTime = convertTime(userTime);

        if (7.0 <= fTime && 8.0 >= fTime) {
            Console.WriteLine("Breakfast Time!");
        } else if (12.0 <= fTime && 13.0 >= fTime) {
            Console.WriteLine("Lunch Time!");
        } else if (18.0 <= fTime && 19.0 >= fTime) {
            Console.WriteLine("Dinner Time!");
        } else {
            Console.WriteLine("Not Time To Eat!");
        }
    }
}
