using UnityEngine;

[CreateAssetMenu(fileName = "InteractableConfig", menuName = "Config/InteractableConfig")]
public class InteractableConfig : ScriptableObject
{
    [field: SerializeField] public InteractableData Data { get; private set; }
}
