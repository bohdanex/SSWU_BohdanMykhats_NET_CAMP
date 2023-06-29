using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text;

namespace CrossRoads
{
    [Serializable]
    public class MyClass
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public override string ToString()
        {
            return $"Id: {Id}, {Name}";
        }
    }

    [Serializable]
    public class MyClassCollection
    {
        private List<MyClass> myClasses;

        public MyClassCollection(IEnumerable<MyClass> myClass)
        {
            myClasses = myClass.ToList();
        }

        public void Add(MyClass myClass)
        {
            myClasses.Add(myClass);
        }

        public string JsonSerialized()
        {
            return JsonConvert.SerializeObject(myClasses);
        }

        public void Deserealize(string json)
        {
            myClasses = JsonConvert.DeserializeObject<List<MyClass>>(json)!;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            foreach (MyClass myClass in myClasses)
            {
                sb.Append(myClass.ToString());
            }

            return sb.ToString();
        }
    }
}
