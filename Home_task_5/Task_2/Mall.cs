using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public class Mall
    {
        public Box MainBox { get; private set; }

        public Mall(Box boxes)
        {
            MainBox = boxes;
        }

        public override string ToString()
        {
            if(MainBox is null)
            {
                throw new ArgumentNullException(nameof(MainBox));
            }

            return MainBox.ToString();
        }

        public string ToString(bool structuredOutput = false)
        {
            if (MainBox is null)
            {
                throw new ArgumentNullException(nameof(MainBox));
            }

            StringBuilder output= new StringBuilder();
            string mallName = MainBox.Name!.ToUpper();
            const string space = "  ";
            string hyphens = new('-', (Console.WindowWidth - 4 - mallName.Length )/ 2 );

            output.Append(hyphens); output.Append(space);
            output.Append(mallName);
            output.Append(space); output.AppendLine(hyphens);
            output.AppendLine(MainBox.ToString(structuredOutput));
            output.Append(hyphens); output.Append(space);
            output.Append(mallName);
            output.Append(space); output.AppendLine(hyphens);
            return output.ToString();
        }
    }
}
