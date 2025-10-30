using UnityEngine;

public class Fire : MonoBehaviour
{
    public Camera kamera;
    public LayerMask Ecikatman;
    public float menzil = 100f;
    public ParticleSystem muzzle;
    KarakterKontrol hpKontrol;
    Animator anim;

    private float sarjor = 30;
    private float cephane = 240;
    private float sarjorKapasitesi = 30;
    private bool reloadYapiliyor = false; // reload sırasında ateşi engellemek için

    AudioSource sesKaynagi;
    public AudioClip atesSes;
    public AudioClip reloadSes;

    void Start()
    {
        kamera = Camera.main;
        if (kamera == null) Debug.LogError("Main Camera bulunamadı. Kamera'nın Tag'i 'MainCamera' mı?");
        hpKontrol = this.gameObject.GetComponent<KarakterKontrol>();
        anim = this.gameObject.GetComponent<Animator>();
        sesKaynagi = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (hpKontrol.YasiyorMu() == true)
        {
            // Reload sırasında ateş etmeyi tamamen engelle
            if (reloadYapiliyor)
            {
                anim.SetBool("atesEt", false);
                return;
            }

            if (Input.GetMouseButton(0))
            {
                if (sarjor > 0)
                {
                    anim.SetBool("atesEt", true);
                }

                if (sarjor <= 0)
                {
                    anim.SetBool("atesEt", false);
                }

                if (sarjor <= 0 && cephane > 0)
                {
                    sesKaynagi.PlayOneShot(reloadSes);
                    anim.SetBool("sarjorDegistirme", true);
                    reloadYapiliyor = true;
                }
            }
            else
            {
                anim.SetBool("atesEt", false);
            }
        }
    }

    // Bu fonksiyonu animasyonun sonuna (Reload bittiği frame'e) Animation Event olarak ekle
    public void SarjorDegistirme()
    {
        cephane -= sarjorKapasitesi - sarjor;
        sarjor = sarjorKapasitesi;
        anim.SetBool("sarjorDegistirme", false);

        // reload bitince ateş etmeye tekrar izin ver ama otomatik ateşlenmesin
        reloadYapiliyor = false;
    }

    public void AtesEtme()
    {
        if (reloadYapiliyor) return; // reload sırasında ateş etme
        if (kamera == null) kamera = Camera.main;
        if (kamera == null) return;

        if (sarjor > 0)
        {
            sarjor--;
            muzzle.Play();
            sesKaynagi.PlayOneShot(atesSes);

            Ray ray = kamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, menzil, Ecikatman.value))
            {
                Dusman dusman = hit.collider.GetComponent<Dusman>();
                if (dusman == null)
                {
                    return;
                }
                dusman.HasarAl();
            }
        }
    }

    public float GetSarjor()
    {
        return sarjor;
    }

    public float GetCephane()
    {
        return cephane;
    }
}
