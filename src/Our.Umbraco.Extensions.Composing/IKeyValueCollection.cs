using Umbraco.Core.Composing;

namespace Our.Umbraco.Extensions.Composing
{
    public interface IKeyValueCollection<TKey, TItem> : IBuilderCollection<TItem>
    {
        TItem this[TKey key] { get; }

        bool TryGetValue(TKey key, out TItem value);
    }
}