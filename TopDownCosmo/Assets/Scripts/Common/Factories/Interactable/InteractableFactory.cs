using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableFactory", menuName = "Factory/InteractableFactory")]
public class InteractableFactory : GameObjectFactory
{
    [SerializeField] private List<InteractableFactoryData> _datas;

    public Interactable Get(InteractableType type)
    {
        InteractableFactoryData data = GetConfig(type);

        Interactable interactable = CreateGameObjectInstance(data.Prefab);

        if (interactable == null)
        {
            Debug.LogErrorFormat($"Префаб типа ({type}) не найден");
            return null;
        }

        interactable.SetEffects(data.Config.Data.Effects);
        interactable.Init();

        return interactable;
    }

    public InteractableFactoryData GetConfig(InteractableType type)
    {
        InteractableFactoryData data = _datas.FirstOrDefault(x => x.Config.Data.Type == type);

        if (data == null)
        {
            Debug.LogErrorFormat($"Интерактивный тип ({type}) не найден");
            return null;
        }

        return data;
    }
}




