namespace Midterm_Project;
using System.IO;
class Program
{   
    enum Status {
        Freshmen,
        Sophomore,
        Junior,
        Senior
    }
    public class Student {
        private string firstName, lastName, major;
        private int creditHours;
        private double score1, score2, score3;
        private Status classStatus;
        public Student(string fName, string lName, int cHours, string mjr, double s1, double s2, double s3) {
            firstName = fName;
            lastName = lName;
            creditHours = cHours;
            major = mjr;
            score1 = s1;
            score2 = s2;
            score3 = s3;

            updateStatus();
        }
        public string FirstName {
            get {return firstName;}
            set {firstName = value;}
        }
        public string LastName {
            get {return lastName;}
            set {lastName = value;}
        }
        public string Major {
            get {return major;}
            set {major = value;}
        }
        public int CreditHours {
            get {return creditHours;}
        }
        public string getName() {
            return $"{firstName} {lastName}";
        }
        public void addHours(int hours) {
            creditHours += hours;
            updateStatus();
        }
        public void updateStatus() {
            if (creditHours >= 90) {
                classStatus = Status.Senior;
            } else if (creditHours >= 60) {
                classStatus = Status.Junior;
            } else if (creditHours >= 30) {
                classStatus = Status.Sophomore;
            } else {
                classStatus = Status.Freshmen;
            }
        }
        public double getAverageScore() {
            double scoreSum = score1 + score2 + score3;
            return Math.Round(scoreSum/3, 2);
        }
        public char getLetterGrade() {
            double avgScore = getAverageScore();
            if (avgScore >= 90) {
                return 'A';
            } else if (avgScore >= 80) {
                return 'B';
            } else if (avgScore >= 70) {
                return 'C';
            } else if (avgScore >= 60) {
                return 'D';
            } else {
                return 'F';
            }
        }
        public string getStudentInfo() {
            string info = $"{getName()}: {classStatus}({major})\nAverage Score: {getAverageScore()}% -- Grade: {getLetterGrade()}\n\n";
            return info;
        }
    }
    static void Main(string[] args)
    {
        string[] studentDatabase = File.ReadAllLines("student_data.csv");
        List<Student> studentDataStorage = new List<Student>();
        foreach (string line in studentDatabase) {
                    string[] studentData = line.Split(",");

                    string fn = studentData[0];
                    string ln = studentData[1];
                    int ch = Convert.ToInt32(studentData[2]);
                    string major = studentData[3];
                    double s1 = Convert.ToDouble(studentData[4]);
                    double s2 = Convert.ToDouble(studentData[5]);
                    double s3 = Convert.ToDouble(studentData[6]);

                    Student student = new Student(fn, ln, ch, major, s1, s2, s3);
                    Console.WriteLine($"{student.getStudentInfo()}");
                    studentDataStorage.Add(student);
                }

        File.WriteAllText("report.txt", "--------------Student Grade Report--------------\n------------------------------------------------\n");
        foreach (Student student in studentDataStorage) {
                File.AppendAllText("report.txt", student.getStudentInfo());
        }
        
    }
}
