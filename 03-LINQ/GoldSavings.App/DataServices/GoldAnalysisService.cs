using System;
using System.Collections.Generic;
using System.Linq;
using GoldSavings.App.Model;

namespace GoldSavings.App.Services
{
    public class GoldAnalysisService
    {
        private readonly List<GoldPrice> _goldPrices;

        public GoldAnalysisService(List<GoldPrice> goldPrices)
        {
            _goldPrices = goldPrices;
        }
        public double GetAveragePrice()
        {
            return _goldPrices.Average(p => p.Price);
        }
        //a method
        public List<GoldPrice> GetTop3Prices()
        {
            var Yearoffset = DateTime.Now.AddYears(-1);
            var tempPrices = _goldPrices
                .Where(p => p.Date >= Yearoffset)
                .ToList();

            return tempPrices.OrderByDescending(p => p.Price).Take(3).ToList();

        }
        public List<GoldPrice> GetDown3Prices()
        {
            var Yearoffset = DateTime.Now.AddYears(-1);
            var tempPrices = _goldPrices
                .Where(p => p.Date >= Yearoffset)
                .ToList();

            return tempPrices.OrderBy(p => p.Price).Take(3).ToList();

        }
        // a query
        public List<GoldPrice> GetTop3Prices_Query()
        {
            var Yearoffset = DateTime.Now.AddYears(-1);
            var tempPrices = (from p in _goldPrices
                where p.Date >= Yearoffset
                orderby p.Price descending
                select p).Take(3).ToList();

            return tempPrices;

        }
        public List<GoldPrice> GetDown3Prices_Query()
        {
            var Yearoffset = DateTime.Now.AddYears(-1);
            var tempPrices = (from p in _goldPrices
                where p.Date >= Yearoffset
                orderby p.Price ascending
                select p).Take(3).ToList();

            return tempPrices;

        }
        // b
        public List<GoldPrice> GetEarnDays()
        {
            var january = (from p in _goldPrices
                where p.Date >= new DateTime(2020, 1, 1) && p.Date <= new DateTime(2020, 1, 31)
                select p).ToList();

            var EarningDays = new List<GoldPrice>();
            foreach (var p in january){
                var days = (from d in _goldPrices
                where d.Price >= p.Price * 1.05 && d.Date > p.Date
                select d).ToList();
                EarningDays.AddRange(days);
            }
            return EarningDays.Take(10).ToList();
        }
        // c
        public List<GoldPrice> GetSecondTen()
        {
            var dates = (from p in _goldPrices
                where p.Date >= new DateTime(2019, 1, 1) && p.Date <= new DateTime(2022, 12,31)
                orderby p.Price descending
                select p).Skip(10).Take(3).ToList();
            
            return dates;
        }
        // d
        public List<GoldPrice> GetYearlyAverge()
        {
            var averagePrices = (from p in _goldPrices
                where p.Date.Year == 2020 || p.Date.Year == 2023 || p.Date.Year == 2024
                group p by p.Date.Year into g
                select new GoldPrice
                {
                    Date = new DateTime(g.Key, 1, 1),
                    Price = g.Average(p => p.Price)
                }
                ).ToList();

            return averagePrices;

        }
        // e
        public (GoldPrice? bestBuy, GoldPrice? bestSell, double? returnOnInvestment) GetBestInvestment()
        {

                var bestBuy = (from p in _goldPrices
                            where p.Date.Year >= 2020 && p.Date.Year <= 2024
                            orderby p.Price
                            select p).FirstOrDefault();

                if (bestBuy == null)
                    return (null, null, null);

                var bestSell = (from p in _goldPrices
                                where p.Date > bestBuy.Date
                                orderby p.Price descending
                                select p).FirstOrDefault();

                if (bestSell == null)
                    return (bestBuy, null, null);

                double roi = ((bestSell.Price - bestBuy.Price) / bestBuy.Price) * 100;

                return (bestBuy, bestSell, roi);
        

        }
    }
}
