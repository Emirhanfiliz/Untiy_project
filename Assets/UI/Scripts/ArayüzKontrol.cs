using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArayüzKontrol : MonoBehaviour
{
    public TextMeshProUGUI mermiText;
    public TextMeshProUGUI saglikText;

    GameObject oyuncu;

    void Start()
    {
        oyuncu = GameObject.Find("Ch48_nonPBR");

    }
    
    void Update()
    {
        mermiText.text = oyuncu.GetComponent<Fire>().GetSarjor().ToString() + "/" + oyuncu.GetComponent<Fire>().GetCephane().ToString();
        saglikText.text = "HP:" + oyuncu.GetComponent<KarakterKontrol>().GetSaglik().ToString();
    }

}
