using System;
using System.Globalization;
using System.Numerics;
using Parser;

namespace Interpreter
{
    //walks over the AST and checks for issues
    public class ExpressionEvaluationVisitor : AlgebraBaseVisitor<BigInteger>
    {
        public override BigInteger VisitPowerExpression(AlgebraParser.PowerExpressionContext context) => 
            BigInteger.Pow(base.Visit(context.num), (int)base.Visit(context.pow));

        public override BigInteger VisitNumberExpression(AlgebraParser.NumberExpressionContext context) => 
            BigInteger.Parse(context.GetText(), NumberStyles.Any);

        public override BigInteger VisitMultOrDivideExpression(AlgebraParser.MultOrDivideExpressionContext context) =>
            context.op.Text switch
            {
                "*" => (base.Visit(context.left) * base.Visit(context.right)),
                "/" => (base.Visit(context.left) / base.Visit(context.right)),
                _ => throw new ArgumentException($"Unsupported operator '{context.op.Text}'")
            };

        public override BigInteger VisitPlusOrMinusExpression(AlgebraParser.PlusOrMinusExpressionContext context) =>
            context.op.Text switch
            {
                "+" => (base.Visit(context.left) + base.Visit(context.right)),
                "-" => (base.Visit(context.left) - base.Visit(context.right)),
                _ => throw new ArgumentException($"Unsupported operator '{context.op.Text}'")
            };

        public override BigInteger VisitNegativeNumberExpression(AlgebraParser.NegativeNumberExpressionContext context) => 
            -1 * base.Visit(context.num);


        protected override BigInteger AggregateResult(BigInteger aggregate, BigInteger nextResult) => 
            aggregate + nextResult;
    }
}
