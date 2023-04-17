namespace Task_3.Readers
{
    public class TxtParser : IParser
    {
        public ElectricityUsage Parse(string filePath)
        {
            using FileStream stream = new(filePath + ".txt", FileMode.Open, FileAccess.Read, FileShare.None);
            using TextReader reader = new StreamReader(stream);

            string[]? txtElectricityUsage;

            reader.ReadLine(); //skip the first row
            txtElectricityUsage = reader.ReadLine()?.Split(',', StringSplitOptions.TrimEntries);
            uint apartmentCount = Convert.ToUInt32(txtElectricityUsage[0]);
            byte quarterNumber = Convert.ToByte(txtElectricityUsage[1]);
            decimal pricePerKilowatt = Convert.ToDecimal(txtElectricityUsage[2]);

            reader.ReadLine(); //skip the third row
            string[]? txtUsers = SplitUsers(reader.ReadToEnd());
            List<User> users = new List<User>();

            if(txtUsers is not null)
            {
                foreach (string s in txtUsers)
                {
                    string[] txtUserData = s.Split(',', StringSplitOptions.TrimEntries);
                    DateTime[] dates =
                    {
                        DateTime.ParseExact(txtUserData[6], "yyyy.MM.dd", null),
                        DateTime.ParseExact(txtUserData[7], "yyyy.MM.dd", null),
                        DateTime.ParseExact(txtUserData[8], "yyyy.MM.dd", null),
                    };

                    User user = new User
                        (
                        UInt32.Parse(txtUserData[0] ?? "0"),
                        txtUserData[1],
                        txtUserData[2],
                        Decimal.Parse(txtUserData[3] ?? "0"),
                        Decimal.Parse(txtUserData[4] ?? "0"),
                        Decimal.Parse(txtUserData[5] ?? "0"),
                        dates
                        );

                    users.Add(user);
                }
            }
            else
            {
                throw new NullReferenceException();
            }
            return new ElectricityUsage(apartmentCount, quarterNumber, pricePerKilowatt, users);
        }

        private static string[]? SplitUsers(string text)
        {
            List<string> users = new List<string>();
            int OpenBraceIndex = 0;

            for(int charIndex = 0; charIndex < text.Length; ++charIndex)
            {
                if (text[charIndex].Equals('{'))
                {
                    OpenBraceIndex = charIndex;
                }
                else if (text[charIndex].Equals('}'))
                {
                    users.Add(text[(OpenBraceIndex + 1)..charIndex]);
                    ++charIndex;
                }
            }

            return users.ToArray();
        }
    }
}
