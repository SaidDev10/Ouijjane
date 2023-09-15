using System.Linq.Expressions;

namespace Ouijjane.Shared.Domain.Specifications;
public interface ISpecification<T>
{
    List<Expression<Func<T, bool>>> Criteria { get; }

    List<Expression<Func<T, object>>> Includes { get; }

    List<string> IncludeStrings { get; }

    Expression<Func<T, object>>? OrderBy { get; }

    Expression<Func<T, object>>? OrderByDescending { get; }

    Func<IQueryable<T>, IOrderedQueryable<T>>? OrderedExpression { get; }

    Expression<Func<T, object>>? GroupBy { get; }

    int Take { get; }

    int Skip { get; }

    bool IsPagingEnabled { get; }

    bool IsDistinct { get; }

    bool IsReadOnly { get; }

    bool SplitQuery { get; }
}
