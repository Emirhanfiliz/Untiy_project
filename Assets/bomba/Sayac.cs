using UnityEngine;
using TMPro;

public class GeriSayim : MonoBehaviour
{
    // === Süre ve Metin Ayarları ===
    public TMP_Text sayacText;
    public float toplamSure = 180f;
    private float kalanSure;
    private bool durdur = false;
    private bool patladiMi = false;

    // === Ses Ayarları ===
    [Header("Ses Ayarları")]
    public AudioSource audioSource; // Audio Source bileşeni Inspector'dan bağlanacak
    public AudioClip bipSound;      // Bip ses dosyası Inspector'dan bağlanacak
    
    // Sesin hızlanma kontrolü
    private bool hizlanmaBasladi = false; 

    void Start()
    {
        kalanSure = toplamSure;
        UpdateText();
        
        // Ses çalmayı başlat
        if (audioSource != null && bipSound != null)
        {
            audioSource.clip = bipSound;
            audioSource.Play(); // Loop açıksa sürekli çalmaya başlar
        }
    }

    void Update()
    {
        if (!durdur && kalanSure > 0f)
        {
            kalanSure -= Time.deltaTime;
            
            // SON 10 SANİYE KONTROLÜ (Gerilimi artır)
            if (kalanSure <= 10f && !hizlanmaBasladi)
            {
                // Sesin çalma hızını (pitch) artır
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
                audioSource.pitch = 1f; // Pitch'i sıfırla
            }
            
            sayacText.text = "GOREV BASARILI!!";
            Debug.Log("Bomba Başarıyla İmha Edildi! Savunmacılar Kazandı.");
        }
    }
    
    void Patlat()
    {
        if (patladiMi) return;
        
        patladiMi = true;
        
        // PATLAMA ANINDA SESİ DURDUR
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.pitch = 1f; // Pitch'i sıfırla
            // Opsiyonel: Burada tek seferlik bir patlama sesi çalabilirsiniz.
        }
        
        sayacText.text = "PATLAMA!";
        Debug.Log("BOOM! Bomba Patladı! Saldırganlar Kazandı.");
    }
}