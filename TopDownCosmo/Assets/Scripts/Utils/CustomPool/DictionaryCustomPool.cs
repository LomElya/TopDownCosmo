using System.Collections.Generic;
using UnityEngine;

public class DictionaryCustomPool<TKey, TValue> where TValue : MonoBehaviour
{
    private static TKey _key;

    private Dictionary<TKey, CustomPool<TValue>> _pools =
     new Dictionary<TKey, CustomPool<TValue>>();

    public TValue Get(TKey key, TValue value)
    {
        _key = key;

        var pool = GetPool(value);
        TValue newValue = pool.Get();

        return newValue;
    }

    public void Hide(TValue value, TKey key)
    {
        _key = key;

        var pool = GetPool(value);
        pool.Release(value);
    }

    public void Clear() => _pools.Clear();

    public void Clear(TKey key)
    {
        if (_pools.ContainsKey(key))
        {
            _pools.Remove(key);
        }
    }

    private CustomPool<TValue> GetPool(TValue value)
    {
        CustomPool<TValue> pool;

        // Создать новый пул, если такого нет
        if (!_pools.ContainsKey(_key))
        {
            pool = new CustomPool<TValue>(value);
            _pools.Add(_key, pool);
        }
        // Если пул есть, возвращаем его
        else
            pool = _pools[_key];

        return pool;
    }
}
