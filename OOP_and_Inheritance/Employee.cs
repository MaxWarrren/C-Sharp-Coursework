namespace OOP_and_Inheritance;
#pragma warning disable CS8618
public class Employee {
    public enum EmployeeType{
        Sales,
        Manager,
        Production
    }
    private string firstName;
    private string lastName;
    private string id;
    private EmployeeType empType;

    public string FirstName {
        get {return firstName;}
        set {firstName = value;}
    }
    public string LastName {
        get {return lastName;}
        set {lastName = value;}
    }
    public string ID {
        get {return id;}
    }
    public EmployeeType EmpType {
        get {return empType;}
        set {empType = value;}
    }
    public Employee(string firstName, string lastName, string id, EmployeeType empType) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.id = id;
        this.empType = empType;
    }

    public virtual void getEmployeeInfo() {
        Console.WriteLine($"Name: {firstName} {lastName}\nID: {id}\nType: {empType}");
    }
}

 