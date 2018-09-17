using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * https://www.codewars.com/kata/evaluate-mathematical-expression
 * Given a mathematical expression as a string you must return the result as a number.

Numbers
Number may be both whole numbers and/or decimal numbers. The same goes for the returned result.

Operators
You need to support the following mathematical operators:

Multiplication *
Division /
Addition +
Subtraction -
Operators are always evaluated from left-to-right, and * and / must be evaluated before + and -.

Parentheses
You need to support multiple levels of nested parentheses, ex. (2 / (2 + 3.33) * 4) - -6

Whitespace
There may or may not be whitespace between numbers and operators.

An addition to this rule is that the minus sign (-) used for negating numbers and parentheses will never be separated by whitespace. I.e., all of the following are valid expressions.

1-1    // 0
1 -1   // 0
1- 1   // 0
1 - 1  // 0
1- -1  // 2
1 - -1 // 2

6 + -(4)   // 2
6 + -( -4) // 10
And the following are invalid expressions

1 - - 1    // Invalid
1- - 1     // Invalid
6 + - (4)  // Invalid
6 + -(- 4) // Invalid
Validation
You do not need to worry about validation - you will only receive valid mathematical expressions following the above rules.

NOTE: Both eval and Function are disabled. Same goes for String.match.
 * 
 */
namespace PracticeTasksCsharpVS.Tasks.Codewars.Level2
{
    public class EvaluateMathematicalExpression//For now this is just a copy paste of calculator
    {
        class Node
        {
            public double Value;
            public char Operation;
            public Node[] Operands = new Node[2];
            public Node Parent;

            public void SetOperand(Node node, int position)
            {
                node.Parent = this;
                Operands[position] = node;
            }
        }

        delegate double Operation(double a, double b);

        Dictionary<char, Operation> Operators = new Dictionary<char, Operation> {
            {'+', delegate (double a, double b) { return a+b; } },
            {'-', delegate (double a, double b) { return a-b; } },
            {'*', delegate (double a, double b) { return a*b; } },
            {'/', delegate (double a, double b) { return a/b; } }
        };

        public double App(string inp)
        {
            string[] elements = inp.Trim().Split(' ');
            Node current = new Node { Value = double.Parse(elements[0]) }, root = current;
            char prev = '+';

            for (int i = 1; i < elements.Length; i++)
            {
                if (IsHigher(elements[i][0], prev))
                {
                    current.SetOperand(new Node { Value = current.Value }, 0);
                    current.SetOperand(new Node { Value = double.Parse(elements[i + 1]) }, 1);
                    current.Operation = elements[i][0];
                    current = current.Operands[1];
                }
                else
                {
                    new Node { Operation = elements[i][0] }.SetOperand(root, 0);
                    root = root.Parent;
                    root.SetOperand(new Node { Value = double.Parse(elements[i + 1]) }, 1);
                    current = root.Operands[1];
                }
                prev = elements[i++][0];
            }

            return Calculate(root);
        }

        double Calculate(Node node)
        {
            if (node.Operands[0] != null)
            {
                return Operators[node.Operation](Calculate(node.Operands[0]), Calculate(node.Operands[1]));
            }
            else
            {
                return node.Value;
            }
        }

        bool IsHigher(char a, char b)//true for a>b
        {
            if ((a == '*' || a == '/') && (b == '+' || b == '-')) return true;
            else return false;
        }

        static object[] Data =
        {
            new object[]{"1 + 2 * 5 - 1", 10},
            new object[]{ "2 / 2 + 3 * 4 - 6", 7 }
        };

        [Test]
        [TestCaseSource("Data")]
        public void Test(string inp, double expected)
        {
            Assert.AreEqual(expected, App(inp), 0.1);
        }
    }
}
