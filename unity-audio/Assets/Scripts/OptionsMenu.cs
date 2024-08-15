using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


/// <summary>
/// Options menu button methods
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle invertYToggle;
    [SerializeField] private Button backButton;
    [SerializeField] private Button applyButton;

    private const string BGM_KEY = "BGMVolume";
    private const string SFX_KEY = "SFXVolume";
    private const string INVERT_Y_KEY = "InvertY";

    // init
    private void Start()
    {
        LoadSettings();
        SetupUI();
    }

    // add listeners
    private void SetupUI()
    {
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        invertYToggle.onValueChanged.AddListener(SetInvertY);
        backButton.onClick.AddListener(BackToMainMenu);
        applyButton.onClick.AddListener(ApplySettings);
    }

    // Get playerprefs
    private void LoadSettings()
    {
        bgmSlider.value = PlayerPrefs.GetFloat(BGM_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_KEY, 1f);
        invertYToggle.isOn = PlayerPrefs.GetInt(INVERT_Y_KEY, 0) == 1;

        SetBGMVolume(bgmSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetInvertY(invertYToggle.isOn);
    }

    // set BGM volume
    private void SetBGMVolume(float volume)
    {
        SetGroupVolume("BGM", volume);
        PlayerPrefs.SetFloat(BGM_KEY, volume);
    }

    // set SFX volume
    private void SetSFXVolume(float volume)
    {
        SetGroupVolume("Running", volume);
        SetGroupVolume("Landing", volume);
        SetGroupVolume("Ambience", volume);
        PlayerPrefs.SetFloat(SFX_KEY, volume);
    }

    // set volume by group
    private void SetGroupVolume(string groupName, float volume)
    {
        float dbVolume = volume > 0.0001f ? Mathf.Log10(volume) * 20 : -80f;
        audioMixer.SetFloat(groupName, dbVolume);
    }

    // Trigger invertY in CameraController if ticked
    private void SetInvertY(bool inverted)
    {
        CameraController.invertY = inverted;
        PlayerPrefs.SetInt(INVERT_Y_KEY, inverted ? 1 : 0);
    }

    // Switch to main menu
    private void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // apply settings to Playerprefs
    private void ApplySettings()
    {
        PlayerPrefs.Save();
    }
}