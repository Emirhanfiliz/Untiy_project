using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GeriSayim : MonoBehaviour
{
    // === Süre ve Metin Ayarları ===
    public TMP_Text sayacText;
    [Header("Geri Bildirim")]
    public TMP_Text feedbackText; 
    public float toplamSure = 10f;
    private float kalanSure;
    private bool durdur = false; // TusIleDur script'i bu değişkene IsDurduruldu özelliği üzerinden erişecek.
    private bool patladiMi = false;

    // === Ses Ayarları ===
    [Header("Ses Ayarları")]
    public AudioSource audioSource;
    public AudioClip bipSound;
    
    // Sesin hızlanma kontrolü
    private bool hizlanmaBasladi = false; 

    // YENİ: TusIleDur script'inin sayaç durumunu kontrol etmesi için public özellik
    public bool IsDurduruldu 
    {
        get { return durdur; }
    }

    void Start()
    {
        kalanSure = toplamSure;
        UpdateText();

        // Başlangıçta büyük geri bildirim metnini gizle
        if (feedbackText != null)
        {
            feedbackText.gameObject.SetActive(false);
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Time.timeScale = 1f;
        
        // Ses çalmayı başlat
        if (audioSource != null && bipSound != null)
        {
            audioSource.clip = bipSound;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (!durdur && kalanSure > 0f)
        {
            kalanSure -= Time.deltaTime;
            
            // SON 10 SANİYE KONTROLÜ (Gerilimi artır)
            if (kalanSure <= 10f && !hizlanmaBasladi && audioSource != null)
            {
                audioSource.pitch = 1.5f; 
                hizlanmaBasladi = true;
            }
            
            if (kalanSure <= 0f)
            {
                kalanSure = 0f;
                durdur = true;
                Patlat();
            }

            UpdateText();
        }
    }

    void UpdateText()
    {
        int dakika = Mathf.FloorToInt(kalanSure / 60);
        int saniye = Mathf.FloorToInt(kalanSure % 60);
        sayacText.text = string.Format("{0:00}:{1:00}", dakika, saniye);
    }

    public void DurdurSayac()
    {
        if (kalanSure > 0f && !durdur)
        {
            durdur = true;
            
            // İMHA ANINDA SESİ DURDUR
            if (audioSource != null)
            {
                audioSource.Stop(); 
                audioSource.pitch = 1f;
            }
            
            // OYUN SONU MESAJI
            sayacText.gameObject.SetActive(false); 
            
            if (feedbackText != null)
            {
                 feedbackText.gameObject.SetActive(true);
                 feedbackText.text = "GOREV BASARILI!\nMENUYE YONLENDIRILIYORSUNUZ";
            }

            Debug.Log("Bomba Başarıyla İmha Edildi! Savunmacılar Kazandı.");
            
            // 3 saniye sonra ana menüye git
            Invoke("LoadMainMenu", 3f);
        }
    }
    void Patlat()
{
    if (patladiMi) return;
    
    patladiMi = true;
    
    
    if (audioSource != null)
    {
        audioSource.Stop();
        audioSource.pitch = 1f;
    }
    
    // OYUN SONU MESAJI
    sayacText.gameObject.SetActive(false); 
    
    if (feedbackText != null)
    {
        feedbackText.gameObject.SetActive(true);
        
        
        
       feedbackText.text = "GOREV BASARISIZ \nMENUYE YONLENDIRILIYORSUNUZ";
    
    }
    
    Debug.Log("BOOM! Bomba Patladı! Saldırganlar Kazandı.");
    
    
    Invoke("LoadMainMenu", 3f);
}

    void LoadMainMenu()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

      SceneManager.LoadScene("MainMenu");
    }
}
