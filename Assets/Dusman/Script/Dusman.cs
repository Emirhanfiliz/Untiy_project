using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Dusman : MonoBehaviour
{
    public float zombiHP = 100;
    private Animator zombiAnim;
    private bool zombiOlu;
    public GameObject hedefOyuncu;
    public float Kovalamamesafe;
    public float saldirmaMesafesi;
    private float mesafe;
    private NavMeshAgent zombiNavMesh;

    private AudioSource sesKaynagi;
    public AudioClip saldirmaSesi;

    public float attackRate = 1f;
    private float nextAttackTime = 0f;
    
    // Hız Ayarları
    [Header("Hız Ayarları")]
    [SerializeField] private float temelHiz = 3.5f; // Devriye/Başlangıç Hızı
    [SerializeField] private float kovalamaHizi = 4.0f; // Kovalama sırasındaki sabit hız

    // Patrol Script referansı
    public Patrol patrolScript;

    void Start()
    {
        zombiAnim = GetComponent<Animator>();
        hedefOyuncu = GameObject.Find("Ch48_nonPBR");
        zombiNavMesh = GetComponent<NavMeshAgent>();
        sesKaynagi = GetComponent<AudioSource>();

        patrolScript = GetComponent<Patrol>();
        if (patrolScript != null)
            patrolScript.enabled = true;  // Başlangıçta devriye aktif
            
        // Başlangıç hızı olarak temel hızı ata
        zombiNavMesh.speed = temelHiz;
    }

    void Update()
    {
        if (zombiHP <= 0)
        {
            if(!zombiOlu)
            {
                zombiOlu = true;
                zombiAnim.SetBool("oldu", true);
                zombiAnim.SetBool("saldiriyor", false);
                zombiAnim.SetBool("yuruyor", false);
                zombiAnim.SetBool("yavas", false);

                zombiNavMesh.isStopped = true;
                zombiNavMesh.enabled = false;

                if(patrolScript != null)
                    patrolScript.enabled = false;

                StartCoroutine(Yokol());
            }
            return;
        }

        KarakterKontrol oyuncu = hedefOyuncu.GetComponent<KarakterKontrol>();
        if (!oyuncu.hayattaMi)
        {
            zombiAnim.SetBool("saldiriyor", false);
            zombiAnim.SetBool("yuruyor", false);
            zombiAnim.SetBool("yavas", false);

            if(patrolScript != null)
                patrolScript.enabled = false;

            zombiNavMesh.isStopped = true;
            return;
        }

        mesafe = Vector3.Distance(transform.position, hedefOyuncu.transform.position);

        if (mesafe < saldirmaMesafesi)
        {
            // Saldırma
            zombiNavMesh.isStopped = true;
            zombiAnim.SetBool("yuruyor", false);
            zombiAnim.SetBool("saldiriyor", true);
            zombiAnim.SetBool("yavas", false);

            if(patrolScript != null)
                patrolScript.enabled = false;

            transform.LookAt(hedefOyuncu.transform.position);
            
            // Saldırıya geçince hızı temel devriye hızına çek (Daha sabit bir duruş için)
            zombiNavMesh.speed = temelHiz;
        }
        else if (mesafe < Kovalamamesafe)
        {
            // Kovalama
            zombiNavMesh.isStopped = false;
            
            // Hızı doğrudan Kovalama Hızına ayarla (4.0f)
            zombiNavMesh.speed = kovalamaHizi; 
            
            zombiNavMesh.SetDestination(hedefOyuncu.transform.position);
            zombiAnim.SetBool("yuruyor", true);
            zombiAnim.SetBool("saldiriyor", false);
            zombiAnim.SetBool("yavas", false);

            if(patrolScript != null)
                patrolScript.enabled = false;

            transform.LookAt(hedefOyuncu.transform.position);
        }
        else
        {
            // Uzak → Patrol devriye başlasın
            zombiAnim.SetBool("saldiriyor", false);
            zombiAnim.SetBool("yuruyor", false);
            
            // Devriye hızı her zaman temel hız olsun
            zombiNavMesh.speed = temelHiz; 

            if(patrolScript != null)
                patrolScript.enabled = true;  // Devriye başlasın
        }
    }

    public void HasarVer()
    {
        KarakterKontrol oyuncu = hedefOyuncu.GetComponent<KarakterKontrol>();
        if (zombiOlu || !oyuncu.hayattaMi) return;
        if (Time.time < nextAttackTime) return;

        mesafe = Vector3.Distance(transform.position, hedefOyuncu.transform.position);
        if (mesafe <= saldirmaMesafesi)
        {
            sesKaynagi.PlayOneShot(saldirmaSesi);
            oyuncu.HasarAl();
            nextAttackTime = Time.time + attackRate;
        }
    }

    IEnumerator Yokol()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    public void HasarAl()
    {
        if (zombiOlu) return;
        zombiHP -= Random.Range(15, 25);
    }
}