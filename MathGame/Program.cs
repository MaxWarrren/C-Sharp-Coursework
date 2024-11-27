using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MathGame;

class Program
{   
    static int getIntInRange(int x, int y, string message) {
        int n;
        while (true) {
            Console.WriteLine(message);
            string res = Console.ReadLine();
            try {
                n = Convert.ToInt32(res);
                if (x<=n && n<=y) {
                    return n;
                } else {
                    Console.WriteLine("ERROR: Must be in specified range\n");
                }
            } catch (Exception e) {
                Console.WriteLine("ERROR: Must enter a number\n");
            }
        }
    }

    static Dictionary<string, int> generateExpressions(int dif, int qCnt) {
        Dictionary<string, int> newExpressions = new Dictionary<string, int>();
        Random r = new Random();

        string exp;
        int n1, n2, min, max;
        
        min = (int)Math.Pow(10, dif-1);
        max = (int)Math.Pow(10, dif);

        for (int z=0; z<qCnt; z++) {
            n1 = r.Next(min, max);
            n2 = r.Next(min, max);
            exp = $"{n1} + {n2} = ";
            newExpressions.Add(exp, n1+n2);
        }

        return newExpressions;
    }

    static void Main(string[] args)
    {
        Dictionary<string, int> expressions = new Dictionary<string, int>();
        string userName, res;
        int difficulty, questionCnt;
        int answersCorrect = 0, questionOn = 1;

        while (true) {
            Console.WriteLine("Enter player name");
            res = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(res)) {
                userName = res;
                break;
            } else {
                Console.WriteLine("ERROR! You must enter a name\n");
            }
        }

        Console.WriteLine($"\nWelcome {userName}!\n");

        difficulty = getIntInRange(1, 3, "Enter difficulty level (1, 2, 3)");
        questionCnt = getIntInRange(3, 10, "\nEnter the number of questions to ask (3, 10)");

        Console.WriteLine($"Generating {questionCnt} questions at difficulty level {difficulty}...");

        expressions = generateExpressions(difficulty, questionCnt);


        foreach (string exp in expressions.Keys) {
            int correctAnswer = expressions[exp];
            int guessesLeft = 3, userAnswer;

            while (guessesLeft > 0) {
                Console.WriteLine($"Question {questionOn}: {exp}");
                res = Console.ReadLine();
                try {
                    userAnswer = Convert.ToInt32(res);
                    if (userAnswer == correctAnswer) {
                        Console.WriteLine("YaY! You Got It Right!!!");
                        answersCorrect++;
                        break;
                    } else {
                        Console.WriteLine("OOPS! You Got it WRONG!!!");
                        guessesLeft--;
                    }
                } catch (Exception e) {
                    Console.WriteLine("OOPS! You Got it WRONG!!!");
                    guessesLeft--;
                }
            } 

            if (guessesLeft == 0) {
                Console.WriteLine($"Correct Answer: {correctAnswer}");
            }

            questionOn++;
        }

        double gradePercent = 100*Math.Round((double)answersCorrect/(double)questionCnt, 2);
        Console.WriteLine($"Congratulations {userName}! You got {answersCorrect} ouf of {questionCnt} questions correct: {gradePercent}%");
    }
}
