using System.Collections.Generic;
using UnityEngine;

public abstract class Content<T> : ScriptableObject where T : IContents
{
    [SerializeField] protected List<T> _contents;

    public IEnumerable<T> Contents => _contents;
}
