using amiliur.web.shared.Filtering;
using System.Linq.Expressions;

namespace amiliur.figforms.shared;

public class FilterExpressionBuilder
{
    public static Expression<Func<TEntity, bool>> BuildPredicate<TEntity>(FilterExpr filterExpr)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");

        var body = BuildExpressionBody(filterExpr, parameter);

        return Expression.Lambda<Func<TEntity, bool>>(body, parameter);
    }

    private static Expression BuildExpressionBody(FilterExpr filterExpr, ParameterExpression parameter)
    {
        switch (filterExpr)
        {
            case BinaryExpr binary:
                var left = BuildExpressionBody(binary.Left, parameter);
                var right = BuildExpressionBody(binary.Right, parameter);

                return binary.FilterType switch
                {
                    WhereFilterType.And => Expression.AndAlso(left, right),
                    WhereFilterType.Or => Expression.OrElse(left, right),
                    _ => throw new NotSupportedException($"Binary operation '{binary.FilterType}' is not supported"),
                };

            case UnaryExpr unary:
                var operand = BuildExpressionBody(unary.Operand, parameter);

                return unary.FilterType switch
                {
                    WhereFilterType.IsNotNull => Expression.NotEqual(operand, Expression.Constant(null)),
                    // Add other unary operations here...
                    _ => throw new NotSupportedException($"Unary operation '{unary.FilterType}' is not supported"),
                };

            case FieldValueExpr fieldValue:
                var member = Expression.Property(parameter, fieldValue.Field);
                var constant = Expression.Constant(fieldValue.Value);

                return fieldValue.FilterType switch
                {
                    WhereFilterType.Equal => Expression.Equal(member, constant),
                    WhereFilterType.NotEqual => Expression.NotEqual(member, constant),
                    WhereFilterType.GreaterThan => Expression.GreaterThan(member, constant),
                    WhereFilterType.LessThan => Expression.LessThan(member, constant),
                    WhereFilterType.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
                    WhereFilterType.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
                    // Add other comparison operations here...
                    _ => throw new NotSupportedException($"Comparison operation '{fieldValue.FilterType}' is not supported"),
                };

            default:
                throw new NotSupportedException($"Filter expression type '{filterExpr.GetType().Name}' is not supported");
        }
    }
}