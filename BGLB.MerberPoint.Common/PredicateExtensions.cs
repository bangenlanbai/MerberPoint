using System;
using System.Linq;
using System.Linq.Expressions;

namespace BGLB.MerberPoint.Common
{
    public static class PredicateExtensions
    {

        internal class ParameterReplacer : ExpressionVisitor
        {


            public ParameterReplacer(ParameterExpression paranExpr)
            {

                this.ParameterExpression = paranExpr;

            }
            public ParameterExpression ParameterExpression { get; private set; }
            public Expression Replace(Expression expr)
            {
                return this.Visit(expr);
            }
            protected override Expression VisitParameter(ParameterExpression p)
            {
                return this.ParameterExpression;
            }

        }




        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expression1,
        Expression<Func<T, bool>> expression2)
        {
            var invokedExpression = Expression.Invoke(expression2, expression1.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>(Expression.Or(expression1.Body, invokedExpression),
            expression1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expression1,
              Expression<Func<T, bool>> expression2)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);
            var left = parameterReplacer.Replace(expression1.Body);
            var right = parameterReplacer.Replace(expression2.Body);
            var body = Expression.And(left, right);
            return Expression.Lambda < Func < T,bool >> (body,candidateExpr) ;
        }
    }
}
