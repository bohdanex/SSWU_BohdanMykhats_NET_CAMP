using Task_3.Exceptions;
using Task_3.Readers;

namespace Task_3
{
    public static class ConsoleAppPresenter
    {
        public static void RunApp()
        {

            ElectricityManager electricityManager = new ElectricityManager();

            try
            {
                bool isFileFound = false;
                while (!isFileFound)
                {
                    try
                    {
                        Console.Write("Enter a year -> ");
                        uint year = Convert.ToUInt32(Console.ReadLine());
                        Console.Write("Enter a quarter -> ");
                        byte quarter = Convert.ToByte(Console.ReadLine());
                        electricityManager.ReadFile(year, quarter, new TxtParser());
                        isFileFound = true;
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("File not found. Please re-enter the data");
                    }
                }

                if (electricityManager.ElectricityUsage is not null)
                {
                    ElectricityUsage electricityUsage = electricityManager.ElectricityUsage;
                    Console.WriteLine(electricityUsage.ToString(".", null));

                    Console.WriteLine("\nPrint by apartment (#12)\n");
                    try
                    {
                        Console.WriteLine(electricityUsage.PrintByApartment(12));
                    }
                    catch (ApartmentNotFoundException)
                    {
                        Console.WriteLine("Apartment not found");
                    }

                    Console.WriteLine("\nName of the owner with the largest debt: " + electricityUsage.GetMostArrearUsername());

                    Console.WriteLine("Apartment with no consumption: " + electricityUsage.ZeroConsumptionApartment());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
