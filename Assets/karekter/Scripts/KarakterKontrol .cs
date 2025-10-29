using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float karakterHiz = 2f;
    [SerializeField] private float kosmaCarpani = 2f;
    private float saglik = 100;
    bool hayattaMi;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        hayattaMi = true;
    }

    void Update()
    {

        if (saglik <= 0)
        {
            hayattaMi = false;
            anim.SetBool("yasiyorMu", hayattaMi);

        }
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
    public bool YasiyorMu()
    {
        return hayattaMi;
    }
    public void HasarAl()
    {
        saglik -= Random.Range(5, 15);

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
        transform.Translate(hareket, Space.Self);


        this.gameObject.transform.Translate(hareket);
        if (hareket != Vector3.zero)
        {
            transform.forward = hareket;
        }
    }
}
