using System.Collections.Immutable;

namespace Restorator.Domain.Models
{
    public class PaginatedList<T> : List<T>, IReadOnlyCollection<T>
    {
        public int PageIndex { get; }
        public int TotalItems { get; }
        public int ItemsPerPage { get; }
        public bool HasNextPage => (TotalItems - ItemsPerPage * PageIndex) > 0;
        public bool HasPreviousPage => PageIndex > 1;

        public PaginatedList(int index, int totalItems, int itemsPerPage, IEnumerable<T> items)
        {
            PageIndex = index;
            TotalItems = totalItems;
            ItemsPerPage = itemsPerPage;
            AddRange(items);
        }
    }

    public class ReadOnlyPaginatedList<T>
    {
        public int PageIndex { get; }
        public int TotalItems { get; }
        public int ItemsPerPage { get; }
        public bool CanGetNextPage => (TotalItems - ItemsPerPage * PageIndex) > 0;
        public IReadOnlyCollection<T> Items { get; }

        public ReadOnlyPaginatedList(int index, int totalItems, int itemsPerPage, IEnumerable<T> items)
        {
            Items = items.ToImmutableList();
        }
    }
}