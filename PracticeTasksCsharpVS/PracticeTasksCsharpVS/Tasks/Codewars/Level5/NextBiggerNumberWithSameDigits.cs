using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Codewars.Level5
{
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
