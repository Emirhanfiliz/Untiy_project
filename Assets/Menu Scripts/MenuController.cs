using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections.Generic;
// using Unity.VisualScripting; // Kullanılmayanlar temizlendi
// using UnityEditor.Rendering.Fullscreen.ShaderGraph; // Kullanılmayanlar temizlendi


public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TextMeshProUGUI volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;


    [Header("Gameplay Settings")]
    [SerializeField] private TextMeshProUGUI controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;


    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TextMeshProUGUI brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;


    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;


    [Header("Comfirmation")]
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Levels To Load")]

    // Örnek sahne adını varsayılan olarak ayarlayın
    public string _newGameLevel = "Level1"; 
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;


    public Resolution[] resolutions;

    public void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


    public void NewGameDialogYes()
    {
        Debug.Log("NewGameDialogYes çağrıldı. Time.timeScale = 1 yapılıyor.");
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;

        SceneManager.LoadScene(_newGameLevel);
        Debug.Log("Sahne yükleme komutu verildi.");
    }

    // ⭐ Load Game Fonksiyonu (Kaydın varlığını kontrol eder ve yükler)
    public void LoadGameDialogYes()
{
    Debug.Log("LOAD CHECK: LoadGameDialogYes çağrıldı.");
    
    if (PlayerPrefs.HasKey("SavedLevel"))
    {
        levelToLoad = PlayerPrefs.GetString("SavedLevel");
        
        Debug.Log("LOAD CHECK: Kayıt bulundu. Yüklenmek istenen sahne adı: " + levelToLoad);
        
        // ❌ Hatanın en yaygın kaynağı burası: Eğer sahne adı boş geliyorsa
        if (string.IsNullOrEmpty(levelToLoad))
        {
            Debug.LogWarning("LOAD CHECK: Kayıtlı sahne adı boş geldi, bu bir hatadır. 'No Saved Game' diyalogu açılıyor.");
            noSavedGameDialog.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f; // Zamanı başlat
            SceneManager.LoadScene(levelToLoad);
        }
    }
    else
    {
        // Kayıt Anahtarı Hiç Yoksa
        Debug.Log("LOAD CHECK: PlayerPrefs'te 'SavedLevel' anahtarı bulunamadı. 'No Saved Game' diyalogu açılıyor.");
        noSavedGameDialog.SetActive(true);
    }
}

    
    public void SaveGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        PlayerPrefs.SetString("SavedLevel", currentSceneName);
        
        PlayerPrefs.Save(); 
        
        Debug.Log("Oyun kaydedildi. Kayıtlı Sahne: " + currentSceneName);
        
        StartCoroutine(ConfirmationBox());
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }

        PlayerPrefs.SetInt("masterSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool isFullScreen)
    {
        _isFullScreen = isFullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }
    
    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterFullScreen", (_isFullScreen ? 1 : 0));
        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        Screen.fullScreen = _isFullScreen;
        QualitySettings.SetQualityLevel(_qualityLevel);
        StartCoroutine(ConfirmationBox());
    }


    public void ResetButton(string MenuType)
    {
        if (MenuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");
            
            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);
            
            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;

            GraphicsApply();
        }


        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (MenuType == "Gameplay")
        {
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}