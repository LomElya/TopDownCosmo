using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData")]
public class LevelData : ScriptableObject, IIdentifier
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public float LevelLength { get; private set; }
    [field: SerializeField] public float StartSpeed { get; private set; }
    [field: SerializeField] public float EndSpeed { get; private set; }
    [field: SerializeField] public int GoldForPass { get; private set; }
    [field: SerializeField] public List<InteractableSpawnData> InteractableSpawn { get; private set; }

    public void SetID(int id) =>
        ID = id;
}
