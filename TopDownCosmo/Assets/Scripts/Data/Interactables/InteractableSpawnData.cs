using UnityEngine;

[System.Serializable]
public class InteractableSpawnData
{
    [field: SerializeField] public InteractableType Type { get; private set; }
    [field: SerializeField] public float StartCooldown { get; private set; }
    [field: SerializeField] public float EndCooldown { get; private set; }
    [field: SerializeField] public float PrewarmTime { get; private set; }

}
