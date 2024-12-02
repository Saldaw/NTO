using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphicsSettingsMenu : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    private Resolution[] resolutions;

    private void Start()
    {
        resolutionDropdown.ClearOptions();
        DetermineAvailableScreenResolutions();
        LoadSettings();
    }

    private void DetermineAvailableScreenResolutions()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        foreach (var resolution in resolutions)
        {
            string option = $"{resolution.width}x{resolution.height} {(int)resolution.refreshRateRatio.value}Ãö";
            options.Add(option);
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    public void SetFullscreen(bool isFullsceen)
    {
        Screen.fullScreen = isFullsceen;
        PlayerPrefs.SetInt("FullScreenPreference", System.Convert.ToInt32(Screen.fullScreen));
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionDropdown.value = resolutionIndex;

        Application.targetFrameRate = (int)resolution.refreshRateRatio.value;
        PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        qualityDropdown.value = qualityIndex;
        PlayerPrefs.SetInt("QualitySettingsPreference", qualityDropdown.value);
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("QualitySettingsPreference"))
        {
            SetQuality(PlayerPrefs.GetInt("QualitySettingsPreference"));
        }
        else
        {
            SetQuality(2);
        }

        if (PlayerPrefs.HasKey("ResolutionPreference"))
        {
            SetResolution(PlayerPrefs.GetInt("ResolutionPreference"));
        }
        else
        {
            SetResolution(resolutions.Length - 1);
        }

        if (PlayerPrefs.HasKey("FullScreenPreference"))
        {
            SetFullscreen(System.Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenPreference")));
        }
        else
        {
            SetFullscreen(true);
        }
    }

}
