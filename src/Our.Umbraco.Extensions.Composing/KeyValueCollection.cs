using System.Collections;
using System.Collections.Generic;

namespace Our.Umbraco.Extensions.Composing
{
    public class KeyValueCollection<TItem> : KeyValueCollection<string, TItem>
    {
        public KeyValueCollection(IDictionary<string, TItem> collection)
            : base(collection)
        {

        }
    }

    public class KeyValueCollection<TKey, TItem> : IKeyValueCollection<TKey, TItem>
    {
        private readonly IDictionary<TKey, TItem> _collection;

        public KeyValueCollection(IDictionary<TKey, TItem> collection)
        {
            _collection = collection;
        }

        public TItem this[TKey key] => _collection[key];

        public bool TryGetValue(TKey key, out TItem value)
        {
            return _collection.TryGetValue(key, out value);
        }

        #region IEnumerable

        public int Count => _collection.Count;

        public IEnumerator<TItem> GetEnumerator() => _collection.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}