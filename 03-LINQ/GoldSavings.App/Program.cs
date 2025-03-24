using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Investor!");

        // Step 1: Get gold prices
        GoldDataService dataService = new GoldDataService();
        DateTime startDate_1 = new DateTime(2019,12,31);
        DateTime endDate_1 = new DateTime(2020,12,31);
        DateTime endDate_2 = new DateTime(2021,12,31);
        DateTime endDate_3 = new DateTime(2022,12,31);                
        DateTime endDate_4 = new DateTime(2023,12,31);
        DateTime endDate_5 = new DateTime(2024,12,31);
        DateTime endDate_6 = DateTime.Now;
        List<GoldPrice> goldPrices1 = dataService.GetGoldPrices(startDate_1, endDate_1).GetAwaiter().GetResult();
        List<GoldPrice> goldPrices2 = dataService.GetGoldPrices(endDate_1, endDate_2).GetAwaiter().GetResult();
        List<GoldPrice> goldPrices3 = dataService.GetGoldPrices(endDate_2, endDate_3).GetAwaiter().GetResult();
        List<GoldPrice> goldPrices4 = dataService.GetGoldPrices(endDate_3, endDate_4).GetAwaiter().GetResult();
        List<GoldPrice> goldPrices5 = dataService.GetGoldPrices(endDate_4, endDate_5).GetAwaiter().GetResult();
        List<GoldPrice> goldPrices6 = dataService.GetGoldPrices(endDate_5, endDate_6).GetAwaiter().GetResult();
        goldPrices1.AddRange(goldPrices2);
        goldPrices1.AddRange(goldPrices3);
        goldPrices1.AddRange(goldPrices4);
        goldPrices1.AddRange(goldPrices5);
        goldPrices1.AddRange(goldPrices6);
        DateTime endDate_7= new DateTime(2018,12,31);
        List<GoldPrice> goldPrices7 = dataService.GetGoldPrices(endDate_7, startDate_1).GetAwaiter().GetResult();
        goldPrices1.AddRange(goldPrices7);
        var goldPrices = dataService.ReadGoldPricesFromFile("prices.xml");
        if (goldPrices.Count == 0)
        {
            Console.WriteLine("No data found. Exiting.");
            return;
        }

        Console.WriteLine($"Retrieved {goldPrices1.Count} records. Ready for analysis.");
        dataService.SavePricesToXML(goldPrices1, "prices.xml");
        
        // Step 2: Perform analysis
        GoldAnalysisService analysisService = new GoldAnalysisService(goldPrices);
        var avgPrice = analysisService.GetAveragePrice();
        var top3Prices = analysisService.GetTop3Prices();
        var down3Prices = analysisService.GetDown3Prices();
        var top3Prices_query = analysisService.GetTop3Prices_Query();
        var down3Prices_query = analysisService.GetDown3Prices_Query();
        var earningDays = analysisService.GetEarnDays();
        var secondDates = analysisService.GetSecondTen();
        var averagePrices = analysisService.GetYearlyAverge();
        var bestBuy_res= analysisService.GetBestInvestment();

        // Step 3: Print results
        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice, 2), "Average Gold Price Last Half Year");
        GoldResultPrinter.PrintPrices(top3Prices, "Top 3 prices");
        GoldResultPrinter.PrintPrices(down3Prices, "Lowest 3 prices");
        GoldResultPrinter.PrintPrices(top3Prices_query, "Top 3 prices query");
        GoldResultPrinter.PrintPrices(down3Prices_query, "Lowest 3 prices query");
        GoldResultPrinter.PrintPrices(earningDays, "5% Days");
        GoldResultPrinter.PrintPrices(secondDates, "Second ten");
        GoldResultPrinter.PrintPrices(averagePrices, "Average prices");
        if (bestBuy_res.bestBuy != null)
        {
            GoldResultPrinter.PrintSingleValue(bestBuy_res.bestBuy.Date.ToString("yyyy-MM-dd"), "Best Buy Date");
            GoldResultPrinter.PrintSingleValue(bestBuy_res.bestBuy.Price, "Best Buy Price");
        }

        if (bestBuy_res.bestSell != null)
        {
            GoldResultPrinter.PrintSingleValue(bestBuy_res.bestSell.Date.ToString("yyyy-MM-dd"), "Best Sell Date");
            GoldResultPrinter.PrintSingleValue(bestBuy_res.bestSell.Price, "Best Sell Price");
        }

        if (bestBuy_res.returnOnInvestment.HasValue)
        {
            GoldResultPrinter.PrintSingleValue($"{bestBuy_res.returnOnInvestment.Value:F2}%", "Return on Investment");
        }
        else
        {
            Console.WriteLine("No valid investment period found.");
        }
        
        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

        Console.WriteLine("----------Task2----------");
        Satisfactory<int> Task2 = new Satisfactory<int>();
        int year = 2024;
        int year_2 = 2023;
        Console.WriteLine($"Is {year} a leap year? - {Task2.isLeapYear(year)}");
        Console.WriteLine($"Is {year_2} a leap year? - {Task2.isLeapYear(year_2)}");
        Console.WriteLine($"List is empty: {Task2.IsEmpty()}");
        Task2.Add(10);
        Task2.Add(20);
        Task2.Add(30);
        Task2.Add(40);
        Console.WriteLine("Added elements to List");
        Console.WriteLine($"List is empty: {Task2.IsEmpty()}");
        Console.WriteLine($"Random element up to index 2: {Task2.Get(2)}");
        Console.WriteLine($"Random element up to index 3: {Task2.Get(3)}");

    }
}
