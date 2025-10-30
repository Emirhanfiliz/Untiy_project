using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float karakterHiz = 2f;
    [SerializeField] private float kosmaCarpani = 2f;
    [SerializeField] private float maxSaglik = 100f;

    public HealthBar healthBar;
    private float saglik;
    bool hayattaMi;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        hayattaMi = true;
        saglik = maxSaglik;
        healthBar.GiveFullHealth(saglik);
    }

    void Update()
    {
        if (hayattaMi == true)
        {
            Hareket();

            bool kosuyor = Input.GetKey(KeyCode.LeftShift);
            anim.SetBool("Running", kosuyor);
        }
    }

    public float GetSaglik()
    {
        return saglik;
    }
    
    public float GetMaxSaglik()
    {
        return maxSaglik;
    }

    public bool YasiyorMu()
    {
        return hayattaMi;
    }
    
    public void HasarAl()
    {
        saglik -= Random.Range(5, 15);
        
        if (saglik < 0)
        {
            saglik = 0;
        }

        healthBar.SetHealth(saglik);
        
        if (saglik <= 0 && hayattaMi)
        {
            hayattaMi = false;
            anim.SetBool("yasiyorMu", hayattaMi);
        }
    }

    void Hareket()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", yatay);
        anim.SetFloat("Vertical", dikey);

        float hiz = karakterHiz;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            hiz *= kosmaCarpani;
        }

        Vector3 hareket = new Vector3(yatay, 0, dikey) * hiz * Time.deltaTime;
        
        // Bu iki satır birbirini tekrar ediyor. İlkini (Space.Self olanı) kullanmak genellikle daha iyidir.
        // Eğer ikisini de kullanmakta ısrarcıysanız, transform.Translate(hareket, Space.Self); satırını kaldırabilirsiniz.
        transform.Translate(hareket, Space.Self); 
        this.gameObject.transform.Translate(hareket);
        

        if (hareket != Vector3.zero)
        {
            transform.forward = hareket;
        }
    }
}