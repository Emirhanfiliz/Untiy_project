using UnityEngine;

public class TusIleDur : MonoBehaviour
{
    public GeriSayim geriSayim; // Daha önceki geri sayım script'i
    public KeyCode tetikTus = KeyCode.E; // E tuşu ile tetiklenecek
    public float etkileşimMesafe = 3f; // Cube’e kaç birim yaklaşıldığında basabilir

    private Transform oyuncu;

    void Start()
    {
        oyuncu = GameObject.FindGameObjectWithTag("Oyuncu").transform;
        if(oyuncu == null)
        {
            Debug.LogError("Oyuncu tag'i bulunamadı! Oyuncu objesine 'Oyuncu' tag'i ekle.");
        }
    }

    void Update()
    {
        if(oyuncu != null)
        {
            float mesafe = Vector3.Distance(transform.position, oyuncu.position);

            if(mesafe <= etkileşimMesafe && Input.GetKeyDown(tetikTus))
            {
                geriSayim.DurdurSayac();
                Debug.Log("Sayaç E tuşu ile durduruldu!");
            }
        }
    }
}
