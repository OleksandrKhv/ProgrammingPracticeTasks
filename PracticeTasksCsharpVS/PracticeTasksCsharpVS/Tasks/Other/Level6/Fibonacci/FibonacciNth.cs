using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Other.Level6.Fibonacci
{
    public class FibonacciNth
    {
        public long App(int N)
        {
            return 1;
        }

        public int App1(int N)
        {
            for (int[] a = { 1, 1 }; (N > 2)?true:(N=a[1])<-1; a[N%2] += a[(--N) % 2]);
            return N;
        }

        public int App2(int N)
        {
            return App2_1(1, 1, N - 2);
        }

        private int App2_1(int a, int b, int N)
        {
            return N>0?App2_1(b, a + b, N-1): b;
        }

        public int App3(int N)
        {
            int[] a = { 1, 1 };
            for (; (N > 2); a[N % 2] += a[(--N) % 2]) ;
            return a[1];
        }

        public int App4(int N)
        {
            int a = 1, b = 1;
            N = N-2;
            while (N-- > 0)
            {
                a += b;
                b = b ^ a;
                a = a ^ b;
                b = b ^ a;
            }
            return b;
        }

        public int App5(int N)
        {
            int a = 1, b = 1,t;
            N = N - 2;
            while (N-- > 0)
            {
                t =a+ b;
                a = b;
                b = t;
            }
            return b;
        }

    }

    

    [TestFixture]
    public class Test
    {
        static readonly int[] aa = { 1, 2 };

        //const int[] aa11 = new int[5];

        public static object[] TestData = {

        };

        [Test]
        public void Test1()
        {
            FibonacciNth a = new FibonacciNth();

            int[] r = new int[5], expected = {1,1,2,3,5 };
            for(int i = 1; i < 6; i++)
            {
                r[i-1]=a.App1(i);
            }
            Assert.AreEqual(expected, r);
            int a1 = 5;
        }
    }
}
