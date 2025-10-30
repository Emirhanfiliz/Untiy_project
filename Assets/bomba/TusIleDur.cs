using UnityEngine;

public class TusIleDur : MonoBehaviour
{
    public GeriSayim geriSayim; // Geri sayım script'i Inspector'dan bağlanacak
    public KeyCode tetikTus = KeyCode.E;
    public float etkileşimMesafe = 3f;

    private Transform oyuncu;

    void Start()
    {
        // Oyuncuyu "Player" tag'i ile arıyoruz.
        oyuncu = GameObject.FindGameObjectWithTag("Player").transform;
        if(oyuncu == null)
        {
            // Hata mesajı: Oyuncu objesine "Player" tag'i eklenmeli.
            Debug.LogError("Player tag'i bulunamadı! Oyuncu objesine 'Player' tag'i ekle.");
        }
    }

    void Update()
    {
        // GÜVENLİK KONTROLÜ: 
        // 1. Oyuncu veya GeriSayim referansı yoksa çık.
        // 2. Sayaç zaten durmuşsa (imha/patlama gerçekleşmişse) çık.
        if (oyuncu == null || geriSayim == null || geriSayim.IsDurduruldu) return;
        
        float mesafe = Vector3.Distance(transform.position, oyuncu.position);

        if(mesafe <= etkileşimMesafe && Input.GetKeyDown(tetikTus))
        {
            // DurdurSayac çağrıldığında, GeriSayim script'i oyun sonu mantığını (mesaj gösterme, sahne yükleme) yönetecektir.
            geriSayim.DurdurSayac();
            
            // İmha başarılı olduktan sonra bu script'i kapat (tekrar basılmasını engeller)
            enabled = false; 
        }
    }
}
