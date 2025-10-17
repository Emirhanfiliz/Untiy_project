using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    private Animator anim;
    private float karakterHiz = 5f;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        Hareket();
    }

    void Hareket()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", yatay);
        anim.SetFloat("Vertical", dikey);

        // Karakterin hareket yönü
        Vector3 hareket = new Vector3(yatay, 0, dikey);

        // Normalize edip hızla çarpıyoruz
        this.gameObject.transform.Translate(yatay * karakterHiz * Time.deltaTime, 0, dikey * karakterHiz * Time.deltaTime);

        // Karakter dönsün (eğer hareket ediyorsa)
        if (hareket != Vector3.zero)
        {
            transform.forward = hareket;
        }
    }
}
