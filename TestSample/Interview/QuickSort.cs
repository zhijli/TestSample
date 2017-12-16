using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSample.Interview
{
    [TestClass]
    public class QuickSort
    {
        [TestMethod]
        public void QuickSortTest()
        {
            var ran = new Random(DateTime.Now.Second);
            var data = new List<int>();
            for (int i = 0; i < 500; i++)
            {
                data.Add(ran.Next(0, 10000));
                Console.Write(data[i]+" ");
            }
            Sort(data);
            for (int i = 0; i < 500; i++)
            {
                Console.Write(data[i]+" ");
            }
        }

        public void Sort(List<int> data)
        {
            if (data == null || data.Count < 2)
            {
                return;
            }
            QuickSortInternal(data, 0, data.Count - 1);
        }

        public void QuickSortInternal(List<int> data, int first, int last)
        {
            if (first >= last)
            {
                return;
            }
            var value = data[first];
            int left = first;
            int right = last;
            while (left < right)
            {
                while (data[left] <= value && left < right)
                {
                    left++;
                }
                while (data[right] >= value && left < right)
                {
                    right--;
                }

                Swap(data, left, right);
            }
            if (data[left] <= value)
            {
                Swap(data, first, left);
                QuickSortInternal(data, first, left - 1);
                QuickSortInternal(data, left + 1, last);
            }
            else
            {
                Swap(data, first, left - 1);
                QuickSortInternal(data, first, left - 2);
                QuickSortInternal(data, left, last);
            }


        }

        public void Swap(List<int> data, int i, int j)
        {
            var temp = data[i];
            data[i] = data[j];
            data[j] = temp;
        }
    }
}
