using UnityEngine;
using System.Collections;

public class FirstPersonController : MonoBehaviour
{

    public float movementspeed = 8.0f;
    public float mousespeed = 5.0f;
    public float updownRange = 40.0f;
    float verticalRotation = 0;

    public LayerMask playerLayer;
    public LayerMask safeZoneLayer;
    public GameObject girl;

    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = (true);


    }

    // Update is called once per frame
    void Update()
    {
        //Rotation
        float siderotate = Input.GetAxis("Mouse X") * mousespeed;
        verticalRotation -= Input.GetAxis("Mouse Y") * mousespeed;

        //float currentupdown = Camera.main.transform.rotation.eulerAngles.x;

        verticalRotation = Mathf.Clamp(verticalRotation, -updownRange, updownRange);
        //Blickrichtung nach oben und unten wird nur durch neigung der Camera relaisiert
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        //Blickrichtung zur Seite dreht das ganze Spielermodell
        transform.Rotate(0, siderotate, 0);

        //Bewegung
        float forwardSpeed = Input.GetAxis("Vertical") * movementspeed;
        float sidespeed = Input.GetAxis("Horizontal") * movementspeed;
        Vector3 speed = new Vector3(sidespeed, 0, forwardSpeed);

        speed = transform.rotation * speed; //Vector an Playerachsen ausrichten
        CharacterController cc = GetComponent<CharacterController>();
        cc.SimpleMove(speed);


    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name == "SafeZone")
        {
            Debug.Log("Player ENTERED Trigger");
            AI SN = girl.GetComponent<AI>();
            SN.safeZoneEntered = true;
        }
        else
            Debug.Log("Something else triggered");
    }

    void OnTriggerExit(Collider c)
    {
        Debug.Log("Player LEFT Trigger");
        AI SN = girl.GetComponent<AI>();
        SN.safeZoneEntered = false;
    }
}