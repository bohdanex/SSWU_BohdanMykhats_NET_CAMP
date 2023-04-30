using System.Text;

namespace Tensor
{
    public class Tensor
    {
        private byte _rank;
        private int[] _tensor;
        private int[] _maxIndexPerDimension;

        public byte Rank { get { return _rank; } }

        public int this[params int[] dementionIndeces]
        {
            get
            {
                return _tensor[GetOneDemensionalIndex(dementionIndeces)];
            }
            set
            {
                _tensor[GetOneDemensionalIndex(dementionIndeces)] = value;
            }
        }

        public Tensor(byte rank, params int[] sizeOfEachDimension)
        {
            this._rank = rank;
            this._maxIndexPerDimension = sizeOfEachDimension;
            this._tensor = new int[] { 0 };
            //По суті в цей метод скопійовано все що була раніше в конструкторі.
            //Це зроблено тому, що користувач може мати намір змінити ранг матриці.
            //Тому для притримування правила D.R.Y (Don't Repeat Yourself) був зроблений окремий метод
            SetTensorRank(rank, sizeOfEachDimension);
        }

        private int GetOneDemensionalIndex(params int[] tensorIndeces)
        {
            if(_rank == 0) return 0;

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

        public void SetTensorRank(byte rank, params int[] sizeOfEachDimension)
        {
            if (rank == 0)
            {
                _tensor = new int[1];
                //коли ранг тензора 0 (скаляр), то sizeOfEachDemention ігнорується
                _maxIndexPerDimension = new int[1] { 0 };
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
                //присвоєння кожному еелементу масиву, число, яке дорівнює 'довжині кожного виміру - 1',
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

        public void FillWithRandom(int minRandom = 0, int maxRandom = 82)
        {
            Random random = new();
            for (int i = 0; i < _tensor.Length; ++i)
            {
                _tensor[i] = random.Next(minRandom, maxRandom);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            if(_rank == 0)
            {
                return this[0].ToString();
            }
            for (int i = 0; i < _tensor.Length; ++i)
            {
                sb.Append($"{_tensor[i],-4} ");
                if (i % 8 == 7) sb.AppendLine();
            }
            return sb.ToString();
        }

        public static implicit operator Tensor(int value)
        {
            Tensor tensor = new Tensor(0,0);
            tensor[0] = value;
            return tensor;
        }
    }
}