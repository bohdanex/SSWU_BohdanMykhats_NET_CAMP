namespace Task_3
{
    public static class Validator
    {
        public static byte ValidateQuarter(byte quarterNumber)
        {
            if(quarterNumber > 4)
            {
                throw new ArgumentException("The quarter number must be between 0 and 4");
            }

            return quarterNumber;
        }

        public static bool IsValidUserCount(List<User>? users, uint apartmentCount)
        {
            if (users is null && apartmentCount == 0)
            {
                return true;
            }
            else if(users!.Count != apartmentCount)
            {
                return false;
            }

            return true;
        }

        public static DateTime[] ValidateIndicatorsDateTime(DateTime[] dates)
        {
            if (dates.Length != 3)
            {
                throw new ArgumentException("The quarter has only three months");
            }

            foreach (DateTime date in dates)
            {
                if (date > DateTime.Now)
                {
                    throw new ArgumentException("Invalid date time");
                }
            }

            for(int i = 1; i < dates.Length; ++i)
            {
                if (dates[i] < dates[i - 1])
                {
                    throw new ArgumentException("The months must follow each other");
                }
            }

            return dates;
        }

        public static decimal ValidateIndicator(decimal inputIndicator, decimal outputIndicator)
        {
            if(IsLessThanZero(inputIndicator) || IsLessThanZero(outputIndicator))
            {
                throw new ArgumentException("The input values must be more than or equal to zero");
            }
            if(inputIndicator > outputIndicator)
            {
                throw new ArgumentException("The output indicator number must more than input");
            };

            return outputIndicator;
        }

        public static string ValidateUserSurname(string userSurname)
        {
            if(userSurname.Length < 3 || userSurname.Length > 18)
            {
                throw new ArgumentException("The length of the user's surname must be more than 3 and less than 19");
            }

            return userSurname;
        }

        public static bool IsLessThanZero(decimal value)
        {
            if(value < 0)
            {
                return true;
            }
                
            return false;
        }
    }
}
