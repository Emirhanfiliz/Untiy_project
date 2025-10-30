using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArayüzKontrol : MonoBehaviour
{
    public TextMeshProUGUI mermiText;
     public HealthBar healthBar;

    GameObject oyuncu;

    void Start()
    {
        oyuncu = GameObject.Find("Ch48_nonPBR");

    }
    
    void Update()
    {
        mermiText.text = oyuncu.GetComponent<Fire>().GetSarjor().ToString() + "/" + oyuncu.GetComponent<Fire>().GetCephane().ToString();
        healthBar.SetHealth(oyuncu.GetComponent<KarakterKontrol>().GetSaglik());
    }

}
