namespace Vending_Machine;

class Program
{
    static void Main(string[] args)
    {
        int amountDue = 50, coin;
        string userResponse;
        int[] coinValues = {1, 5, 10, 25};

        while (amountDue >= 0) {
            Console.WriteLine("Amount Due: " + amountDue);

            while (true) {
                Console.WriteLine("Insert Coin:");
                userResponse = Console.ReadLine();
                coin = Convert.ToInt32(userResponse);
                
                if (coinValues.Contains(coin)){
                    amountDue -= coin;
                    break;
                } else {
                    Console.WriteLine("Invalid coin value.");
                }
            }  
        }

        Console.WriteLine("Change Owed: " + Math.Abs(amountDue));
    }
}
