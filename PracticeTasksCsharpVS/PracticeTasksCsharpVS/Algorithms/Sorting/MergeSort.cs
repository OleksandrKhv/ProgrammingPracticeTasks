using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Algorithms.Sorting
{
    [TestFixture]
    class MergeSort<T>
    {

        public static int[] Sort(int[] input)
        {
            int size = 1;
            int maxSize = input.Length / 2;
            int pos1, pos2, pos2Max;
            int[] res = new int[input.Length];
            while (size < input.Length)
            {
                for (int pos = 0; pos < input.Length - size; pos += size * 2)
                {
                    pos1 = 0;
                    pos2 = size;
                    pos2Max = Math.Min(input.Length - pos, size * 2);
                    while (true)
                    {
                        if (input[pos + pos1] > input[pos + pos2])
                        {
                            res[pos + pos1 + pos2 - size] = input[pos + pos1];
                            pos1++;
                            if (pos1 >= size)
                            {
                                while (pos2 < pos2Max)
                                {
                                    res[pos + pos1 + pos2 - size] = input[pos + pos2];
                                    pos2++;
                                }
                                break;
                            }
                        }
                        else
                        {
                            res[pos + pos1 + pos2 - size] = input[pos + pos2];
                            pos2++;
                            if (pos2 >= pos2Max)
                            {
                                while (pos1 < size)
                                {
                                    res[pos + pos1 + pos2 - size] = input[pos + pos1];
                                    pos1++;
                                }
                                break;
                            }
                        }
                    }
                }
                size *= 2;
                for (int i = 0; i < maxSize * 2; input[i] = res[i++]) ;
            }
            return res;
        }

        static object[] TestData =
        {
            new object[]{ new int[] { 5,1,6,4 }, new int[] { 6,5,4,1} },
            new object[]{ new int[] { 7,5,1,6,4 }, new int[] {7, 6,5,4,1} }
        };

        [Test]
        [TestCaseSource("TestData")]
        public void Test(int[] inp, int[] expected)
        {
            int[] result = Sort(inp);
            Assert.AreEqual(expected, result);
        }
    }
}
