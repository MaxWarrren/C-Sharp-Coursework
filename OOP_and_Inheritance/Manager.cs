namespace OOP_and_Inheritance;
#pragma warning disable CS8618

class Manager : Employee {
    private string department, region;
    public string Department {
        get { return department; }
        set { department = value; }
    }
    public string Region {
        get { return region; }
        set { region = value; }
    }
    public Manager (string firstName, string lastName, string id, string department, string region) : base(firstName, lastName, id, EmployeeType.Manager) {
        this.department = department;
        this.region = region;
    }
}