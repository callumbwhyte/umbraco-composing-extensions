using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Our.Umbraco.Extensions.Composing
{
    public class LazyKeyValueCollection<TItem> : LazyKeyValueCollection<string, TItem>
    {
        public LazyKeyValueCollection(IDictionary<string, Lazy<TItem>> collection)
            : base(collection)
        {

        }
    }

    public class LazyKeyValueCollection<TKey, TItem> : IKeyValueCollection<TKey, TItem>
    {
        private readonly IDictionary<TKey, Lazy<TItem>> _collection;

        public LazyKeyValueCollection(IDictionary<TKey, Lazy<TItem>> collection)
        {
            _collection = collection;
        }

        public TItem this[TKey key] => _collection[key].Value;

        public bool TryGetValue(TKey key, out TItem value)
        {
            if (_collection.TryGetValue(key, out Lazy<TItem> lazyValue) == true)
            {
                value = lazyValue.Value;
            }
            else
            {
                value = default(TItem);
            }

            return value != null;
        }

        #region IEnumerable

        public int Count => _collection.Count;

        public IEnumerator<TItem> GetEnumerator() => _collection.Values
            .Select(x => x.Value)
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}