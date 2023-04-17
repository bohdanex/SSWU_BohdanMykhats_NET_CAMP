using System.Text.Json;

namespace Task_3.Readers
{
    public class JsonParser : IParser
    {
        public ElectricityUsage Parse(string filePath)
        {
            using FileStream fileStream = new(filePath + ".json", FileMode.Open);
            using JsonDocument jsonDocument = JsonDocument.Parse(fileStream);

            JsonElement jsonElement = jsonDocument.RootElement;

            uint apartmentCount = jsonElement.GetProperty("ApartmentCount").GetUInt32();
            byte quarterNumber = jsonElement.GetProperty("QuarterNumber").GetByte();
            decimal pricePerKilowatt = jsonElement.GetProperty("PricePerKilowatt").GetDecimal();

            List<User>? users = jsonElement.GetProperty("Users").Deserialize<List<User>>();

            return new ElectricityUsage(apartmentCount, quarterNumber, pricePerKilowatt, users);
        }
    }
}
