using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InteractableData
{
    [field: SerializeField] public InteractableType Type { get; private set; }
    [field: SerializeField] public List<EffectModel> Effects { get; private set; }
}
