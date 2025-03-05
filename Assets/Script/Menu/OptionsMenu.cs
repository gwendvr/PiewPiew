using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle vSyncToggle;
    public Slider volumeSlider;
    public Button saveButton;

    private Resolution[] availableResolutions;
    private int chosenResolutionIndex;
    
    void Start()
    {
        // Initialiser les rÃ©solutions
        availableResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < availableResolutions.Length; i++)
        {
            string option = availableResolutions[i].width + " x " + availableResolutions[i].height;
            options.Add(option);

            if (availableResolutions[i].width == Screen.currentResolution.width && availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // Initialiser VSync
        vSyncToggle.isOn = QualitySettings.vSyncCount > 0;
        vSyncToggle.onValueChanged.AddListener(SetVSync);

        // Initialiser Volume
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        // Sauvegarder les modifications
        saveButton.onClick.AddListener(SaveSettings);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = availableResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVSync(bool isVSyncOn)
    {
        QualitySettings.vSyncCount = isVSyncOn ? 1 : 0;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);
        PlayerPrefs.SetInt("VSync", vSyncToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
        Debug.Log("Settings Saved");
    }
    
    private void OnEnable()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex", resolutionDropdown.value);
        vSyncToggle.isOn = PlayerPrefs.GetInt("VSync", vSyncToggle.isOn ? 1 : 0) == 1;
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", volumeSlider.value);
    }
}