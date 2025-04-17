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
        Debug.Log("Donn�es sauvegard�es : " + GetPath());
    }

    public static PlayerData Load()
    {
        string path = GetPath();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Donn�es charg�es !");
            return data;
        }
        else
        {
            Debug.Log("Aucune sauvegarde trouv�e. Cr�ation de donn�es par d�faut.");
            return new PlayerData(); // retourne des donn�es par d�faut
        }
    }

    public static void DeleteSave()
    {
        File.Delete(GetPath());
        Debug.Log("Sauvegarde supprim�e.");
    }
}
