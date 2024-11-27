namespace BankingApplication;
abstract class Person {
    protected string FirstName;
    protected string LastName;
    protected string _PIN;
    public string PIN {get {return _PIN;}}
    public Person(string firstName, string lastName, string pin) {
        this.FirstName = firstName;
        this.LastName = lastName;
        this._PIN = pin;
    }
    public abstract string GetInfo();
    public virtual string GetName() {
        return $"{FirstName} {LastName}";
    }
}
class Employee : Person {
 
    private string username;
    public string Username {get {return username;}}
    private JobTitles jobTitle;
    public JobTitles JobTitle {get {return jobTitle;}}
    public Employee(string firstName, string lastName, string username, string pin, JobTitles jobTitle) : base(firstName, lastName, pin) {
        this.username = username;
        this.jobTitle = jobTitle;
    }
    public override string GetInfo()
    {
        return $"Name: {base.GetName()} - Title: {this.JobTitle}";
    }
}
class Customer : Person {
    private string accountNumber;
    public string AccountNumber {get {return accountNumber;}}
    private float balance;
    public float Balance {get {return balance;}}
    private float loanBalance;
    public float LoanBalance {get {return loanBalance;}}
    private AccountTypes accountType;
    public AccountTypes AccountType {get {return accountType;}}
    private LoanTypes loanType;
    public LoanTypes LoanType {get {return loanType;}}

    public Customer(string firstName, string lastName, string accountNumber, string pin, string accType, float balance, string loanType, float loanBalance): base(firstName, lastName, pin) {
        this.accountNumber = accountNumber;
        this.balance = balance;
        this.loanBalance = loanBalance;
        this.accountType = (AccountTypes)Enum.Parse(typeof(AccountTypes), accType); //converting string to enum type
        this.loanType = (LoanTypes)Enum.Parse(typeof(LoanTypes), loanType); //converting string to enum type
        
    }
    public void Withdrawl(float amount) {balance -= amount;}
    public void Deposit(float amount) {balance += amount;}
    public bool CheckSufficientFunds(float amount) {return (balance - amount) >= 0;}
    public void PayLoanBalance(float amount) {loanBalance -= amount; loanBalance = (loanBalance < 0) ? 0 : loanBalance;} // if loanBalance falls below 0, set it back to 0
    public void TransferFunds(float amount, Customer user) {this.Withdrawl(amount); user.Deposit(amount);}
    public override string GetInfo()
    {
        return $"\n---------- ACCOUNT #:{accountNumber} --------------\nName: {base.GetName()}\n{accountType} Account Balance: ${balance}\n{loanType} Loan Balance: ${loanBalance}";
    }


}