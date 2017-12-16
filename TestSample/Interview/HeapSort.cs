using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSample.Interview
{
    [TestClass]
    public class HeapSort
    {
        [TestMethod]
        public void HeapSortTest()
        {
            var ran = new Random(DateTime.Now.Second);
            var data = new List<int>();
            for (int i = 0; i < 500; i++)
            {
                data.Add(ran.Next(0, 10000));
                Console.WriteLine(data[i]);
            }
            Sort(data);
            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine(data[i]);
            }
        }

        //O(n*lgn)
        public void Sort(List<int> data)
        {
            BuildHeap(data, data.Count - 1);
            for (int i = data.Count - 1; i >= 1; i--)
            {
                Swap(data, 0, i);
                MaxHeapify(data, i - 1, 0);
            }
        }

        //O(n)
        public void BuildHeap(List<int> data, int last)
        {
            for (int i = Parent(last); i >= 0; i--)
            {
                MaxHeapify(data, last, i);
            }
        }

        //O(lgn)
        public void MaxHeapify(List<int> data, int last, int index)
        {
            int current = index;

            while (true){
                int left = Left(current);
                int right = Right(current);
                int largest = current;
                if (left <= last && data[left] > data[current])
                {
                    largest = left;
                }
                if (right <= last && data[right] > data[largest])
                {
                    largest = right;
                }
                if (largest != current)
                {
                    Swap(data, current, largest);
                    current = largest;
                }
                else
                {
                    break;
                }
            }
        }

        public int Parent(int index)
        {
            return (index - 1) / 2;
        }

        public int Left(int index)
        {
            return index * 2 + 1;
        }

        public int Right(int index)
        {
            return index * 2 + 2;
        }

        public void Swap(List<int> data, int i, int j)
        {
            var temp = data[i];
            data[i] = data[j];
            data[j] = temp;
        }

    }
}
