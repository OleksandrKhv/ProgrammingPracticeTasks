using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeTasksCsharpVS.Tasks.Codewars.Level5
{
    /*TASK
     * 
     * https://www.codewars.com/kata/molecule-to-atoms
     * 
For a given chemical formula represented by a string, count the number of atoms of each element contained in the molecule
and return an object (associative array in PHP, Dictionary<string, int> in C#, Map in Java).

For example:

var magnesiumHydroxide = 'Mg(OH)2';
parseMolecule(magnesiumHydroxide); // return {Mg: 1, O: 2, H: 2}
 var fremySalt = 'K4[ON(SO3)2]2';
parseMolecule(fremySalt); // return {K: 4, O: 14, N: 2, S: 4} 
As you can see, some formulas have brackets in them. 
The index outside the brackets tells you that you have to multiply count of each atom inside the bracket on this index. 
For example, in Fe(NO3)2 you have one iron atom, two nitrogen atoms and six oxygen atoms.

Note that brackets may be round, square or curly and can also be nested. Index after the braces is optional.
     */

    [TestFixture]
    public class MoleculaToAtom
    {
        //Using only round brackent...
        //Assuming input is always valid, no checking for the other one...
        public Dictionary<string, int> App(string molecula)//TODO rewrite this
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            string currentA = string.Empty;//Use StringBuilder?
            string digit;
            char c;
            Stack<int> multList = new Stack<int>();
            int num = 1, mult = 1;

            for (int i = molecula.Length - 1; i > -1; i--)
            {
                c = molecula[i];
                if (char.IsDigit(c))
                {
                    digit = c.ToString();
                    while (char.IsDigit(molecula[i - 1]))
                    {
                        i--;
                        digit = molecula[i] + digit;
                    }
                    if (molecula[i - 1] == ')')
                    {
                        i--;
                        multList.Push(int.Parse(digit));
                        mult *= multList.Peek();
                    }
                    else num = int.Parse(digit);
                }
                else if (c == ')')
                {
                    multList.Push(1);
                }
                else if (c == '(')
                {//Can add a function to check for different bracket types here, as is stated in the task...
                    mult /= multList.Pop();
                }
                else if (char.IsLetter(c))
                {
                    currentA = c + currentA;
                    if (char.IsUpper(c))
                    {
                        if (result.TryGetValue(currentA, out int tmp))//Can we do a replace, not remove-add?
                        {
                            result.Remove(currentA);
                        }
                        result.Add(currentA, num * mult + tmp);
                        currentA = string.Empty;
                        num = 1;
                    }
                }
            }
            return result;
        }

        public static object[] Data()
        {
            List<object> data = new List<object>();//list element{string input, Distionary expectedResult}
            Dictionary<string, int> tmp;

            //------
            tmp = new Dictionary<string, int>
            {
                { "Mg", 1 },
                { "O", 2 },
                { "H", 2 }
            };
            data.Add(new object[]
            {
                "Mg(OH)2",
                tmp
            });

            //------
            tmp = new Dictionary<string, int>
            {
                { "K", 4 },
                { "O", 14 },
                { "N", 2 },
                { "S", 4 }
            };
            data.Add(new object[]
            {
                "K4(ON(SO3)2)2",
                tmp
            });

            //------
            tmp = new Dictionary<string, int>
            {
                { "Mg", 3 },
                { "O", 40 },
                { "H", 40 }
            };
            data.Add(new object[]
            {
                "(Mg3(((OH)2))20)",
                tmp
            });

            return data.ToArray();
        }

        [Test]
        [TestCaseSource("Data")]
        public void Test(string input, Dictionary<string, int> expected)
        {
            Dictionary<string, int> actual = App(input);

            CollectionAssert.AreEquivalent(expected, actual);
            Assert.That(expected, Is.EquivalentTo(actual));//Is there some preference for what assert to use?
        }
    }
}
