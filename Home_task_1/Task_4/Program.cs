﻿namespace Tensor
{
    internal class Program
    {// Але я не зможу задати таку конструкцію int[,] a; Tensor t=a; Xb
        static void Main(string[] args)
        {
            try
            {
                //Ініціалізація тензора як тривимірної матриці (тензор рангу 3)
                Tensor tensor = new(3, 3,5,7);
                tensor = 2;
                tensor.FillWithRandom();
                Console.WriteLine(tensor);

                //Спробуємо записати значення у тензор, а потім це значення завантажити з нього
                Console.WriteLine("\nset value to tensor at indices [2, 3, 1] = 12;");
                tensor[2, 3, 1] = 12;
                int valueFromTensor = tensor[2, 3, 1];
                Console.WriteLine($"tensor[2, 3, 1] = {valueFromTensor}");

                //неявне приведення типу int до типу Tensor
                //У такому випадку тензор - це скаляр
                tensor = 152;
                Console.WriteLine($"\nOne dimensional tensor (scalar): {tensor}");

                //Спроба наткнутися на виняток
                Console.WriteLine("Trying to change tensor rank...");
                tensor.SetTensorRank(1, 2 );
                

                Tensor tensor2 = new(1, 3);
                tensor2[1] = 2;
                tensor2[0] = 1;
                Console.WriteLine(tensor2);

                Tensor zero = new(0);
            }
            catch(InvalidDataException e)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(e.Message);
            }
            catch(IndexOutOfRangeException e2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e2.Message);
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
