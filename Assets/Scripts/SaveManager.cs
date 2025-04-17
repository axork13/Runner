using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static string fileName = "playerData.json";

    private static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public static void Save(PlayerData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(), json);
        Debug.Log("Données sauvegardées : " + GetPath());
    }

    public static PlayerData Load()
    {
        string path = GetPath();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Données chargées !");
            return data;
        }
        else
        {
            Debug.Log("Aucune sauvegarde trouvée. Création de données par défaut.");
            return new PlayerData(); // retourne des données par défaut
        }
    }

    public static void DeleteSave()
    {
        File.Delete(GetPath());
        Debug.Log("Sauvegarde supprimée.");
    }
}
