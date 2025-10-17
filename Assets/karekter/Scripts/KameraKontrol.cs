using UnityEngine;

public class KameraKontrol : MonoBehaviour
{
    public Transform hedef;
    public Vector3 hedefMesafe;
    [SerializeField]
    private float FareHassasiyeti = 2f;
    float fareX, fareY;

    Vector3 objRot;
    public Transform KarakterVucut;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

    }

    void LateUpdate()
    {
        if (hedef == null) return;


        transform.position = Vector3.Lerp(transform.position, hedef.position + hedefMesafe, Time.deltaTime * 10f);


        fareX += Input.GetAxis("Mouse X") * FareHassasiyeti;
        fareY -= Input.GetAxis("Mouse Y") * FareHassasiyeti;


        fareY = Mathf.Clamp(fareY, -40f, 25f);


        transform.rotation = Quaternion.Euler(fareY, fareX, 0f);
        hedef.rotation = Quaternion.Euler(0f, fareX, 0f);


        if (KarakterVucut != null)
        {
            float bodyTiltX = fareY + 10f;

            bodyTiltX = Mathf.Clamp(bodyTiltX, -40f, 45f);
            KarakterVucut.rotation = Quaternion.Euler(bodyTiltX, fareX, 0f);
        }
    }

}
