using UnityEngine;
using TMPro; // TextMeshPro kullanıyorsan

public class GeriSayim : MonoBehaviour
{
    public TMP_Text sayacText; // UI Text
    public float toplamSure = 300f; // 5 dakika = 300 saniye
    private float kalanSure;
    private bool durdur = false;

    void Start()
    {
        kalanSure = toplamSure;
        UpdateText();
    }

    void Update()
    {
        if (!durdur && kalanSure > 0f)
        {
            kalanSure -= Time.deltaTime;
            if (kalanSure < 0f) kalanSure = 0f;
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
        durdur = true;
    }

    public void BaslatSayac()
    {
        durdur = false;
    }

    public void Sifirla()
    {
        kalanSure = toplamSure;
        UpdateText();
    }
}
