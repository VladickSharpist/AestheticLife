using Aesthetic.CQRS.Abstractions;

namespace Aesthetic.CQRS.Queries;

public class Query: IQuery
{
    public Query(long id)
    {
        Id = id;
    }
    public long Id { get; set; }
}