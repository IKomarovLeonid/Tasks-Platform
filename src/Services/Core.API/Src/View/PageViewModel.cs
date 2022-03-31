using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.API.View
{
    public class PageViewModel<TModel>
    {
        public ICollection<TModel> Items { get; private init; } = new Collection<TModel>();

        public static PageViewModel<TModel> New(ICollection<TModel> items) => new PageViewModel<TModel>() { Items = items };
    }
}
