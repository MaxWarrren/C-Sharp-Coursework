namespace OOP_and_Inheritance;
#pragma warning disable CS8618

class SalesPerson : Employee {
    public enum SalesLevel {
        Bronze,
        Silver,
        Gold,
        Diamond,
        Platinum
    }
    private string department;
    private float sales;
    private SalesLevel salesLevel;

    public float Sales {
        get { return sales; }
    }
    public SalesPerson(string firstName, string lastName, string id, string department, float sales) : base(firstName, lastName, id, EmployeeType.Sales) {
        this.department = department;
        this.sales = sales;
        salesLevel = getSalesLevel();
    }
    public void addSales(float amount) {
        this.sales += amount;
    }
    public SalesLevel getSalesLevel() {
        if (sales >= 40000) {
            return SalesLevel.Platinum;
        } else if (sales >= 30000) {
            return SalesLevel.Diamond;
        } else if (sales >= 20000) {
            return SalesLevel.Gold;
        } else if (sales >= 10000) {
            return SalesLevel.Silver;
        } else {
            return SalesLevel.Bronze;
        }
    }

    public string getSalesInfo() {
        return $"Sales Level: {getSalesLevel()} | Sales: ${this.sales}";
    }
    public override void getEmployeeInfo() {
        base.getEmployeeInfo();
        Console.WriteLine(getSalesInfo());
        

    }
}