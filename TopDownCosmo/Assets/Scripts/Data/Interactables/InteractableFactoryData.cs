using System;
using UnityEngine;

[Serializable]
public class InteractableFactoryData
{
    [field: SerializeReference] public Interactable Prefab;
    [field: SerializeReference] public InteractableConfig Config;
}
