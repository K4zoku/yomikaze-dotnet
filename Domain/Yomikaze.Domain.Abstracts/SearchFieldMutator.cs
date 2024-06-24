namespace Yomikaze.Domain.Abstracts;

public delegate IQueryable<TItem> QueryMutator<TItem, in TSearch>(IQueryable<TItem> items, TSearch search);

public class SearchFieldMutator<TItem, TSearch>(Predicate<TSearch> condition, QueryMutator<TItem, TSearch> mutator)
{
    private Predicate<TSearch> Condition { get; set; } = condition;
    private QueryMutator<TItem, TSearch> Mutator { get; set; } = mutator;

    public IQueryable<TItem> Apply(TSearch search, IQueryable<TItem> query)
    {
        return Condition(search) ? Mutator(query, search) : query;
    }
}