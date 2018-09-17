using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Codewars.Level4
{

    [TestFixture]
    public class AddingBigNumbers
    {
        public string App(string a, string b)
        {
            StringBuilder result = new StringBuilder();
            string tmp;
            int dif = a.Length - b.Length, sum = 0;

            if (dif < 0)
            {
                tmp = a;
                a = b;
                b = a;
                dif *= -1;
            }

            tmp = a.Substring(0, dif);
            a = a.Substring(dif);

            for (int i = a.Length - 1; i > -1; i--)
            {
                sum = GetInt(a[i]) + GetInt(b[i]) + sum;
                result.Insert(0, sum.ToString().Last());
                if (sum > 9)
                {
                    sum = 1;
                }
                else sum = 0;
            }

            if (sum > 0)
            {
                if (tmp.Length > 0)
                {
                    result.Insert(0, sum + GetInt(tmp.Last()));
                    tmp = tmp.Remove(tmp.Length - 1);
                }
                else result.Insert(0, sum);
            }
            return tmp + result.ToString();
        }

        private int GetInt(char c)
        {
            return c - '0';
        }

        public static object[] Data = // Input(a,b), expected
        {
            new object[]{ new string []{"123","321" },"444" },
            new object[]{ new string []{"11","99" },"110" },
            new object[]{ new string []{"99","11" },"110" },
            new object[]{ new string []{"999","99" },"1098" }
        };

        [Test]
        [TestCaseSource("Data")]
        public void Test(string[] input, string expected)
        {
            string actual = App(input[0], input[1]);

            Assert.AreEqual(expected, actual);
        }
    }
}
