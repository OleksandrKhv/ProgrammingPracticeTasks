using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Codewars.Level4
{
    /*
     * https://www.codewars.com/kata/next-bigger-number-with-the-same-digits
     * 
     * You have to create a function that takes a positive integer number and returns the next bigger number formed by the same digits:

nextBigger(12)==21
nextBigger(513)==531
nextBigger(2017)==2071
If no bigger number can be composed using those digits, return -1:

nextBigger(9)==-1
nextBigger(111)==-1
nextBigger(531)==-1
     */

    [TestFixture]
    public class NextBiggerNumberWithSameDigits
    {
        public string App(string num)
        {
            List<char> digits;
            for (int i = num.Length - 1; i > -1; i--)
            {
                if (num[i] < num.Last())
                {
                    digits = new List<char>(num.Substring(i+1));
                    digits[digits.Count-1] = num[i];
                    digits.Sort();
                    return num.Substring(0,i) + num.Last() + new string(digits.ToArray());
                }
            }
            return "-1";
        }

        public static object[] Data =
        {
            new object[] { "123", "132"},
            new object[] { "321", "-1"},
            new object[] { "12543", "13245"}
        };

        [Test]
        [TestCaseSource("Data")]
        public void Test(string inp, string expected)
        {
            Assert.AreEqual(expected, App(inp));
        }
    }
}
