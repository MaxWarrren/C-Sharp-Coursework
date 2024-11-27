using System.Dynamic;
using System.Reflection;
using System.Linq;
using System.Net.Sockets;

namespace Crime_Analyzer;

class Program
{   
    class CrimeYear {
        //holds all data for a specific year
        public int Year {get; set;}
        public Dictionary<string, int> crimeData = new Dictionary<string, int>();
        public void addCrimeStat(string type, int instances) {
            crimeData.Add(type, instances);
        }

        public void logData() {
            Console.WriteLine($"Test Log Of Year {Year}");
            foreach (KeyValuePair<string, int> stat in crimeData) {
            Console.WriteLine($"{stat.Key} | {stat.Value}");
        }
        }
    }
    static void Main(string[] args)
    {
        string csvPath = args[0];
        string dataReportPath = args[1];
        
        string[] rawCrimeData = File.ReadAllLines(csvPath);
        string[] crimeCategories = rawCrimeData[0].Split(',');

        List<CrimeYear> allCrimeData = new List<CrimeYear>();

        for (int n=1; n<rawCrimeData.Length; n++) {
            string[] rawCrimeYearData = rawCrimeData[n].Split(',');
            CrimeYear crimeYear = new CrimeYear() {Year = Convert.ToInt32(rawCrimeYearData[0])};

            for (int i=1; i<rawCrimeYearData.Length; i++) {
                int crimeCount = Convert.ToInt32(rawCrimeYearData[i]);
                string crimeType = crimeCategories[i]; 
                crimeYear.addCrimeStat(crimeType, crimeCount);
            }

            allCrimeData.Add(crimeYear);
        }

        //----------------------------
        /*Console.WriteLine("Crime Anaylzer Report");
        int yearsOfData = allCrimeData.Count;
        var linqQuery = from CrimeYear in allCrimeData where CrimeYear.crimeData["Murder"] < 15000 select CrimeYear.Year;
        int[] murdersBelow15000 = linqQuery.ToArray();
         

        Console.WriteLine("Crime Anaylzer Report");
        Console.WriteLine($"Period: {allCrimeData[0].Year} - {allCrimeData[yearsOfData-1].Year} | {yearsOfData} years");
        Console.WriteLine("Years with less than 15000 murders | " + string.Join(", ", murdersBelow15000));*/


       int[] nums = { 1, -2, 3, 0, -4, 5};
        var posNums = from n in nums
                        where n >= 0
                        select n;
        foreach (int i in posNums){
            Console.Write(i + " ");
        }
    }
}
