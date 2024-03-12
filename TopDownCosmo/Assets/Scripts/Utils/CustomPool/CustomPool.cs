using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CustomPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _objects;

    public CustomPool(T prefab)
    {
        _prefab = prefab;
        _objects = new List<T>();
    }

    public T Get()
    {
        T obj = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);

        if (obj == null)
            obj = Create();

        obj.gameObject.SetActive(true);
        return obj;
    }

    public void Release(T obj) =>
        obj.gameObject.SetActive(false);

    private T Create()
    {
        T obj = GameObject.Instantiate(_prefab);
        _objects.Add(obj);

        return obj;
    }
}
