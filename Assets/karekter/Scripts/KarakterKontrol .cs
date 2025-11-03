using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    private Animator anim;
    
    private Rigidbody rb;
    [SerializeField] private float ziplamaKuvveti = 7f; 
    [SerializeField] private bool yerdeMi = true;

    [SerializeField] private float karakterHiz = 2f;
    [SerializeField] private float kosmaCarpani = 2f;
    [SerializeField] private float maxSaglik = 100f;

    public HealthBar healthBar;
    private float saglik;
    public bool hayattaMi;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>(); 

        hayattaMi = true;
        saglik = maxSaglik;
        healthBar.GiveFullHealth(saglik);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    void Update()
    {
        if (hayattaMi == true)
        {
            Hareket();
            ZıplamaKontrol();

            bool kosuyor = Input.GetKey(KeyCode.LeftShift);
            anim.SetBool("Running", kosuyor);
        }
    }

    void ZıplamaKontrol()
    {
        if (Input.GetKeyDown(KeyCode.Space) && yerdeMi)
        {
            Zıpla();
        }
    }
    
    void Zıpla()
    {
        rb.AddForce(Vector3.up * ziplamaKuvveti, ForceMode.Impulse);
        yerdeMi = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!yerdeMi && collision.contacts.Length > 0)
        {
            if (Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0.5f) 
            {
                yerdeMi = true;
            }
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

        Vector3 hareket = new Vector3(yatay, 0, dikey).normalized * hiz * Time.deltaTime;
        
        transform.Translate(hareket, Space.Self); 
        // this.gameObject.transform.Translate(hareket); // Tekrar eden satırı kaldırdım

        if (hareket.magnitude > 0)
        {
            transform.forward = new Vector3(yatay, 0, dikey).normalized;
        }
    }
}