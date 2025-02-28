using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); // trovare la posizione del mouse nel gioco (x,y,z).

        Vector3 rotation = mousePos - transform.position; // sottraiamo la posizione dell'oggetto.

        float rotZ = Mathf.Atan2(rotation.y,rotation.x) * Mathf.Rad2Deg; // gradi di rotazione nella z.

        transform.rotation = Quaternion.Euler(0,0,rotZ);
    }
}
