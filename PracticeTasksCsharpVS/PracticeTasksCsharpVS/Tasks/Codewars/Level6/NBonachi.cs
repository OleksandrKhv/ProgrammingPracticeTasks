using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * https://www.codewars.com/kata/fibonacci-tribonacci-and-friends
 * 
 * If you have completed the Tribonacci sequence kata, you would know by now that mister Fibonacci has at least a bigger brother. If not, give it a quick look to get how things work.

Well, time to expand the family a little more: think of a Quadribonacci starting with a signature of 4 elements and each following element is the sum of the 4 previous, a Pentabonacci (well Cinquebonacci would probably sound a bit more italian, but it would also sound really awful) with a signature of 5 elements and each following element is the sum of the 5 previous, and so on.

Well, guess what? You have to build a Xbonacci function that takes a signature of X elements - and remember each next element is the sum of the last X elements - and returns the first n elements of the so seeded sequence.

xbonacci {1,1,1,1} 10 = {1,1,1,1,4,7,13,25,49,94}
xbonacci {0,0,0,0,1} 10 = {0,0,0,0,1,1,2,4,8,16}
xbonacci {1,0,0,0,0,0,1} 10 = {1,0,0,0,0,0,1,2,3,6}
xbonacci {1,1} produces the Fibonacci sequence
 * 
 */

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
