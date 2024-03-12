using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Config/LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [SerializeField] private List<LevelData> _levelData;

    public IEnumerable<LevelData> LevelData => _levelData;

    private void OnValidate()
    {
        int id = 0;

        foreach (LevelData data in _levelData)
        {
            data.SetID(id);
            id++;
        }
    }

    public LevelData GetLevelData(int id)
    {
        LevelData data = LevelData.FirstOrDefault(x => x.ID == id);
        if (data == null)
        {
            Debug.LogErrorFormat($"Не найден уровень по ID {id}");
            return null;
        }

        return data;
    }


}
