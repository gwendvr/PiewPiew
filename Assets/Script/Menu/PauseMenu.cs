using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    
    void Start()
    {
        // Assure-toi d'activer ce script uniquement si nécessaire
        if (pauseMenuUI)
            pauseMenuUI.SetActive(false); // Masquer le menu au départ
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Reprendre le temps du jeu
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Mettre le jeu en pause
        isPaused = true;
    }

    public void SaveAndQuit()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameManager.instance.SaveGame(player.transform);
        }

        SceneManager.LoadScene("SC_Menu");
    }
}

