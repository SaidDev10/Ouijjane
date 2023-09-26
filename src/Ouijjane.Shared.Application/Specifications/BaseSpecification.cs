using System.Linq.Expressions;

namespace Ouijjane.Shared.Application.Specifications;
public abstract class BaseSpecification<T> : ISpecification<T>
{
    public List<Expression<Func<T, bool>>> Criteria { get; } = new();

    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public List<string> IncludeStrings { get; } = new();

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderedExpression { get; private set; }

    public Expression<Func<T, object>>? GroupBy { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPagingEnabled { get; private set; }

    public bool IsDistinct { get; private set; }

    public bool IsReadOnly { get; private set; }

    //public bool SplitQuery { get; private set; }

    protected BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria.Add(criteria);
    }

    protected BaseSpecification()
    {

    }

    protected void AddCriteria(Expression<Func<T, bool>> additionalCriteria)
    {
        Criteria.Add(additionalCriteria);
    }

    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    protected void ApplyPaging(int pageNumber, int pageSize)
    {
        IsPagingEnabled = true;
        Skip = (pageNumber - 1) * pageSize;
        Take = pageSize;
    }

    protected void ApplyDistinct()
    {
        IsDistinct = true;
    }

    protected void ApplyReadOnly()
    {
        IsReadOnly = true;
    }

    protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    public void ApplyOrderExpression(Func<IQueryable<T>, IOrderedQueryable<T>> query)
    {
        OrderedExpression = query;
    }

    protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }

    //protected void AsSplitQuery(bool splitQuery)
    //{
    //    SplitQuery = splitQuery;
    //}
}