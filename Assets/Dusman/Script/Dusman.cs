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
    public float mesafe;
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
            float mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);
            if (mesafe < Kovalamamesafe)
            {
                zombiNavMesh.SetDestination(hedefOyuncu.transform.position);
            }
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