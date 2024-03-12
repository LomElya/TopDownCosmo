using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AbilityData
{
    [field: SerializeField] public int ID;
    [field: SerializeField] public string Name;
    [field: SerializeField] public string Description;
    [field: SerializeField] public List<EffectModel> Effects;
}
