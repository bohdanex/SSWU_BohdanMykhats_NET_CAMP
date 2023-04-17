using System.Text;
using Task_3.Exceptions;

namespace Task_3
{
    public class ElectricityUsage : IFormattable
    {
        private static string INFO_COLUMNS = 
            $"{"",35}{"Dates of withdrawal of indicators",-55}|{"Apartment",9} | {"Surname",-20}|{"Price per quarter",18}|{"Days passed",12}|";

        private byte _quarterNumber;
        private List<User>? _users;

        public uint ApartmentCount { get; private set; }
        public byte QuarterNumber { get => _quarterNumber; set => _quarterNumber = Validator.ValidateQuarter(value); }
        public List<User>? Users 
        {
            get => _users; 

            private set 
            { 
                if(Validator.IsValidUserCount(value, ApartmentCount))
                {
                    _users = value!;
                }
                else
                {
                    throw new ArgumentException("User count and apartment count are not matching");
                }
            } 
        }
        public decimal PricePerKilowatt { get; set; }

        public ElectricityUsage()
        {
            ApartmentCount = 0;
            QuarterNumber = 0;
            Users = null;
            PricePerKilowatt = 0;
        }

        public ElectricityUsage(uint apartmentCount, byte quarterNumber, decimal pricePerKilowatt, List<User>? users)
        {
            ApartmentCount = apartmentCount;
            QuarterNumber = quarterNumber;
            PricePerKilowatt = pricePerKilowatt;
            Users = users;
        }

        public string PrintByApartment(uint apartmentNumber)
        {
            if(Users is null)
            {
                throw new NullReferenceException("The Users is null");
            }

            try
            {
                User user = Users.First(user => user.ApartmentNumber == apartmentNumber);
                decimal pricePerQuartal = (user.OutputElectricityDisplay - user.InputElectricityDisplay) * PricePerKilowatt;
                TimeSpan interval = DateTime.Today - user.ConsumptionIndicators[2];

                StringBuilder output = new();
                output.AppendLine(INFO_COLUMNS);
                output.Append(user.ToString());
                output.AppendLine($"{pricePerQuartal,18:C}|{interval.Days,12}|");

                return output.ToString();
            }
            catch (InvalidOperationException)
            {
                throw new ApartmentNotFoundException();
            }
        }

        public string? GetMostArrearUsername()
        {
            int indexOfMostArrearUser = -1;
            decimal currentMaxArrear = 0;

            if(Users is null)
            {
                throw new NullReferenceException("The Users is null");
            }

            for(int i = 0; i < Users.Count; ++i)
            {
                decimal usageDifference = Users[i].OutputElectricityDisplay - Users[i].InputElectricityDisplay;
                decimal priceToKilowatt = Users[i].Deposit / PricePerKilowatt;
                if (usageDifference > priceToKilowatt && currentMaxArrear < priceToKilowatt)
                {
                    indexOfMostArrearUser = i;
                    currentMaxArrear = priceToKilowatt;
                }
            }

            if (indexOfMostArrearUser == -1) 
            {
                return null;
            }

            return Users[indexOfMostArrearUser].Surname;
        }

        public uint ZeroConsumptionApartment()
        {
            if(Users is null)
            {
                throw new NullReferenceException();
            }
            try
            {
                return Users.First(user => user.InputElectricityDisplay == user.OutputElectricityDisplay).ApartmentNumber;
            }
            catch(ArgumentNullException)
            {
                throw new ApartmentNotFoundException();
            }
        }

        public override string ToString()
        {
            StringBuilder output = new();
            output.AppendLine($"Quarter: {QuarterNumber}");
            output.AppendLine(INFO_COLUMNS);
            
            if(Users is not null)
            {
                foreach (User user in Users)
                {
                    decimal pricePerQuartal = (user.OutputElectricityDisplay - user.InputElectricityDisplay) * PricePerKilowatt;
                    TimeSpan interval = DateTime.Today - user.ConsumptionIndicators[2];
                    output.Append(user.ToString());
                    output.AppendLine($"{pricePerQuartal,18:C}|{interval.Days,12}|");
                }
            }
            else
            {
                throw new NullReferenceException();
            }
            return output.ToString();
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if(format is null)
            {
                return ToString();
            }

            StringBuilder output = new();
            output.AppendLine($"Quarter: {QuarterNumber}");
            output.AppendLine(INFO_COLUMNS);

            if (Users is not null)
            {
                foreach (User user in Users)
                {
                    decimal pricePerQuartal = (user.OutputElectricityDisplay - user.InputElectricityDisplay) * PricePerKilowatt;
                    TimeSpan interval = DateTime.Today - user.ConsumptionIndicators[2];
                    output.Append(user.ToString(format, formatProvider));
                    output.AppendLine($"{pricePerQuartal,18:C}|{interval.Days,12}|");
                }
            }
            else
            {
                throw new NullReferenceException();
            }
            return output.ToString();
        }
    }
}
