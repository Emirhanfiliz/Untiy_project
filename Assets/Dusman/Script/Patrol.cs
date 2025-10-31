using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    public Transform[] noktalar;
    private int mevcutNokta = 0;
    private NavMeshAgent agent; 
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Kontrol için Dusman scriptine referans alalım (Opsiyonel ama iyi bir pratik)
        Dusman dusman = GetComponent<Dusman>();

        if(noktalar.Length > 0)
        {
            agent.isStopped = false; 
            
            // Hız ayarını Patrol'den kaldırdık, çünkü Dusman.cs zaten temelHiz'i (yürüme) ayarlıyor.
            // agent.speed = 2f; // Bu satır çıkarıldı!
            
            // Devriye başladığında yürüme animasyonunu başlat
            if (anim != null)
            {
                // Yürümeyi başlatmak için "yuruyor" veya "yavas" bool'unuzu kullanın.
                // Eski Dusman.cs'de yavas bool'u kullanıldığı için, onu kullanmaya devam edelim.
                anim.SetBool("yavas", true); 
                // Eğer koşma animasyonu "yuruyor" ise, burada "yuruyor" true, kovalama sırasında "kosuyor" true olmalı.
            }
            
            agent.SetDestination(noktalar[mevcutNokta].position);
        }
    }

    void Update()
    {
        if(!enabled) return;
        if(noktalar.Length == 0) return;

        // Noktaya ulaşıldı mı kontrol
        if(agent.remainingDistance < 0.5f && !agent.pathPending)
        {
            MevcutNoktayaGit();
        }

        // Animasyon Kontrolü:
        // Eğer ajan hareket etmiyorsa (hedefe varmışsa), yürüme animasyonunu durdur.
        if (anim != null)
        {
            if (agent.velocity.sqrMagnitude < 0.01f) // Çok yavaşsa veya durduysa
            {
                anim.SetBool("yavas", false);
                // Ek olarak, Durma animasyonu için "yuruyor" bool'unuz varsa onu da false yapın.
                // anim.SetBool("yuruyor", false); 
            }
            else
            {
                anim.SetBool("yavas", true);
                // anim.SetBool("yuruyor", true); 
            }
        }
    }

    void MevcutNoktayaGit()
    {
        if(noktalar.Length == 0) return;
        mevcutNokta = (mevcutNokta + 1) % noktalar.Length;
        agent.SetDestination(noktalar[mevcutNokta].position);
    }
}