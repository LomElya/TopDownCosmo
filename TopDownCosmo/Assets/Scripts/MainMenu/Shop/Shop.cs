using UnityEngine;

public abstract class Shop : ScriptableObject, IContents, IIdentifier
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField, Range(0, 5000)] public int Price { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    public void SetID(int id) =>
        ID = id;
}
