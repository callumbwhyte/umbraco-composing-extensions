using System;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Our.Umbraco.Extensions.Composing
{
    public class LazyKeyValueCollectionBuilder<TBuilder, TCollection, TItem> : LazyKeyValueCollectionBuilder<TBuilder, TCollection, string, TItem>
        where TBuilder : LazyKeyValueCollectionBuilder<TBuilder, TCollection, TItem>
        where TCollection : class, IKeyValueCollection<string, TItem>
    {

    }

    public class LazyKeyValueCollectionBuilder<TBuilder, TCollection, TKey, TItem> : KeyValueCollectionBuilder<TBuilder, TCollection, TKey, TItem>
        where TBuilder : LazyKeyValueCollectionBuilder<TBuilder, TCollection, TKey, TItem>
        where TCollection : class, IKeyValueCollection<TKey, TItem>
    {
        public override TCollection CreateCollection(IFactory factory)
        {
            Func<Type, Lazy<TItem>> createLazy = type
                => new Lazy<TItem>(() => CreateItem(factory, type));

            var items = GetItems().ToDictionary(x => x.Key, x => createLazy(x.Value));

            return factory.CreateInstance<TCollection>(items);
        }
    }
}