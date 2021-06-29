using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Our.Umbraco.Extensions.Composing
{
    public abstract class KeyValueCollectionBuilder<TBuilder, TCollection, TItem> : KeyValueCollectionBuilder<TBuilder, TCollection, string, TItem>
        where TBuilder : KeyValueCollectionBuilder<TBuilder, TCollection, TItem>
        where TCollection : class, IKeyValueCollection<string, TItem>
    {

    }

    public abstract class KeyValueCollectionBuilder<TBuilder, TCollection, TKey, TItem> : CollectionBuilderBase<TBuilder, TCollection, TItem>
        where TBuilder : KeyValueCollectionBuilder<TBuilder, TCollection, TKey, TItem>
        where TCollection : class, IKeyValueCollection<TKey, TItem>
    {
        private readonly IDictionary<TKey, Type> _items;

        public KeyValueCollectionBuilder()
        {
            _items = new Dictionary<TKey, Type>();
        }

        public IDictionary<TKey, Type> GetItems() => _items;

        public void Add(TKey key, Type type)
        {
            _items[key] = type;

            Configure(types => types.Add(type));
        }

        public void Add<T>(TKey key)
            where T : TItem
        {
            Add(key, typeof(T));
        }

        public void Remove(Type type)
        {
            _items.RemoveAll(x => x.Value == type);

            Configure(types => types.Remove(type));
        }

        public void Remove<T>()
            where T : TItem
        {
            Remove(typeof(T));
        }

        public void Remove(TKey key)
        {
            var type = _items[key];

            _items.Remove(key);

            Configure(types => types.Remove(type));
        }

        public void Clear()
        {
            _items.Clear();
        }

        protected override TItem CreateItem(IFactory factory, Type itemType)
        {
            return base.CreateItem(factory, itemType);
        }

        public override TCollection CreateCollection(IFactory factory)
        {
            var items = _items.ToDictionary(x => x.Key, x => CreateItem(factory, x.Value));

            return factory.CreateInstance<TCollection>(items);
        }
    }
}