using System.Text;

namespace Task_2
{
    public static class MallManager
    {
        public static string FindPathToProduct(Box box, params string[] products)
        {
            StringBuilder sb = new();
            foreach (string product in products)
            {
                sb.Append("Path to ");
                sb.AppendLine(product);
                sb.AppendLine(PathToProduct(box, product));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private static string PathToProduct(Box box, string product, byte offsetCounter = 0)
        {
            List<string> paths = new();
            foreach (Item item in box.Items!)
            {
                string itemName = String.Empty;

                StringBuilder offsetAsText = new("");
                byte i = offsetCounter;
                while (i > 0)
                {
                    offsetAsText.Append(" ");
                    --i;
                }
                
                if (item.Name == product && item is Product)
                {
                    itemName = item.ToString();
                }

                else if (item is Box)
                {
                    
                    itemName = PathToProduct((Box)item, product, ++offsetCounter);
                }
                if (!String.IsNullOrEmpty(itemName))
                {
                    paths.Add(box.Name!);
                    paths.Add(itemName);
                    return paths.Aggregate((left, right) => left + $"\n{offsetAsText}|>" + right);
                }
            }

            return String.Empty;
        }

        public static Box? ZipBoxes(string text)
        {
            Box? mainBox = null;

            List<string> fullPathsToProducts = SplitPaths(text);
            foreach (string path in fullPathsToProducts)
            {
                string[] itemNames = path.Split('>');
                if(itemNames.Length == 1)
                {
                    return new Box(itemNames[0], null);
                }
                Box leafBox = new(itemNames[^1].ToString(), null);
                if (itemNames[^1].Contains("(") && itemNames[^1].Contains(")") && itemNames[^1].Contains(" "))
                {
                    List<Item> products = GetProducts(itemNames[^1]);
                    leafBox = new(itemNames[^2].ToString(), products);
                }
                else
                {
                    List<Box> resultBoxes= new List<Box>();
                    foreach (string item in itemNames)
                    {
                        resultBoxes.Add(new Box(item, null));
                    }
                    return MergeBoxes(new Stack<Box>(resultBoxes));
                }
                
                if (mainBox is null)
                {
                    
                    List<Box> boxTree= new List<Box>();
                    for (int i = 0; i < itemNames.Length - 2; ++i)
                    {
                        boxTree.Add(new Box(itemNames[i].ToString(), null));
                    }
                    boxTree.Add(leafBox);
                    mainBox = MergeBoxes(new Stack<Box>(boxTree));
                }
                else
                {
                    Box boxLocked = mainBox;
                    for (int i = 0; i < itemNames.Length - 2; ++i)
                    {
                        Box? currentBox = GetBoxByDepartment(mainBox, itemNames[i]);
                        if(currentBox is not null)
                        {
                            boxLocked= currentBox;
                        }
                        if(currentBox is null || (currentBox is not null && i == itemNames.Length - 3))
                        {
                            i += Convert.ToInt32(currentBox is not null);
                            Stack<Box> stackBoxes = new Stack<Box>();
                            for (int j = i; j < itemNames.Length - 2; ++j)
                            {
                                stackBoxes.Push(new Box(itemNames[j],null));
                            }
                            stackBoxes.Push(leafBox);
                            boxLocked.AddItem(MergeBoxes(stackBoxes));
                            break;
                        }
                    }
                }
            }
            return mainBox;
        }
        private static Box? GetBoxByDepartment(Box box, string department)
        {
            if(box.Name == department)
            {
                return box;
            }
            else
            {
                foreach (Item item in box.Items!)
                {
                    if(item is Box)
                    {
                        Box? fromLoop = GetBoxByDepartment(item as Box, department);
                        if(fromLoop is not null)
                        {
                            return fromLoop;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return null;
        }

        private static Box MergeBoxes(Stack<Box> boxes)
        {
            if (boxes.Count == 1)
            {
                return boxes.Pop();
            }
            Box rightBox = boxes.Pop();
            Box leftBox = boxes.Pop();
            leftBox.Items = new List<Item> { rightBox };
            boxes.Push(leftBox);
            

            return MergeBoxes(boxes);
        }

        private static List<Item> GetProducts(string text)
        {
            List<Item> products = new();
            string[] productAsText = text.Split(',', StringSplitOptions.TrimEntries);
            foreach (string s in productAsText)
            {
                string productName = s[0..(s.IndexOf('('))];
                string[] dimensions = s[(s.IndexOf('(') + 1)..(s.IndexOf(')'))].Split(' ', StringSplitOptions.TrimEntries);
                double height = double.Parse(dimensions[0]);
                double width = double.Parse(dimensions[1]);
                double length = double.Parse(dimensions[2]);
                products.Add(new Product(productName, height, width, length));
            }
            return products;
        }

        private static List<string> SplitPaths(string text)
        {
            const char hat = '^';
            const char goTo = '>';
            List<string> pathToProducts = new List<string>();

            for (int i = 1; i < text.Length; ++i)
            {
                if (text[i].Equals(hat))
                {
                    int removeIndex = FindSecondOccurenceFromEnd(text[0..i], goTo) + 1;
                    if (!text[i - 1].Equals(goTo))
                    {
                        pathToProducts.Add(text[0..i]);
                    }
                    text = text.Remove(removeIndex, i - removeIndex + 1);
                    i = 0;
                }
            }

            pathToProducts.Add(text);
            return pathToProducts;
        }

        private static int FindSecondOccurenceFromEnd(string text, char c)
        {
            bool charExist = false;
            for (int i = text.Length - 1; i > 0; --i)
            {
                if (text[i].Equals(c))
                {
                    if (charExist)
                    {
                        return i;
                    }
                    charExist = true;
                }
            }

            return -1;
        }
    }
}
