namespace Yomikaze.Domain.Abstracts;

public delegate IQueryable<TItem> QueryMutator<TItem, in TSearch>(IQueryable<TItem> items, TSearch search);

public class SearchFieldMutator<TItem, TSearch>(Predicate<TSearch> condition, QueryMutator<TItem, TSearch> mutator)
{
    private Predicate<TSearch> Condition { get; } = condition;
    private QueryMutator<TItem, TSearch> Mutator { get; } = mutator;

    public string? Name { get; set; }

    public IQueryable<TItem> Apply(TSearch search, IQueryable<TItem> query)
    {
        if (Name is not null)
        {
            Console.WriteLine($"Applying {Name}...");
        }

        return Condition(search) ? Mutator(query, search) : query;
    }
}