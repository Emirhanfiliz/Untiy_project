using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    private Animator anim;
    private float karakterHiz = 5f;
    private float saglık = 100;
    bool hayattaMi;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        hayattaMi = true;
    }

    void Update()
    {

        if (saglık <= 0)
        {
            hayattaMi = false;
            anim.SetBool("yasiyorMu", hayattaMi);

        }
        if (hayattaMi == true)
        {
            Hareket();
        }
    }
    public bool YasiyorMu()
    {
        return hayattaMi;
    }
    public void HasarAl()
    {
        saglık -= Random.Range(5, 15);

    }

    void Hareket()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", yatay);
        anim.SetFloat("Vertical", dikey);


        Vector3 hareket = new Vector3(yatay, 0, dikey);


        this.gameObject.transform.Translate(yatay * karakterHiz * Time.deltaTime, 0, dikey * karakterHiz * Time.deltaTime);


        if (hareket != Vector3.zero)
        {
            transform.forward = hareket;
        }
    }
}
