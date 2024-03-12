using UnityEditor;
using UnityEngine;
using System.IO;

public class HelperMenu
{
    private static string UserStatePath => $"{Application.persistentDataPath}/userState.def";

    [MenuItem("Tools/Progress/Clear Account Data(удалить файл сохранения)")]
    public static void ClearAccountData()
    {
        var path = UserStatePath;

        if (File.Exists(path))
            File.Delete(path);
    }

    [MenuItem("Tools/Progress/Show user state file(показать файл сохранения)")]
    public static void ShowUserStateFile()
    {
        EditorUtility.RevealInFinder(UserStatePath);
    }

    [MenuItem("Tools/Progress/Add Currencies(добавить валюты)")]
    public static void AddCurrencies()
    {
        var path = UserStatePath;

        if (File.Exists(path))
        {
            var bytes = File.ReadAllBytes(path);
            var state = new UserState();
            var offset = 0;

            state.Deserialize(bytes, ref offset);
            state.Currencies.AddGold(100);

            offset = 0;

            state.Serialize(bytes, ref offset);
            File.WriteAllBytes(path, bytes);
        }
    }

    [MenuItem("Tools/Assets/Clear Asset Bundle Cache(очистить кэш)")]
    public static void DoClearAssetBundleCache()
    {
        AssetBundle.UnloadAllAssetBundles(true);
        Debug.Log($"Clear Asset Bundle Cache result: {Caching.ClearCache()}");
    }
}