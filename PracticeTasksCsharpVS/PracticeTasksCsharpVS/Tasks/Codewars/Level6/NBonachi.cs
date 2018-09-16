using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Codewars.Level6
{
    [TestFixture]
    class NBonachi
    {
        public int App(int[] values, int index)
        {
            int L = values.Length, position = L;
            int[] val = new int[L + 1];
            int a = val[0];
            val[L] = 0;
            for (int i = 0; i < L; val[L] += val[i] = values[i++]) ;
            L++;
            while (++position < index)
            {
                val[position % L] = val[(position - 1) % L] * 2 - val[position % L];
            }
            return val[--position % L];
        }

        public static object[] Data =//int{ first numbers }, NumPositionToGet, expected
        {
            new object[] { new int[] {0,1}, 5, 3 },
            new object[] { new int[] {0,1 ,1}, 5, 4 }
        };

        [Test]
        [TestCaseSource("Data")]

        public void Test(int[] firstNumsInp, int numInp, int expected)
        {
            Assert.AreEqual(expected, App(firstNumsInp, numInp));
        }
    }
}
