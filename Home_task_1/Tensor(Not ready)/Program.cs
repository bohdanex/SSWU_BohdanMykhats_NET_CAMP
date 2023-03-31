namespace Tensor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tensor tn = new Tensor(rank: 4, 2, 3, 4, 5);
            tn.GetOneDemensionalIndex(1,2,3,4);
        }
    }

    public class Tensor
    {
        private byte _rank;
        private int[] _tensor;
        private int[] _maxIndexPerDimension;

        public byte Rank { get { return _rank; } }

        public Tensor(byte rank, params int[] sizeOfEachDimension)
        {
            if(rank == 0)
            {
                _tensor = new int[1];
                //коли ранг тензора 0 (скаляр), то sizeOfEachDemention ігнорується
                _maxIndexPerDimension = new int[1] {0};
            }
            else if (sizeOfEachDimension.Length != rank)
            {
                //Задані розмірності вимірів тензора не задовільняють умову рангу
                //якщо тензор другого рангу (rank=2) тобто двовимірна матриця, то масив з розмірностями
                //має бути таким: [2,5], тобто висота матриці - 2, а ширина - 5
                throw new InvalidDataException("The dimensions of a tensor does not satisfy rank condition");
            }
            //Якщо задані розмірності вимірів тензора відповідають рангу, то утворити тензор
            else
            {
                //Добуток розімрів багатовимірних матриць дорівнює довжині одновимірної матриці
                _tensor = new int[sizeOfEachDimension.Aggregate((a, b) => a * b)];
                //присвоєння кожному еелементу масиву, число, як дорівнює 'довжині кожного виміру - 1',
                //щоб не робити додаткові оперрації при перевірці (ітерації циклу)
                //у методі GetOneDemensionalIndex();
                
                _maxIndexPerDimension = new int[sizeOfEachDimension.Length];
                for (int x = 0; x < sizeOfEachDimension.Length; ++x)
                {
                    _maxIndexPerDimension[x] = sizeOfEachDimension[x] - 1;
                }
            }
            _rank = rank;
        }

        //Переробити на private!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public int GetOneDemensionalIndex(params int[] tensorIndeces)
        {
            if (tensorIndeces.Length != _rank)
            {
                //Задані індекси не відповідають рангу тензора
                throw new InvalidDataException($"Incorrect number of indexes. Tensor rank is {_rank}");
            }
            for(int indexSizeChecker = 0; indexSizeChecker < tensorIndeces.Length; ++indexSizeChecker)
            {
                if (tensorIndeces[indexSizeChecker] > _maxIndexPerDimension[indexSizeChecker])
                {
                    //Індекс виходить за межі розмірності тензора
                    throw new IndexOutOfRangeException("Index position is outside the tensor dimension size");
                }
            }
            
            int tensorIndicesMaxPosition = tensorIndeces.Length - 1;
            int oneDimensionalIndexPostion = 0;

            for(int currentIndex = 0; currentIndex < tensorIndicesMaxPosition; ++currentIndex)
            {
                int dimensionSizeCounter = 1;
                
                for(int sizeIndexer = currentIndex + 1; sizeIndexer < _maxIndexPerDimension.Length; ++sizeIndexer)
                {
                    dimensionSizeCounter *= _maxIndexPerDimension[sizeIndexer] + 1;
                }

                oneDimensionalIndexPostion += tensorIndeces[currentIndex] * dimensionSizeCounter; 
            }

            //додати останній елемент
            oneDimensionalIndexPostion += tensorIndeces[tensorIndeces.Length - 1];

            return oneDimensionalIndexPostion;
        }

        private int GetValueFromTensor(params int[] tensorIndeces)
        {
            return _tensor[0];
        }
        
        //Перевантаження методу для тензора рангу 0, бо параметр params
        //не може мати значення за замовчуванням
        private int GetValueFromTensor()
        {
            return _tensor[0];
        }

        private void SetValueToTensor(params int[] tensorIndeces)
        {
            if (tensorIndeces.Length != _rank)
            {
                throw new InvalidDataException($"Incorrect number of indexes. Tensor rank is {_rank}");
            }
        }

        private void SetTensorValue(int value)
        {
            _tensor[0] = value;
        }
    }
}