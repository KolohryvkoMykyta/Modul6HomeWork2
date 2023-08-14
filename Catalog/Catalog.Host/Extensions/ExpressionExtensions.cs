using System.Linq.Expressions;

namespace Catalog.Host.Extensions
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> leftExpression, Expression<Func<T, bool>> rightExpression)
        {
            var leftParameter = leftExpression.Parameters[0];
            var rightParameter = rightExpression.Parameters[0];

            var visitor = new ReplaceParameterVisitor(rightParameter, leftParameter);

            var leftBody = leftExpression.Body;
            var rightBody = visitor.Visit(rightExpression.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.And(leftBody, rightBody), leftParameter);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return ReferenceEquals(node, _oldParameter) ? _newParameter : base.VisitParameter(node);
            }
        }
    }
}
