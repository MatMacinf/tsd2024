using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoldSavings.App.Client;
using GoldSavings.App.Model;
using System.Xml.Serialization;

namespace GoldSavings.App.Services
{
    public class GoldDataService
    {
        private readonly GoldClient _goldClient;

        public GoldDataService()
        {
            _goldClient = new GoldClient();
        }

        public async Task<List<GoldPrice>> GetGoldPrices(DateTime startDate, DateTime endDate)
        {
            var prices = await _goldClient.GetGoldPrices(startDate, endDate);
            return prices ?? new List<GoldPrice>();  // Prevent null values
        }

        public void SavePricesToXML(List<GoldPrice> goldPrices, string filePath)
        {
          
            XmlSerializer serializer = new XmlSerializer(typeof(List<GoldPrice>));            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, goldPrices);
            }

            Console.WriteLine("Gold prices saved successfully to " + filePath);

        }
        public List<GoldPrice> ReadGoldPricesFromFile(string filePath) =>
            (List<GoldPrice>)new XmlSerializer(typeof(List<GoldPrice>)).Deserialize(new StreamReader(filePath));
    }   
}
