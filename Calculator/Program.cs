using System;
using System.Numerics;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Interpreter;
using Parser;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = string.Empty;
            Console.WriteLine("Algebra Expression Evaluator");
            Console.WriteLine("ANTLR4 Parser Demo");
            Console.WriteLine("Supported operators: + | - | * | ^ ");
            Console.WriteLine("Type 'q' or 'quit' to exit");
            Console.WriteLine("=============================");
            while (input.ToLower() != "q" && input.ToLower() != "quit")
            {
                Console.Write("Enter algebraic expression:");
                input = Console.ReadLine();

                Console.Write($"Result = {Calculate(input)}");
                Console.WriteLine();
            }
        }

        private static BigInteger Calculate(string expression)
        {
            var lexer = new AlgebraLexer(new AntlrInputStream(expression));
            var parser = new AlgebraParser(new CommonTokenStream(lexer));

            var ast = parser.root();
            var evaluationVisitor = new ExpressionEvaluationVisitor();
            return evaluationVisitor.Visit(ast);
        }
    }
}
