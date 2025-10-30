using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    public float zombiHP = 100;
    Animator zombiAnim;
    bool zombiOlu;
    public GameObject hedefOyuncu;
    public float Kovalamamesafe;
    public float saldirmaMesafesi;
    float mesafe;
    NavMeshAgent zombiNavMesh;

    AudioSource sesKaynagi;
    public AudioClip saldirmaSesi;

    // Cooldown ayarı
    public float attackRate = 1f; // saniye başına 1 saldırı
    private float nextAttackTime = 0f;

    void Start()
    {
        zombiAnim = this.GetComponent<Animator>();
        hedefOyuncu = GameObject.Find("Ch48_nonPBR");
        zombiNavMesh = this.GetComponent<NavMeshAgent>();
        sesKaynagi = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (zombiHP <= 0)
        {
            if(!zombiOlu) // sadece bir kere çalışsın
            {
                zombiOlu = true;
                zombiAnim.SetBool("oldu", true);
                StartCoroutine(Yokol());
                zombiNavMesh.isStopped = true;
            }
            return;
        }

        mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);

        if (mesafe < saldirmaMesafesi)
        {
            // saldırma mesafesinde
            zombiNavMesh.isStopped = true;
            zombiAnim.SetBool("yuruyor", false);
            zombiAnim.SetBool("saldiriyor", true);
            this.transform.LookAt(hedefOyuncu.transform.position);
        }
        else if (mesafe < Kovalamamesafe)
        {
            // kovalama mesafesinde
            zombiNavMesh.isStopped = false;
            zombiNavMesh.SetDestination(hedefOyuncu.transform.position);
            zombiAnim.SetBool("yuruyor", true);
            zombiAnim.SetBool("saldiriyor", false);
            this.transform.LookAt(hedefOyuncu.transform.position);
        }
        else
        {
            // uzak
            zombiNavMesh.isStopped = true;
            zombiAnim.SetBool("yuruyor", false);
            zombiAnim.SetBool("saldiriyor", false);
        }
    }

    // Bu fonksiyon attack animasyonunun vuracağı frame'ine animation event ile eklenmeli
    public void HasarVer()
    {
        if (Time.time < nextAttackTime) return; // cooldown kontrolü

        mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);
        if (mesafe <= saldirmaMesafesi)
        {
            sesKaynagi.PlayOneShot(saldirmaSesi);
            hedefOyuncu.GetComponent<KarakterKontrol>().HasarAl();
            nextAttackTime = Time.time + attackRate;
        }
    }

    IEnumerator Yokol()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

    public void HasarAl()
    {
        zombiHP -= Random.Range(15, 25);
    }
}
