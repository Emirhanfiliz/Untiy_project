using UnityEngine;

public class Fire : MonoBehaviour
{
    public Camera kamera;
    public LayerMask Ecikatman;
    public float menzil = 100f;

    void Start()
    {
        kamera = Camera.main;
        if (kamera == null) Debug.LogError("Main Camera bulunamadı. Kamera'nın Tag'i 'MainCamera' mı?");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AtesEtme();
        }
    }

    void AtesEtme()
    {
        if (kamera == null) kamera = Camera.main;
        if (kamera == null) return;

        // Ekran ortasındaki crosshair için viewport center kullanılıyor.
        Ray ray = kamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Görsel debug (kısa süreli)
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

            // HasarAl metodunun parametre alıp almadığını kontrol edin.
            // Eğer HasarAl() parametre istiyorsa örn. HasarAl(25);
            dusman.HasarAl();
            Debug.Log("Dusmana hasar verildi: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Hiçbir şey vurulmadı ya da LayerMask engelliyor.");
            // Test için şu satırı kullanarak tüm layer'ları kontrol edebilirsin:
            // if (Physics.Raycast(ray, out hit, menzil)) Debug.Log("Hit (no mask): " + hit.collider.name);
        }
    }
}
