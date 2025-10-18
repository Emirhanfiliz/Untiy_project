using System.Collections;
using UnityEngine;

public class Dusman : MonoBehaviour
{
    public float zombiHP = 100;
    Animator zombiAnim;
    bool zombiOlu;

    void Start()
    {
        zombiAnim = this.GetComponent<Animator>();
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