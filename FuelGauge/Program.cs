namespace Fuel_Gauge;

class Program
{
    static void Main(string[] args)
    {   
        string response;
        int num, dem;
        float gasLeft;
        int[] fValues = new int[2];
        while (true) {
            Console.WriteLine("Fraction:");
            response = Console.ReadLine();
            if (response.ToLower() == "end") {
                break;
            } else {
                try {
                    fValues = Array.ConvertAll(response.Split("/"), int.Parse);
                    num = fValues[0];
                    dem = fValues[1];
                    if (dem>=num && dem > 0) {
                        gasLeft = (float)Math.Round((float)num/(float)dem, 2);
                        if (gasLeft >= 0.99) {
                            Console.WriteLine("F");
                        } else if (gasLeft <= 0.1) {
                            Console.WriteLine("E");
                        } else {
                            Console.WriteLine($"{gasLeft*100}%");
                        }
                    } else {
                        continue;
                    }
                } catch (Exception e) {
                    continue;
                }
            }
        }
    }
}
