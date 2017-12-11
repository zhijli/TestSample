// -----------------------------------------------------------------------
//  <copyright company="Microsoft">
//      Copyright (c) Microsoft Corporation.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace TestSample.Interview
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using global::Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HeapSortTest
    {
        [TestMethod]
        public void HeapSortBvt()
        {
            var random = new Random(DateTime.Now.Millisecond);
            var size = 100;
            List<int> number = new List<int>();
            for (int i = 0; i < size; i++)
            {
                number.Add(random.Next(0, 10000));
            }

            Console.WriteLine("Before Sort:");
            foreach (int num in number)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
            HeapSort(number);
            Console.WriteLine("After Sort:");
            number.ForEach(i => Console.Write(i + " "));
        }

        public static void HeapSort(List<int> numbers)
        {
            HeapInit(numbers);

            for (int i = numbers.Count - 1; i >= 1; i--)
            {
                Swap(numbers, 0, i);

                BubbleDown(numbers, i - 1, 0);
            }
        }

        private static void BubbleDown(List<int> numbers, int lastIndex, int index)
        {
            var i = Bubble(numbers, lastIndex, index);
            if (i != index)
            {
                BubbleDown(numbers, lastIndex, i);
            }
        }


        private static int Bubble(List<int> numbers, int lastIndex, int index)
        {
            int lChild = index * 2 + 1;
            int rChild = index * 2 + 2;

            if (lChild > lastIndex)
            {
                return index;
            }
            else
            {
                if (rChild <= lastIndex)
                {
                    if (numbers[lChild] > numbers[rChild])
                    {
                        if (numbers[lChild] > numbers[index])
                        {
                            Swap(numbers, lChild, index);
                            return lChild;
                        }
                        return index;
                    }
                    else
                    {
                        if (numbers[rChild] > numbers[index])
                        {
                            Swap(numbers, rChild, index);
                            return rChild;
                        }
                        return index;

                    }
                }
                else
                {
                    if (numbers[lChild] > numbers[index])
                    {
                        Swap(numbers, lChild, index);
                        return lChild;
                    }
                    return index;
                }
            }
        }

        private static void Swap(List<int> numbers, int v1, int v2)
        {
            if (numbers.Count <= v1 || numbers.Count <= v2)
            {
                throw new Exception("Index not exists");
            }

            var temp = numbers[v1];
            numbers[v1] = numbers[v2];
            numbers[v2] = temp;
        }

        private static void HeapInit(List<int> numbers)
        {
            for (int i = numbers.Count - 1; i >= 0; i--)
            {
                BubbleDown(numbers, numbers.Count - 1, i);
            }
        }


    }
}
