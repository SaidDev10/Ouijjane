using Bogus;
using System.Linq.Expressions;

namespace Ouijjane.Village.Application.Tests.Builders;

public class BaseEntityBuilder<T> where T : class
{
    private readonly Faker _faker = new Faker();

    protected Faker<T> EntityFaker { get; }

    public BaseEntityBuilder()
    {
        EntityFaker = new Faker<T>();
    }

    public BaseEntityBuilder<T> SetDefaultRules(Action<Faker, T> rules)
    {
        EntityFaker.Rules(rules);
        return this;
    }

    public BaseEntityBuilder<T> CustomInstantiator(Func<Faker, T> creator)
    {
        EntityFaker.CustomInstantiator(creator);

        return this;
    }

    public BaseEntityBuilder<T> With<TProp>(Expression<Func<T, TProp>> expression, Func<TProp> value)
    {
        EntityFaker.RuleFor(expression, value);
        return this;
    }

    public BaseEntityBuilder<T> With<TProp>(Expression<Func<T, TProp>> expression, TProp value)
    {
        EntityFaker.RuleFor(expression, value);
        return this;
    }

    public BaseEntityBuilder<T> With<TProp>(Expression<Func<T, TProp>> expression, Func<Faker, TProp> faker)
    {
        EntityFaker.RuleFor(expression, faker(_faker));
        return this;
    }

    public virtual T Build()
    {
        return EntityFaker.Generate();
    }

    public virtual IList<T> BuildList(int count)
    {
        return EntityFaker.Generate(count);
    }
}