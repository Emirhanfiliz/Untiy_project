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

    void Start()
    {
        zombiAnim = this.GetComponent<Animator>();
        hedefOyuncu = GameObject.Find("Ch48_nonPBR");
        zombiNavMesh = this.GetComponent<NavMeshAgent>();
    }

    void Update()

    {

        if (zombiHP <= 0)
        {
            zombiOlu = true;
        }
        if (zombiOlu == true)
        {
            zombiAnim.SetBool("oldu", true);
            StartCoroutine(Yokol());
        }
        else
        {
            mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);
            if (mesafe < Kovalamamesafe)
            {
                zombiNavMesh.isStopped = false;
                zombiNavMesh.SetDestination(hedefOyuncu.transform.position);
                zombiAnim.SetBool("yuruyor", true);

                this.transform.LookAt(hedefOyuncu.transform.position);

            }
            else
            {
                zombiAnim.SetBool("yuruyor", false);
                zombiAnim.SetBool("saldiriyor", false);
                zombiNavMesh.isStopped = true;
            }
            if (mesafe < saldirmaMesafesi)
            {
                zombiNavMesh.isStopped = true;
                zombiAnim.SetBool("yuruyor", false);
                zombiAnim.SetBool("saldiriyor", true);
                this.transform.LookAt(hedefOyuncu.transform.position);
            }
        }

    }
    public void HasarVer()
    {
        hedefOyuncu.GetComponent<KarakterKontrol>().HasarAl();
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