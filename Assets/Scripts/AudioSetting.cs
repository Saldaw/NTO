using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
    [SerializeField] string volumeParmetr = "MasterVolume";
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider slider;

    private const float MULIPLIER = 20f;
    private float volumeValue;

    private void Awake()
    {
        slider = GetComponent<Slider>();

        Invoke("LoadPrefs", 0.01f);

        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        volumeValue = Mathf.Log10(value)*MULIPLIER;
        audioMixer.SetFloat(volumeParmetr, volumeValue);

        SavePrefs();
    }

    private void LoadPrefs()
    {
        volumeValue = PlayerPrefs.GetFloat(volumeParmetr, Mathf.Log10(slider.value) * MULIPLIER);

        audioMixer.SetFloat(volumeParmetr, volumeValue);

        slider.value = Mathf.Pow(10f, volumeValue / MULIPLIER);
    }


    private void SavePrefs()
    {
        PlayerPrefs.SetFloat(volumeParmetr, volumeValue);
    }

    
}
