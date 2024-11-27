namespace Menu_Order;

class Program
{
    static void Main(string[] args)
    {
        Dictionary<string, float> menu = new Dictionary<string, float>();
        menu.Add("Baja Taco", 4.00f);
        menu.Add("Burrito", 7.50f);
        menu.Add("Bowl", 8.50f);
        menu.Add("Nachos", 11.00f);
        menu.Add("Quesadilla", 8.50f);
        menu.Add("Super Burrito", 8.50f);
        menu.Add("Super Quesadilla", 9.50f);
        menu.Add("Taco", 3.00f);
        menu.Add("Tortilla Salad", 8.00f);

        float total = 0.00f;
        string input;
        while (true) {
            Console.WriteLine("Item:");
            input = Console.ReadLine();

            if (input.ToLower() == "end") {
                break;
            } else if (menu.ContainsKey(input)) {
                if (menu.TryGetValue(input, out float price));
                total += price;
                string fTotal = total.ToString("F2");
                Console.WriteLine($"Total: ${fTotal}");
            } else {
                continue;
            }
        }
    }
}
