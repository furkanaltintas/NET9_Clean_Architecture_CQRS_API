using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Paging;

public static class IQueryablePaginateExtensions
{
    public static async Task<Paginate<T>> ToPaginateAsync<T>(
        this IQueryable<T> source,
        int index,
        int size,
        CancellationToken cancellationToken = default)
    {
        int count = await source.CountAsync(cancellationToken).ConfigureAwait(false);

        List<T> items = await source.Skip(index * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

        return Paginate(index, count, items, size);
    }


    public static Paginate<T> ToPaginate<T>(
        this IQueryable<T> source,
        int index,
        int size)
    {
        int count = source.Count();
        List<T> items = source.Skip(index * size).Take(size).ToList();

        return Paginate(index, count, items, size);
    }



    private static Paginate<T> Paginate<T>(
        int index,
        int count,
        List<T> items,
        int size)
    {
        Paginate<T> paginate = new()
        {
            Index = index,
            Count = count,
            Items = items,
            Size = size,
            Pages = (int)Math.Ceiling(count / (double)size)
        };

        return paginate;
    }
}