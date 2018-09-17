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
    //TODO double brackets are not allowed: 1+((-1)), after parsing this will be 1+((-1, but it should work anyway... 
    public class EvaluateMathematicalExpression//For now this is just a copy paste of calculator
    {
        class Node
        {
            public double Value;
            public string Operation;
            public Node[] Operands = new Node[2];
            public Node Parent;

            public void SetOperand(Node node, int position)
            {
                node.Parent = this;
                Operands[position] = node;
            }
        }

        delegate double Operation(double a, double b);

        Dictionary<string, Operation> Operators = new Dictionary<string, Operation> {
            {"+", delegate (double a, double b) { return a+b; } },
            {"-", delegate (double a, double b) { return a-b; } },
            {"*", delegate (double a, double b) { return a*b; } },
            {"/", delegate (double a, double b) { return a/b; } },
            {"+u", delegate (double a, double b) { return b; } },
            {"-u", delegate (double a, double b) { return -b; } },
        };



        public double App(string inp)
        {
            string[] elements = GetElements(inp);
            Node current, root, newNode;
            Stack<Node> roots = new Stack<Node>();
            char prev = '+';
            bool nextNumRoot = false;

            if (elements[0].Last() == 'u')
            {
                current = new Node { Operation = elements[0] };
            }
            else
            {
                current = new Node { Value = double.Parse(elements[0]) };
            }
            root = current;
            roots.Push(root);

            for (int i = 1; i < elements.Length; i++)//"(2 / (2 + 3.33) * 4) - -6"
            {
                if (Operators.ContainsKey(elements[i]))
                {
                    newNode = new Node { Operation = elements[i] };
                    if (elements[i].Last() == 'u')
                    {
                        current.SetOperand(newNode, 1);
                        current = current.Operands[1];
                    }
                    else
                    {
                        if (IsHigher(elements[i].First(), prev))
                        {
                            if (current.Parent != null)
                            {
                                if (current.Parent.Operands[0] == current)
                                {
                                    current.Parent.SetOperand(newNode, 0);
                                }
                                else
                                {
                                    current.Parent.SetOperand(newNode, 1);
                                }
                            }
                            newNode.SetOperand(current, 0);
                            if (current == root)
                            {
                                root = newNode;
                            }
                        }
                        else
                        {
                            if (root.Parent != null)
                            {
                                if (root.Parent.Operands[0] == root)
                                {
                                    root.Parent.SetOperand(newNode, 0);
                                }
                                else
                                {
                                    root.Parent.SetOperand(newNode, 1);
                                }
                            }
                            newNode.SetOperand(root, 0);
                            root = newNode;
                        }
                        current = newNode;
                        prev = elements[i][0];
                    }
                    if (nextNumRoot)
                    {
                        roots.Push(newNode);
                        root = newNode;
                        nextNumRoot = false;
                    }
                }
                else if (char.IsDigit(elements[i].First())){
                    newNode = new Node { Value = double.Parse(elements[i]) };
                    current.SetOperand(newNode, 1);
                    current = newNode;
                    if (nextNumRoot)
                    {
                        roots.Push(newNode);
                        root = newNode;
                        nextNumRoot = false;
                    }
                }
                else
                {
                    if (elements[i][0] == '(')
                    {
                        nextNumRoot = true;
                    }
                    else if (elements[i][0] == ')')
                    {
                        prev = '*';
                        if (roots.Count > 1)
                        {
                            roots.Pop();
                            root = roots.Peek();
                            current = root;
                        }
                    }
                }
            }
            print(root);
            return Calculate(root);
        }//(2 / (2 + 3.33) * 4) - -6
        //1+(2+1)+-2*1

        bool IsBracket(char c)
        {
            return c!= '(' && c != ')' ? false : true;
        }

        string[] GetElements(string inp)//"(2 / (2 + 3.33) * 4) - -6"
        {
            List<string> elements = new List<string>();
            char[] separators = new char[] {'+','-','*','/','(',')' };
            inp = inp.Replace(".", ",");//double.Parse wont parse 1.0, dots, need to set format?
            inp = inp.Replace(" ", "").TrimStart('(').TrimEnd(')');
            string[] numbers = RemoveSpaces(inp.Split(separators));
            bool isPrevAOperator = false;
            
            int i = 0,currentNum=0;
            while(i < inp.Length)
            {
                if (!char.IsDigit(inp[i]))
                {
                    if (!IsBracket(inp[i]))
                    {
                        elements.Add(inp[i].ToString() + (isPrevAOperator? "u" : ""));
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
            foreach(string s in inp)
            {
                if (s.Length > 0) result.Add(s);
            }
            return result.ToArray();
        }

        void print(Node node)
        {
            TestContext.Out.Write(" v "+node.Value);
            TestContext.Out.WriteLine(" o " + node.Operation);
            if (node.Operands[0] != null) print(node.Operands[0]);
            if (node.Operands[1] != null) print(node.Operands[1]);
        }

        double Calculate(Node node)
        {
            if(node == null)
            {
                return 0;
            }
            if (node.Operands[1] != null)
            {
                
                double a, b;
                a = Calculate(node.Operands[0]);
                b = Calculate(node.Operands[1]);

                TestContext.Out.WriteLine(a + node.Operation + b);
                return Operators[node.Operation](a, b);
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
