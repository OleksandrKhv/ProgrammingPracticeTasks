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
    public class EvaluateMathematicalExpression
    {
        private abstract class Node
        {
            public List<Node> Operands;
            public Node Parent;

            /// <summary>
            /// // Holds index this node is in parents Operands[]
            /// -1 if no parent
            /// </summary>
            public int ParentsChild = -1;

            public abstract double CalculateValue();
            public abstract string GetString();

            public void SetOperand(Node node, int position)
            {
                node.Parent = this;
                Operands[position] = node;
                node.ParentsChild = position;
            }

            public void AddOperand(Node node)
            {
                Operands.Add(node);
                SetOperand(node, Operands.Count - 1);
            }

            /// <summary>
            /// Inserts current node below target
            /// </summary>
            /// <param name="target"></param>
            public virtual void InsertBelowAsFirstOperand(Node target)
            {
                if (target.Operands.Count > 0)
                {
                    AddOperand(target.Operands.First());
                }
                target.SetOperand(this, 0);
            }

            /// <summary>
            /// Inserts current node abowe target, makes it our first operand,
            ///  and if target had parent makes us its operand, at position the target was
            /// </summary>
            /// <param name="target"></param>
            public virtual void InsertAbowe(Node target)
            {
                if (target.Parent != null)
                {
                    target.Parent.SetOperand(this, target.ParentsChild);
                }
                AddOperand(target);
            }
        }

        private class ValueNode : Node
        {
            public double Value;
            public override double CalculateValue()
            {
                return Value;
            }

            public override void InsertAbowe(Node target)
            {
                throw new NotSupportedException();
            }

            public override string GetString()
            {
                return Value.ToString();
            }
        }

        private class OperatorNode : Node {
            public string Operator;

            public OperatorNode()
            {
                Operands = new List<Node>();
            }
            public override double CalculateValue()
            {
                double[] values = new double[Operands.Count];
                for(int i = 0;i< Operands.Count; i++)
                {
                    values[i] = Operands[i].CalculateValue();
                }
                return Operators[Operator](values);
            }

            public override string GetString()
            {
                string[] values = new string[Operands.Count];
                for (int i = 0; i < Operands.Count; i++)
                {
                    values[i] = Operands[i].GetString();
                }
                if (Operator.Last() == 'u')
                {
                    return Operator + values[0];
                }
                else
                {
                    return string.Format("{0} {1} {2}", values[0], Operator, values[1]);
                }
            }
        }

        class RootNode : Node
        {
            public RootNode()
            {
                Operands = new List<Node>();
            }
            public override double CalculateValue()
            {
                return Operands[0].CalculateValue();
            }

            public override string GetString()
            {
                if (Operands.Count > 0)
                    return string.Format("({0})", Operands[0].GetString());
                else
                    return "()";// This is not normal, but useful to get some output...
            }
        }

        delegate double Operation(double[] values);

        static Dictionary<string, Operation> Operators = new Dictionary<string, Operation> {
            {"+", delegate (double[] values) { return values[0]+values[1]; } },
            {"-", delegate (double[] values) { return values[0]-values[1];} },
            {"*", delegate (double[] values) { return values[0]*values[1]; } },
            {"/", delegate (double[] values) { return values[0]/values[1]; } },
            {"+u", delegate (double[] values) { return values[0]; } },
            {"-u", delegate (double[] values) { return -values[0]; } },
        };


        public double App(string inp)
        {
            string[] elements = GetElements(inp);
            Node current, currentRoot, newNode;
            Stack<Node> roots = new Stack<Node>();
            char prevOperator = '+';

            current = new RootNode();
            currentRoot = current;
            roots.Push(currentRoot);

            for (int i = 0; i < elements.Length; i++)//"(2 / (2 + 3.33) * 4) - -6"
            {
                if (Operators.ContainsKey(elements[i]))
                {
                    newNode = new OperatorNode { Operator = elements[i] };
                    if (elements[i].Last() == 'u')
                    {
                        current.AddOperand(newNode);
                    }
                    else
                    {
                        if (IsHigher(elements[i].First(), prevOperator))
                        {
                            newNode.InsertAbowe(current);
                        }
                        else
                        {
                            newNode.InsertBelowAsFirstOperand(currentRoot);
                        }
                        prevOperator = elements[i][0];
                    }
                    current = newNode;
                }
                else if (char.IsDigit(elements[i].First()))
                {
                    newNode = new ValueNode { Value = double.Parse(elements[i]) };
                    current.AddOperand(newNode);
                    current = newNode;
                }
                else
                {
                    if (elements[i][0] == '(')
                    {
                        newNode = new RootNode();
                        current.AddOperand(newNode);
                        current = newNode;
                        roots.Push(current);
                        currentRoot = roots.Peek();
                    }
                    else if (elements[i][0] == ')')
                    {
                        prevOperator = '*';
                        roots.Pop();
                        currentRoot = roots.Peek();
                        current = currentRoot;
                    }
                }
            }
            TestContext.Out.Write(currentRoot.GetString());
            return currentRoot.CalculateValue();
        }

        bool IsBracket(char c)
        {
            return c != '(' && c != ')' ? false : true;
        }

        string[] GetElements(string inp)
        {
            List<string> elements = new List<string>();
            char[] separators = new char[] { '+', '-', '*', '/', '(', ')' };
            inp = inp.Replace(".", ",");//double.Parse wont parse 1.0, dots, need to set format?
            inp = inp.Replace(" ", "");
            string[] numbers = RemoveSpaces(inp.Split(separators));
            bool isPrevAOperator = false;

            int i = 0, currentNum = 0;
            while (i < inp.Length)
            {
                if (!char.IsDigit(inp[i]))
                {
                    if (!IsBracket(inp[i]))
                    {
                        elements.Add(inp[i].ToString() + (isPrevAOperator ? "u" : ""));
                        isPrevAOperator = true;
                    }
                    else
                    {
                        elements.Add(inp[i].ToString());
                    }
                    i++;
                }
                else
                {
                    i += numbers[currentNum].Length;
                    elements.Add(numbers[currentNum++]);
                    isPrevAOperator = false;
                }
            }
            return elements.ToArray();
        }

        string[] RemoveSpaces(string[] inp)
        {
            List<string> result = new List<string>();
            foreach (string s in inp)
            {
                if (s.Length > 0) result.Add(s);
            }
            return result.ToArray();
        }

        bool IsHigher(char a, char b)//true for a>b
        {
            if ((a == '*' || a == '/') && (b == '+' || b == '-')) return true;
            else return false;
        }

        private static object[] Data =
        {
            new object[]{"1 + 2 * 5 - 1", 10},
            new object[]{ "2 / 2 + 3 * 4 - 6", 7 },
            new object[]{ "(2 / (2 + 3.33) * 4) - -6", 7.5}
        };

        [Test]
        [TestCaseSource("Data")]
        public void Test(string inp, double expected)
        {
            Assert.AreEqual(expected, App(inp), 0.1);
        }
    }
}
