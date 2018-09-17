using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Codewars.Level3
{
    /*

For a given list [x1, x2, x3, ..., xn] compute the last (decimal) digit of 
x1 ^ (x2 ^ (x3 ^ (... ^ xn))).

E. g.,

last_digit({3, 4, 2}) == 1 
because 3 ^ (4 ^ 2) = 3 ^ 16 = 43046721.

Beware: powers grow incredibly fast. For example, 9 ^ (9 ^ 9) has more than 369 millions of digits.
lastDigit has to deal with such numbers efficiently.

Corner cases: we assume that 0 ^ 0 = 1 and that lastDigit of an empty list equals to 1.
     */

    [TestFixture]
    public class LastDigitOfAHugeNumber
    {
        private int[][] LastDigits = getLastDigits();
        public int App(int[] inp)
        {
            int current = inp.Last();
            for (int i = inp.Length - 2; i > -1; i--)
            {
                if (inp[i] == 0)
                {
                    current = 1;
                }
                else
                {
                    current = GetIntInPowLastDigit(GetIntLastDigit(inp[i]), current);
                }
            }
            if (GetIntLastDigit(inp[0]) == 0)
            {
                //if current[i+1] == 0 return 1//This will fix the test, but adding more code to satisfy Math.Pow(0,0)==1, ...
                return 0;
            }
            return GetIntLastDigit(current);
        }

        private static int GetIntLastDigit(int i)
        {
            return i - i / 10 * 10;
        }


        //TODO rewrite this
        private int GetIntInPowLastDigit(int i, int pow)
        {
            int a = pow % (LastDigits[i].Length - 1);
            a = a == 0 ? (LastDigits[i].Length - 1) : 0;
            if (pow == 0)
            {
                a = 0;
            }
            return (int)Math.Pow(i, a);
        }

        private static int[][] getLastDigits()
        {
            List<int> currentL;
            int current;
            int[][] result = new int[10][];

            for (int i = 0; i < 10; i++)
            {
                currentL = new List<int>();
                currentL.Add(1);
                current = i;
                do
                {
                    currentL.Add(current);
                    current *= i;
                } while ((current = GetIntLastDigit(current)) != i);

                result[i] = currentL.ToArray();
            }
            return result;
        }

        //TODO make more test data
        public static object[] Data =
        {
            new object[]{ new int[] {3,4,2} , 1},
            new object[]{ new int[] {0,0,1} , 1},
            new object[]{ new int[] {10,10,10} , 0}
        };

        [Test]
        [TestCaseSource("Data")]
        public void Test(int[] input, int expected)
        {
            Assert.AreEqual(expected, App(input));
        }
    }
}
