using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject cam;
	public GameObject subject;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        yaw += Input.GetAxis("Mouse X");
        pitch -= Input.GetAxis("Mouse Y");
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        cam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        RaycastHit hit;
        if (Physics.Raycast(subject.transform.position+Vector3.up*1.5f, -cam.transform.forward, out hit, 3)) {
            cam.transform.position = hit.point;
        } else {
            cam.transform.position = (subject.transform.position+Vector3.up*1.5f) + (-cam.transform.forward)*3;
        }
        //cam.transform.position = subject.transform.position + (Vector3.right*.75f) + (Vector3.up*1.75f) + (Vector3.back*3.0f);
    }
}
