using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]
public class SaveData
{
    public string sceneName;
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
}

public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/savegame.json";

    public static void SaveGame(Transform playerTransform)
    {
        SaveData data = new SaveData();
        data.sceneName = SceneManager.GetActiveScene().name;
        data.playerPosX = playerTransform.position.x;
        data.playerPosY = playerTransform.position.y;
        data.playerPosZ = playerTransform.position.z;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        return null;
    }

    public static bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }
}