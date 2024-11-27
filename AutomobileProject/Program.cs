namespace Automobile;

class Program
{   
    enum autoType {
            Sedan,
            Truck,
            Van,
            SUV,
    }
    class Automobile {
   
        private string make, model, color, VIN;
        private int year;
        private autoType type;
        public Automobile(string makeName, string modelName, int makeYear, string vinNum, string carColor, autoType carType) {
            make = makeName;
            model = modelName;
            year = makeYear;
            VIN = vinNum;
            color = carColor;
            type = carType;
        }
        public String getMake() {
            return make;
        }
        public String getModel() {
            return model;
        }
        public int getYear() {
            return year;
        }
        public String getVin() {
            return VIN;
        }
        public String getColor() {
            return color;
        }
        public void setColor(string newColor) {
            color = newColor;
        }
        public autoType getType() {
            return type;
        }
        public int getAutoAge() {
            return 2024 - year;
        }
    }
    static void Main(string[] args)
    {   
        Console.WriteLine("\nCreating the first Automobile\n---------------");
        Automobile auto1 = new Automobile("Tesla", "Model X", 2020, "12345", "blue", autoType.Sedan);
        Console.WriteLine($"Make: {auto1.getMake()} \nModel: {auto1.getModel()}\nYear: {auto1.getYear()}\nType: {auto1.getType()} \nVIN: {auto1.getVin()}");
        Console.WriteLine($"Color: {auto1.getColor()}");
        Console.WriteLine("\nChanging the Colour\n---------------");
        auto1.setColor("black");
        Console.WriteLine($"Color: {auto1.getColor()}");

        Console.WriteLine("\nCreating the second Automobile\n---------------");
        Automobile auto2 = new Automobile("Mercedes", "G-Wagon", 2017, "24578", "silver", autoType.SUV);
        Console.WriteLine($"Make: {auto2.getMake()}\nModel: {auto2.getModel()}\nYear: {auto2.getYear()}\nType: {auto2.getType()}\nVIN: {auto2.getVin()}");

        Console.WriteLine("\nPrinting Automobile Ages\n---------------");
        Console.WriteLine($"Auto1 Age: {auto1.getAutoAge()} years");
        Console.WriteLine($"Auto2 Age: {auto2.getAutoAge()} years");
    }
}
