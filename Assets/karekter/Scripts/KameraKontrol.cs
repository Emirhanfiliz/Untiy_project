using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public Transform hedef;
    public Vector3 hedefMesafe;
    public float FareHassasiyeti;
    float fareX, fareY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    {

    }

    void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, hedef.position + hedefMesafe, Time.deltaTime * 10);
        fareX += Input.GetAxis("Mouse X");
        fareY += Input.GetAxis("Mouse Y");
        this.transform.eulerAngles = new Vector3(fareY, fareX, 0);
        hedef.transform.eulerAngles = new Vector3(0, fareX, 0);
    }

}
