using UnityEngine;

public class CustomPoolProvider
{
    private DictionaryCustomPool<string, MonoBehaviour> _dictionaryCustomPool = new();

    public TValye Get<TValye>(TValye value) where TValye : MonoBehaviour
    {
        string key = value.ToString();

        var instance = _dictionaryCustomPool.Get(key, value);

        return (TValye)instance;
    }

    public void Hide<TValue>(TValue value) where TValue : MonoBehaviour
    {
        string key = value.ToString();
        _dictionaryCustomPool.Hide(value, key);
    }

    public void Clear() =>
        _dictionaryCustomPool.Clear();

    public void Clear<TValue>(TValue value) where TValue : MonoBehaviour
    {
        string key = value.ToString();

        _dictionaryCustomPool.Clear(key);
    }
}
