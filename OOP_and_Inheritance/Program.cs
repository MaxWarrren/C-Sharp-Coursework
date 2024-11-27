namespace OOP_and_Inheritance;
#pragma warning disable CS8618

class Program {

    static void Main(string[] args) {
        Employee employee1 = new Employee("Truman", "Tiger", "12345", Employee.EmployeeType.Sales);
        SalesPerson salesPerson1 = new SalesPerson("Mickey", "Mouse", "23456", "Sporting Goods", 7500);
        Manager manager1 = new Manager("Elmer", "Fudd", "56789", "Electronics", "Midwest");

        Console.WriteLine("\n-------Employee 1-------------");
        employee1.getEmployeeInfo();

        Console.WriteLine("\n-------Sales Person 1-------------");
        salesPerson1.getEmployeeInfo();
        salesPerson1.addSales(5250.70f);
        Console.WriteLine($"Updated {salesPerson1.getSalesInfo()}");

        Console.WriteLine("\n-------Manager 1-------------");
        manager1.getEmployeeInfo();
        Console.WriteLine($"Dept: {manager1.Department} | Region: {manager1.Region}");
        manager1.FirstName = "Wiley";
        manager1.LastName = "Coyote";
        manager1.Region = "Southeast";
        manager1.Department = "Automotive";
        Console.WriteLine($"\nNew Name: {manager1.FirstName} {manager1.LastName}");
        Console.WriteLine($"New Dept: {manager1.Department} | New Region: {manager1.Region}");

        
    }
}