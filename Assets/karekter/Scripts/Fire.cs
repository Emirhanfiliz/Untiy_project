using UnityEngine;

public class Fire : MonoBehaviour
{
    public Camera kamera;
    public LayerMask Ecikatman;
    public float menzil = 100f;
    KarakterKontrol hpKontrol;
    Animator anim;

    void Start()
    {
        kamera = Camera.main;
        if (kamera == null) Debug.LogError("Main Camera bulunamadı. Kamera'nın Tag'i 'MainCamera' mı?");
        hpKontrol = this.gameObject.GetComponent<KarakterKontrol>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        // Karakter hayattaysa kontrol et
        if (hpKontrol.YasiyorMu() == true)
        {
            // Fare sol tuşu BASILI TUTULUYORSA
            if (Input.GetMouseButton(0))
            {
                // Animasyon durumunu TRUE yap. Bu, animasyonun sürekli oynamasını sağlar.
                anim.SetBool("atesEt", true);
            }
            // Fare sol tuşu BIRAKILDIĞI AN VEYA BASILI DEĞİLSE
            else
            {
                // Animasyon durumunu FALSE yap. Bu, atış animasyonundan çıkılmasını sağlar.
                anim.SetBool("atesEt", false);
            }
        }
        else
        {
            // Karakter ölmüşse ateş etme animasyonunu kapat.
            anim.SetBool("atesEt", false);
        }
    }

    // Bu metod, SADECE Animator component'indeki Animation Event tarafından çağrılmalıdır.
    public void AtesEtme()
    {
        // Önceki kodunuzdaki Raycast ve hasar verme mantığı burada kalır.
        if (kamera == null) kamera = Camera.main;
        if (kamera == null) return;

        Ray ray = kamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * menzil, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, menzil, Ecikatman.value))
        {
            Debug.Log("Ray hit: " + hit.collider.name);

            Dusman dusman = hit.collider.GetComponent<Dusman>();
            if (dusman == null)
            {
                Debug.LogWarning("Hit objesinde Dusman component yok: " + hit.collider.name);
                return;
            }
            dusman.HasarAl();
            Debug.Log("Dusmana hasar verildi: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Hiçbir şey vurulmadı ya da LayerMask engelliyor.");
        }
    }
}