using UnityEngine;

public class Fire : MonoBehaviour
{
    public Camera kamera;
    public LayerMask Ecikatman;
    public float menzil = 100f;
    public ParticleSystem muzzle;
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
        if (hpKontrol.YasiyorMu() == true)
        {
            if (Input.GetMouseButton(0))
            {
                anim.SetBool("atesEt", true);
            }
            else
            {
                anim.SetBool("atesEt", false);
            }
        }
        else
        {
            anim.SetBool("atesEt", false);
        }
    }

    public void AtesEtme()
    {
        if (kamera == null) kamera = Camera.main;
        if (kamera == null) return;

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