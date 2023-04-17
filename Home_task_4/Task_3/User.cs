using System.Text;

namespace Task_3
{
    public class User : IFormattable
    {
        private DateTime[] _consumptionIndicators;
        private decimal _outputElectricityDisplay;
        private decimal _deposit;

        public uint ApartmentNumber { get; set; }
        public string Address { get; set; }
        public string Surname { get; set; }
        public decimal Deposit 
        { 
            get => _deposit;
            set
            {
                if (Validator.IsLessThanZero(value))
                {
                throw new ArgumentException("The value must be more than or equal to zero");
                }
                _deposit = value;
            } 
        }
        public decimal InputElectricityDisplay { get; set; }
        public decimal OutputElectricityDisplay 
        { 
            get => _outputElectricityDisplay; 
            set
            {
                _outputElectricityDisplay = Validator.ValidateIndicator(InputElectricityDisplay, value);
            }
        }
        public DateTime[] ConsumptionIndicators 
        { 
            get => _consumptionIndicators; 
            set => _consumptionIndicators = Validator.ValidateIndicatorsDateTime(value);
        }

        public User(uint apartmentNumber, string address, string surname, decimal deposit,
            decimal inputElectricityDisplay, decimal outputElectricityDisplay, DateTime[] consumptionIndicators)
            
        {
            ApartmentNumber = apartmentNumber;
            Address = address;
            Surname = surname;
            Deposit = deposit;
            InputElectricityDisplay = inputElectricityDisplay;
            OutputElectricityDisplay = outputElectricityDisplay;
            ConsumptionIndicators = consumptionIndicators;
        }

        public override string ToString()
        {
            StringBuilder dates = new StringBuilder();
            foreach (DateTime date in ConsumptionIndicators)
            {
                dates.Append($"{date.ToString("D"),-30}");
            }

            return $"{dates}|{ApartmentNumber, 9} | {Surname, -20}|";
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            if(format != ".")
            {
                return ToString();
            }

            StringBuilder dates = new StringBuilder();
            foreach (DateTime date in ConsumptionIndicators)
            {
                dates.Append($"{date.ToString("dd.MM.yyyy"),-30}");
            }

            return $"{dates}|{ApartmentNumber, 9} | {Surname,-20}|";
        }
    }
}
