namespace SalesDataAnalyzer;
using System.Linq;
#pragma warning disable CS8618
#pragma warning disable CS0168
class Program
{   
    public class Sale {
        public string InvoiceID {get; set;}
        public char Branch {get; set;}
        public string City {get; set;}
        public string CustomerType {get; set;}
        public string Gender {get; set;}
        public string ProductLine {get; set;}
        public float UnitPrice {get; set;}
        public int Quantity {get; set;}
        public string Date {get; set;}
        public string PaymentType {get; set;}
        public float Rating {get; set;}
        public float SalePrice {
            get {return UnitPrice * Quantity;}
        }
        public int Month {
            get {return int.Parse(Date[0].ToString());}
        }
        public float SalesTax {
            get {return (float)Math.Round(SalePrice*0.05f, 2);}
        }
    }
    static void Main(string[] args)
    {
        if (args.Length != 2) {
            Console.WriteLine("ERROR: Please provide arguements in format <sales_data_file_path> <report_file_path>\n");
            return;
        }
        string salesDataPath = args[0];
        string reportFilePath = args[1];

        List<Sale> sortedSaleData = new List<Sale>();
    
        try {
            string[] rawSalesData = File.ReadAllLines(salesDataPath);
            string[] dataCategories = rawSalesData[0].Split(',');

            for (int i=1; i<rawSalesData.Length; i++) {
                string[] sale = rawSalesData[i].Split(',');
                Sale newSale = new Sale {
                    InvoiceID = sale[0],
                    Branch = Convert.ToChar(sale[1]),
                    City = sale[2],
                    CustomerType = sale[3],
                    Gender = sale[4],
                    ProductLine = sale[5],
                    UnitPrice = float.Parse(sale[6]),
                    Quantity = int.Parse(sale[7]),
                    Date = sale[8],
                    PaymentType = sale[9],
                    Rating = float.Parse(sale[10])
                };
                
                //Console.WriteLine(newSale.Month);
                sortedSaleData.Add(newSale);
            }
        } catch (Exception e) {
            Console.WriteLine($"ERROR: Could not find specified path {salesDataPath}\n");
            return;
        }


        //start LINQ queries
        
        string salesReport = "";

        salesReport += "******************************************\n";
        salesReport += "Total Sales In Dataset\n";
        salesReport += "******************************************\n";

        float totalSales = sortedSaleData.Sum(sale => sale.SalePrice);
        salesReport += totalSales + "\n";

        salesReport += "\n******************************************\n";
        salesReport += "Unique Productlines\n";
        salesReport += "******************************************\n";

        var productLines = sortedSaleData.Select(sale => sale.ProductLine).Distinct();
        foreach (string pl in productLines) {
            salesReport += pl + "\n";
        }

        salesReport += "\n******************************************\n";
        salesReport += "Total Sales Per Product Line\n";
        salesReport += "******************************************\n";

        foreach (string pl in productLines) {
            var salesPerPL = sortedSaleData.Where(sale => sale.ProductLine.Equals(pl)).Sum(sale => sale.SalePrice);
            salesReport += $"{pl}: {salesPerPL}\n";
        }

        salesReport += "\n******************************************\n";
        salesReport += "Total Sales Per City\n";
        salesReport += "******************************************\n";

        var cities = sortedSaleData.Select(sale => sale.City).Distinct();
        foreach (string city in cities) {
            var salesPerCity = sortedSaleData.Where(sale => sale.City.Equals(city)).Sum(sale=>sale.SalePrice);
            salesReport += $"{city}: {salesPerCity}\n";
        }

        salesReport += "\n******************************************\n";
        salesReport += "Product Line With The Highest Unit Price\n";
        salesReport += "******************************************\n";

        var highestPrice = sortedSaleData.Max(sale => sale.UnitPrice);
        var highestSales = sortedSaleData.Where(sale => sale.UnitPrice == highestPrice);
        foreach (var sale in highestSales) {
            salesReport += $"Product Line: {sale.ProductLine}: {sale.UnitPrice}\n";
        }

        salesReport += "\n******************************************\n";
        salesReport += "Total Sales Per Month\n";
        salesReport += "******************************************\n";

        var months = sortedSaleData.Select(sale => sale.Month).Distinct();
        foreach(var month in months) {
            var salesPerMonth = sortedSaleData.Where(sale => sale.Month==month).Sum(sale=>sale.SalePrice);
            //Console.WriteLine(month);
            salesReport += $"{month}: {salesPerMonth}\n";
        }

        salesReport += "\n******************************************\n";
        salesReport += "Total Sales Per Payment Type\n";
        salesReport += "******************************************\n";

        var paymentTypes = sortedSaleData.Select(sale => sale.PaymentType).Distinct();
        foreach (string pt in paymentTypes) {
            var salesPerPT = sortedSaleData.Where(sale => sale.PaymentType.Equals(pt)).Sum(sale=>sale.SalePrice);
            salesReport += $"{pt}: {salesPerPT}\n";
        }  

        salesReport += "\n******************************************\n";
        salesReport += "Total Transactions By Member Type\n";
        salesReport += "******************************************\n";

        var customerTypes = sortedSaleData.Select(sale => sale.CustomerType).Distinct();
        foreach (string ct in customerTypes) {
            var transactionsPerCT = sortedSaleData.Where(sale => sale.CustomerType.Equals(ct)).Count();
            salesReport += $"{ct}: {transactionsPerCT}\n";
        }

        salesReport += "\n******************************************\n";
        salesReport += "Average Rating Per Product Line\n";
        salesReport += "******************************************\n";

        foreach (var pl in productLines) {
            var plInstances = sortedSaleData.Where(sale => sale.ProductLine.Equals(pl));
            float avgRating = plInstances.Count() / plInstances.Sum(sale => sale.Rating);
            salesReport += $"{pl}: {avgRating}\n";
        }

        salesReport += "\n******************************************\n";
        salesReport += "Total Transaction By Payment Type\n";
        salesReport += "******************************************\n";

        foreach (var pt in paymentTypes) {
            var transactionByPT = sortedSaleData.Where(sale => sale.PaymentType.Equals(pt)).Count();
            salesReport += $"{pt}: {transactionByPT}\n";
        }

        salesReport += "\n******************************************\n";
        salesReport += "Number Of Products Sold Per City\n";
        salesReport += "******************************************\n";

        foreach (var city in cities) {
            var productsSoldPerCity = sortedSaleData.Where(sale => sale.City.Equals(city)).Sum(sale => sale.Quantity);
            salesReport += $"{city}: {productsSoldPerCity}\n";
        }

        salesReport += "\n******************************************\n";
        salesReport += "Tax Sale Per Product Line\n";
        salesReport += "******************************************\n";

        foreach (var pl in productLines) {
            var allSalesInPLType = sortedSaleData.Where(sale => sale.ProductLine.Equals(pl));
            salesReport += $"\n*************{pl}*************\n";
            foreach (var sale in allSalesInPLType) {
                salesReport += $"Invoice-ID: {sale.InvoiceID} - Total: {sale.SalePrice} - Tax: {sale.SalesTax}\n";
            }
        }

        File.WriteAllText(reportFilePath, salesReport);

    }
}
