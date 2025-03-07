using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private string saveFilePath;
    public bool loadPosition = false;
    private SaveData savedData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ne pas détruire en changeant de scène
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        saveFilePath = Application.persistentDataPath + "/savegame.json";
    }

    public void SaveGame(Transform playerTransform)
    {
        SaveData data = new SaveData
        {
            sceneName = SceneManager.GetActiveScene().name,
            playerPosX = playerTransform.position.x,
            playerPosY = playerTransform.position.y,
            playerPosZ = playerTransform.position.z
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadGame()
    {
        Time.timeScale = 1;
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            savedData = JsonUtility.FromJson<SaveData>(json);
            loadPosition = true;

            SceneManager.LoadScene(savedData.sceneName);
            SceneManager.sceneLoaded += OnSceneLoaded; // S'assurer que le joueur est bien placé
        }
        else
        {
            Debug.Log("Aucune sauvegarde trouvée, chargement de la scène par défaut.");
            SceneManager.LoadScene("SC_MapMain");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (loadPosition)
        {
            StartCoroutine(SetPlayerPosition());
            loadPosition = false;
            SceneManager.sceneLoaded -= OnSceneLoaded; // Nettoyer l'événement après placement
        }
    }

    private IEnumerator SetPlayerPosition()
    {
        yield return null; // Attendre un frame pour laisser le temps à la scène de se charger

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && savedData != null)
        {
            player.transform.position = new Vector3(savedData.playerPosX, savedData.playerPosY, savedData.playerPosZ);
        }
        else
        {
            Debug.LogWarning("Le joueur n'a pas été trouvé après le chargement de la scène.");
        }
    }

    public bool SaveFileExists()
    {
        return File.Exists(saveFilePath);
    }
}
