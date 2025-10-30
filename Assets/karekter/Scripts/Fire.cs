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
    private bool reloadYapiliyor = false;

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
        if (!hpKontrol.YasiyorMu()) return;

        // Reload sırasında hiçbir işlem yapılmasın
        if (reloadYapiliyor)
        {
            anim.SetBool("atesEt", false);
            return;
        }

        // Manuel reload (R tuşu)
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (sarjor < sarjorKapasitesi && cephane > 0)
            {
                ReloadBaslat();
                return;
            }
        }

        // Ateş etme
        if (Input.GetMouseButton(0))
        {
            if (sarjor > 0)
            {
                anim.SetBool("atesEt", true);
            }

            if (sarjor <= 0)
            {
                anim.SetBool("atesEt", false);
                if (cephane > 0)
                {
                    ReloadBaslat();
                }
            }
        }
        else
        {
            anim.SetBool("atesEt", false);
        }
    }

    // Reload işlemini başlatan yardımcı fonksiyon
    private void ReloadBaslat()
    {
        // Her ihtimale karşı ateşi ve efektleri durdur
        anim.SetBool("atesEt", false);
        if (muzzle.isPlaying) muzzle.Stop();

        sesKaynagi.PlayOneShot(reloadSes);
        anim.SetBool("sarjorDegistirme", true);
        reloadYapiliyor = true;
    }

    // Bu fonksiyonu reload animasyonunun son frame’ine event olarak ekle
    public void SarjorDegistirme()
    {
        float eksikMermi = sarjorKapasitesi - sarjor;
        if (cephane >= eksikMermi)
        {
            cephane -= eksikMermi;
            sarjor = sarjorKapasitesi;
        }
        else
        {
            sarjor += cephane;
            cephane = 0;
        }

        anim.SetBool("sarjorDegistirme", false);
        reloadYapiliyor = false;
    }

    public void AtesEtme()
    {
        if (reloadYapiliyor) return;
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
                if (dusman != null)
                {
                    dusman.HasarAl();
                }
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
