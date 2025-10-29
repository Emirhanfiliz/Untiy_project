using UnityEngine;

public class Fire : MonoBehaviour
{
    public Camera kamera;
    public LayerMask Ecikatman;
    public float menzil = 100f;
    public ParticleSystem muzzle;
    KarakterKontrol hpKontrol;
    Animator anim;

    private float sarjor = 5;
    private float cephane = 10;
    private float sarjorKapasitesi = 5;

    void Start()
    {
        kamera = Camera.main;
        if (kamera == null) Debug.LogError("Main Camera bulunamadı. Kamera'nın Tag'i 'MainCamera' mı?");
        hpKontrol = this.gameObject.GetComponent<KarakterKontrol>();
        anim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (hpKontrol.YasiyorMu() == true)
        {
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
                    anim.SetBool("sarjorDegistirme", true);
                    cephane -= sarjorKapasitesi - sarjor;
                    sarjor = sarjorKapasitesi;
                }
            }
            else
            {
                anim.SetBool("atesEt", false);
            }
        }
       
    }
    public void SarjorDegistirme()
     
        {
            cephane -= sarjorKapasitesi - sarjor;
            sarjor = sarjorKapasitesi;
            anim.SetBool("sarjorDegistirme", false);

        }
    

    public void AtesEtme()
    {
        if (kamera == null) kamera = Camera.main;
        if (kamera == null) return;

        if (sarjor > 0)
        {


            sarjor--;
            muzzle.Play();

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
}