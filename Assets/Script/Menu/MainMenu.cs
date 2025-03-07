
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    private string saveFilePath;
    public Button continueButton;
    public Button deleteSaveButton;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/savegame.json";
        if (continueButton != null)
        {
            continueButton.interactable = GameManager.instance.SaveFileExists();
        }
        if (deleteSaveButton != null)
        {
            deleteSaveButton.interactable = File.Exists(saveFilePath);
        }
    }

    public void ContinueGame()
    {
        GameManager.instance.loadPosition = true;
        GameManager.instance.LoadGame();
    }

    public void NewGame()
    {
        GameManager.instance.loadPosition = false; // Assure un nouveau départ
        GameManager.instance.LoadGame();
    }
    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Sauvegarde supprimée.");
            
            if (continueButton != null)
            {
                continueButton.interactable = false;
            }
            
            if (deleteSaveButton != null)
            {
                deleteSaveButton.interactable = false;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
