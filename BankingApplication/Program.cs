namespace BankingApplication;
#pragma warning disable CS8604;
#pragma warning disable CS8600;
#pragma warning disable CS0618;

enum JobTitles {
        Manager,
        LoanOfficer
}
enum AccountTypes {
    Checking,
    Savings
}
enum LoanTypes {
    Personal,
    Home,
    Auto,
    None
}
class Program {
    static List<Customer> customerData = new List<Customer>();
    static List<Employee> employeeData = new List<Employee>();
    static void Main(string[] args) {
        loadData();
        mainMenu();
    }
    static string GenerateAccountNumber()
    {
        var random = new Random();
        string newAccount = "183977";
        for (int i = 0; i < 10; i++)
        {
            newAccount += random.Next(0, 10).ToString();
        }
        return newAccount;
    }
    static void loadData() {
        string[] rawCustomerData = File.ReadAllLines("customer_data.csv");
        string[] rawEmployeeData = File.ReadAllLines("employee_data.csv");

        for (int i=1; i<rawCustomerData.Length; i++) {
            string[] line = rawCustomerData[i].Split(",");
            Customer newCustomer = new Customer(line[2], line[3], line[0], line[1], line[5], float.Parse(line[4]), line[6], float.Parse(line[7]));
            customerData.Add(newCustomer);
            try {
                line = rawEmployeeData[i].Split(",");
                Employee newEmployee = new Employee(line[2], line[3], line[0], line[1], (JobTitles)Enum.Parse(typeof(JobTitles), line[4]));
                employeeData.Add(newEmployee);
            } catch (Exception e) {
                continue;
            }
        }
    }
    static void mainMenu() {
        while (true) {
            Console.WriteLine("\nWelcome to your Online Banking Application!");
            Console.WriteLine("1. Account Login\n2. Create Account\n3. Administrator Login\n4. Quit\n\nSelect Option");
            try {
                int userChoice = int.Parse(Console.ReadLine());
                switch (userChoice) {
                    case 1: 
                        loginMenu();
                        break;
                    case 2:
                        createAccountMenu();
                        break;
                    case 3:
                        adminLoginMenu();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nERROR: Input not in range (1-4) \n");
                        break;
                }
            } catch (Exception e) {
                Console.WriteLine("\nERROR: Invalid Input Type\n");
            }
        }
    }
    static void loginMenu() {
        Console.WriteLine("\n------------\nACCOUNT LOGIN\n------------");
        while (true) {
            Console.Write("\nEnter Your Account Number: ");
            string getAccountNumber = Console.ReadLine();
            var query = from customer in customerData select customer.AccountNumber;
            if (query.Contains(getAccountNumber)) {
                Customer queriedAccount = (from customer in customerData where customer.AccountNumber == getAccountNumber select customer).Single();
                Console.Write("\nEnter Your PIN: ");
                string getPIN = Console.ReadLine();
                if (queriedAccount.PIN.Equals(getPIN)) {
                    Console.WriteLine($"\nWelcome Back {queriedAccount.GetName()}. How Can We Help You Today?");
                    accountServices(queriedAccount);
                    break;
                } else {
                    Console.WriteLine("\nERROR: Incorrect PIN");
                }
            } else {
                Console.WriteLine("\nERROR: Account Number not found");
            }
        }
    }
    static void createAccountMenu() {
        string fn, ln, pin, newAccType; 
        string newAccNumber = GenerateAccountNumber();
        Console.WriteLine("\n---------------\nCREATE ACCOUNT\n---------------\n");
        Console.Write("Enter Your First Name: ");
        fn = Console.ReadLine();
        Console.Write("\nEnter Your Last Name: ");
        ln = Console.ReadLine();
        while (true) {
            Console.Write("\nCreate A PIN. Must be 4 digits: ");
            pin = Console.ReadLine();
            if (pin.Length == 4) {
                break;
            } else {
                Console.WriteLine("\nERROR: PIN must be 4 digits");
            }
        }
        while (true) {
            Console.WriteLine("\nChoose what type of account to open:\n1. Savings\n2. Checking\n");
            Console.Write("Select Option: ");
            string res = Console.ReadLine();
            try {
                if (int.Parse(res) == 1) {
                    newAccType = "Savings";
                    break;
                } else if (int.Parse(res) == 2) {
                    newAccType = "Checking";
                    break;
                } else {
                    Console.WriteLine("\nERROR: Input not in range (1,2)");
                }
            } catch (Exception e) {
                Console.WriteLine("\nERROR: Invalid Input Type");
            }
        }
        Customer newAccount = new Customer(fn, ln, newAccNumber, pin, newAccType, 100f, "None", 0f);
        customerData.Add(newAccount);
        Console.WriteLine($"\nCongratualtions {newAccount.GetName()}! Your account has been opned with an initial deposit of $100.\nYour account number is #{newAccount.AccountNumber}\nYou now have access to Account Services");
        accountServices(newAccount);
    }
    static void adminLoginMenu() {
        Console.WriteLine("\n---------------\nADMIN LOGIN\n---------------");
        while (true) {
            Console.Write("\nEnter Employee Username: ");
            string getAdminUsername = Console.ReadLine();
            var query = from employee in employeeData select employee.Username;
            if (query.Contains(getAdminUsername)) {
                Employee queriedAccount = (from employee in employeeData where employee.Username.Equals(getAdminUsername) select employee).Single();
                Console.Write("\nEnter Employee PIN: ");
                string getPIN = Console.ReadLine();
                if (queriedAccount.PIN.Equals(getPIN)) {
                    Console.WriteLine($"\nWelcome Back {queriedAccount.GetName()}. How Can We Help You Today?");
                    adminPortal(queriedAccount, true);
                    break;
                } else {
                    Console.WriteLine("\nERROR: Incorrect PIN");
                }
            } else {
                Console.WriteLine("\nERROR: Employee Username not found");
            }
        }
    }
    static void accountServices(Customer currentUser) {
        Console.WriteLine("\n----------------\nACCOUNT SERVICES\n----------------\n");
        Console.WriteLine("What Would You Like To Do?\n");
        Console.WriteLine("1. Make a Withdrawl\n2. Make a Deposit\n3. Transfer Funds\n4. Make Loan Payment\n5. Customer Balance Inquiry\n6. Back To Main Menu\n");
        float amount;
        while (true) {
            Console.Write("Select Option: ");
            try {
                int userChoice = int.Parse(Console.ReadLine());
                switch (userChoice) {
                    case 1:
                        Console.Write($"You currently have ${currentUser.Balance}. How much would you like to withdrawl? ");
                        amount = float.Parse(Console.ReadLine());
                        if (currentUser.CheckSufficientFunds(amount)) {
                            currentUser.Withdrawl(amount);
                            Console.WriteLine($"Withdrawl Successfull | New Balance: {currentUser.Balance}\n");
                        } else {
                            Console.WriteLine("ERROR: Insufficient Funds | Balance cannot go negative\n");
                        }
                        accountServices(currentUser);
                        return;
                    case 2: 
                        Console.Write("\nHow much would you like to deposit? ");
                        amount = float.Parse(Console.ReadLine());
                        currentUser.Deposit(amount);
                        Console.WriteLine($"Deposit Successful | New Balance: {currentUser.Balance}");
                        accountServices(currentUser);
                        return;
                    case 3:
                        Console.Write($"You currently have ${currentUser.Balance}. How much would you like to transfer? ");
                        amount = float.Parse(Console.ReadLine());
                        if (currentUser.CheckSufficientFunds(amount)) {
                            while (true) {
                                Console.Write("Enter transfer account number: ");
                                string getAccountNumber = Console.ReadLine();
                                var query = from customer in customerData select customer.AccountNumber;
                                if (query.Contains(getAccountNumber)) {
                                    Customer queriedAccount = (from customer in customerData where customer.AccountNumber == getAccountNumber select customer).Single();
                                    queriedAccount.Deposit(amount);
                                    currentUser.Withdrawl(amount);
                                    Console.WriteLine($"{amount} successfully transferred to Account #{getAccountNumber} | New Balance: {currentUser.Balance}");
                                    break;
                                } else {
                                    Console.WriteLine("\nERROR: Account Number not found");
                                }
                            }
                        } else {
                            Console.WriteLine("ERROR: Insufficient Funds. You cannot transfer more than you have");
                        }
                        accountServices(currentUser); 
                        return;
                    case 4:
                        if (currentUser.LoanBalance != 0) {
                            Console.Write($"\nHow much will you be paying on your {currentUser.LoanType} Loan today? ");
                            amount = float.Parse((Console.ReadLine()));
                            currentUser.PayLoanBalance(amount);
                            Console.WriteLine($"\nLoan Payment Successfull | New Loan Balance: {currentUser.LoanBalance}");
                        } else {
                            Console.WriteLine("\nYou do not have a loan balance to pay back");
                        }
                        accountServices(currentUser);
                        return;
                    case 5:
                        Console.WriteLine(currentUser.GetInfo());
                        accountServices(currentUser);
                        return;
                    case 6:
                        mainMenu();
                        return;
                    default:
                        Console.WriteLine("\nERROR: Input not in range (1-6)");
                        accountServices(currentUser);
                        return;
            }
            } catch (Exception e) {
                Console.WriteLine("\nERROR: Invalid Input Type\n");
            }
        }      
    }
    static void adminPortal(Employee currentUser, bool showCommands) {
        if (showCommands) {
            Console.WriteLine($"\nWelcome to the Admin Portal {currentUser.GetName()}");
            Console.WriteLine("\nSelect A Report To Review\n");
            Console.WriteLine("1. Show average Savings Account balance\n2. Show total Savings Account balance");
            Console.WriteLine("3. Show average Checking Account balance\n4. Show total Checking Account balance");
            Console.WriteLine("5. Show the number of accounts for each amount type\n6. Show number of each type of loan");
            Console.WriteLine("7. Show average outstanding balance for each type of loan\n8. Show the average outstanding balance for each type of loan");
            Console.WriteLine("9. Show All Employee Information\n10. Show commands\n11. Back to Main Menu");
        }

        Console.Write("\nSelect Option: ");
        int userChoice = int.Parse(Console.ReadLine());

        //for case 1, 2, 3, 4
        var savingsAccQuery = from customer in customerData where customer.AccountType == AccountTypes.Savings select customer.Balance;
        var checkingAccQuery = from customer in customerData where customer.AccountType == AccountTypes.Checking select customer.Balance;

        //for case 6 & 8
        var loanTypesQuery = from customer in customerData select customer.LoanType;
        float homeLoanNum = (from loanType in loanTypesQuery where loanType == LoanTypes.Home select loanType).Count();
        float autoLoanNum = (from loanType in loanTypesQuery where loanType == LoanTypes.Auto select loanType).Count();
        float personalLoanNum = (from loanType in loanTypesQuery where loanType == LoanTypes.Personal select loanType).Count();
        float noneLoanNum = (from loanType in loanTypesQuery where loanType == LoanTypes.None select loanType).Count();

        //for case 7 & 8
        float homeLoanBalance = (from customer in customerData where customer.LoanType == LoanTypes.Home select customer.LoanBalance).Sum();
        float autoLoanBalance = (from customer in customerData where customer.LoanType == LoanTypes.Auto select customer.LoanBalance).Sum();
        float personalLoanBalance = (from customer in customerData where customer.LoanType == LoanTypes.Personal select customer.LoanBalance).Sum(); 

        switch (userChoice) {
            case 1:
                float avgSavings = (float)Math.Round(savingsAccQuery.Sum() / savingsAccQuery.Count(), 2);
                Console.WriteLine($"Average Savings Account Balance: {avgSavings}");
                adminPortal(currentUser, false);
                return;
            case 2:
                Console.WriteLine($"Total Savings Account Balance: {savingsAccQuery.Sum()}");
                adminPortal(currentUser, false);
                return;
            case 3:
                float avgChecking = (float)Math.Round(checkingAccQuery.Sum() / checkingAccQuery.Count(), 2);
                Console.WriteLine($"Average Checking Account Balance: {avgChecking}");
                adminPortal(currentUser, false);
                return;
            case 4:
                Console.WriteLine($"Total Checking Account Balance: {checkingAccQuery.Sum()}");
                adminPortal(currentUser, false);
                return;
            case 5:
                var accountTypesQuery = from customer in customerData select customer.AccountType;
                int checkingAccNum = (from accType in accountTypesQuery where accType == AccountTypes.Checking select accType).Count();
                int savingsAccNum = (from accType in accountTypesQuery where accType == AccountTypes.Savings select accType).Count();
                Console.WriteLine($"Total Number of Accounts:\n\tChecking Accounts: {checkingAccNum}\n\tSavings Accounts: {savingsAccNum}");
                adminPortal(currentUser, false);
                return;
            case 6:
                Console.WriteLine($"Total Number of Each Type of Loan:\n\tHome Loans: {homeLoanNum}\n\tAuto Loans: {autoLoanNum}\n\tPersonal Loans: {personalLoanNum}\n\tNone Loans: {noneLoanNum}");
                adminPortal(currentUser, false);
                return;
            case 7:
                Console.WriteLine($"Total Loan Balance by Loan Type:\n\tHome Loans: {homeLoanBalance}\n\tAuto Loans: {autoLoanBalance}\n\tPersonal Loans: {personalLoanBalance}");
                adminPortal(currentUser, false);
                return;
            case 8:
                float avgHomeLoanBalance = (float)Math.Round(homeLoanBalance / homeLoanNum, 2);
                float avgAutoLoanBalance = (float)Math.Round(autoLoanBalance / autoLoanNum, 2);
                float avgPersonalLoanBalance = (float)Math.Round(personalLoanBalance / personalLoanNum, 2);
                Console.WriteLine($"Average Loan Balance by Loan Type:\n\tHome Loans: {avgHomeLoanBalance}\n\tAuto Loans: {avgAutoLoanBalance}\n\tPersonal Loans: {avgPersonalLoanBalance}");
                adminPortal(currentUser, false);
                return;
            case 9:
                foreach (Employee employee in employeeData) {
                    Console.WriteLine(employee.GetInfo());
                }
                adminPortal(currentUser, false);
                return;
            case 10:
                adminPortal(currentUser, true);
                return;    
            case 11:
                mainMenu();
                return;    
            default:
                Console.WriteLine("\nERROR: Input not in range (1-10)");
                adminPortal(currentUser, false);
                return;
        }
    }
}