using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [Header("All Menu`s")]
    public GameObject pauseMenuUI;
    public GameObject EndGameMenuUI;
    
    [Header("Menu Panels")]
    public GameObject optionsPanel; 

    public static bool GameIsStopped = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel != null && optionsPanel.activeSelf)
            {
                CloseOptions();
            }
            else if (GameIsStopped)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false); 
        }
        
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameIsStopped = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        GameIsStopped = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void LoadMenu()
    {
        SaveGameData();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Oyun kaydedildi ve Ana Menü'ye dönüldü.");
    }

    public void LoadOptions()
    {

        
        
        SceneManager.LoadScene("OptionsController"); 


        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(true);
        }
    }
    
    public void CloseOptions()
    {
        if (optionsPanel != null)
        {
            optionsPanel.SetActive(false);
        }
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
    }


public void QuitGame()
{
    SaveGameData(); 
    Time.timeScale = 1f;

    // Menüye dönerken fare imlecini görünür yapmayı unutmayın
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    SceneManager.LoadScene("MainMenu");
    // Application.Quit() satırını buradan kaldırmanız en doğrusudur.
}
    
    private void SaveGameData()
{
    // Kaydedilen sahne adını konsola yazdırın
    string sceneToSave = SceneManager.GetActiveScene().name;
    PlayerPrefs.SetString("SavedLevel", sceneToSave);
    PlayerPrefs.Save();
    
    Debug.Log("SAVE CHECK: Oyun verileri kaydedildi. Anahtar: SavedLevel | Değer: " + sceneToSave);
}

    public void ApplicationQuit()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}