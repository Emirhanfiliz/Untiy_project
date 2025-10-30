using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] noktalar;
    private int mevcutNokta = 0;
    private NavMeshAgent agent; // artık private, dışarıdan erişmeye gerek yok
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        if(noktalar.Length > 0)
        {
            agent.isStopped = false; // agent aktif
            agent.speed = 2f;        // speed makul olsun
            agent.SetDestination(noktalar[mevcutNokta].position);
        }
    }

    void Update()
    {
        if(!enabled) return; // script disable ise hiç çalışmasın
        if(noktalar.Length == 0) return;

        // Noktaya ulaşıldı mı kontrol
        if(agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            MevcutNoktayaGit();
        }

        // Yavaş bool kontrolü
        if(agent.velocity.sqrMagnitude > 0.001f) // daha hassas
        {
            anim.SetBool("yavas", true);
        }
        else
        {
            anim.SetBool("yavas", false);
        }
    }

    void MevcutNoktayaGit()
    {
        if(noktalar.Length == 0) return;
        mevcutNokta = (mevcutNokta + 1) % noktalar.Length;
        agent.SetDestination(noktalar[mevcutNokta].position);
    }
}
