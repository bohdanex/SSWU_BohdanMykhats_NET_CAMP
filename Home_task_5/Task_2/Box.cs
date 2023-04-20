using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public class Box : Item
    {
        private List<Item> _items;

        public List<Item>? Items 
        {
            get => _items;
            set
            {
                if(value is not null)
                {
                    _items = value;
                    SummariseDimensions();
                }
            }
        }

        public Box(string departmentName, List<Item>? items)
        {
            Name = departmentName;
            _items = new();
            Items = items;
        }

        public void AddItem(Item item)
        {
            Items?.Add(item);
            SummariseDimensions();
        }

        public void RemoveItem(Item item)
        {
            Items?.Remove(item);
            SummariseDimensions();
        }

        private void SummariseDimensions()
        {
            double height = 0;
            double width = 0;
            double length = 0;
            Type itemType = Items[0].GetType();

            foreach (Item item in Items)
            {
                if(item.GetType() != itemType)
                {
                    throw new ArgumentException("A 'Box' can not be a 'Product' and vise-versa");
                }

                if(item.Width > width)
                {
                    width = item.Width;
                }
                if (item.Length > length)
                {
                    length = item.Length;
                }
                height += item.Height;
            }

            Height = height;
            Width = width;
            Length= length;
        }

        public string ToString(bool printPoductTree, byte offset = 0)
        {
            
            if (printPoductTree)
            {
                StringBuilder outputString = new();

                byte i = offset;
                while(i > 0)
                {
                    outputString.Append(' ');
                    --i;
                }
                outputString.AppendLine(ToString());

                foreach (Item item in Items)
                {
                    
                    if (item is Box)
                    {
                        outputString.AppendLine(((Box)item).ToString(true, (byte)(offset + 1)));
                    }
                    else
                    {
                        outputString.AppendLine(item.ToString("5", null));
                    }
                }

                return outputString.ToString();
            }

            return ToString();
        }
    }
}
