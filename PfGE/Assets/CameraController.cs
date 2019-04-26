using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject cam;
	public GameObject subject;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float sensitivity = 2f;

    public float cameraHeight;
    public float cameraDistance;
    public float cameraHorizontalOffset;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
    	//keep the cursor invisible 
    	Cursor.visible = false;

    	//move the pitch and yaw based on the mouse movement
        yaw += Input.GetAxis("Mouse X")*sensitivity;
        pitch -= Input.GetAxis("Mouse Y")*sensitivity;

        //make sure the camera doesn't go upside down
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        //move the camera 
        cam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        //raycast to see if the camera is blocked, if so, place camera as far out as it can go before hitting an object
        RaycastHit hit;
        if (subject) {
	        if (Physics.Raycast((subject.transform.position+subject.transform.right*cameraHorizontalOffset)+Vector3.up*cameraHeight, -cam.transform.forward, out hit, cameraDistance)) {
	            cam.transform.position = hit.point;
	        } else {
	            cam.transform.position = ((subject.transform.position+subject.transform.right*cameraHorizontalOffset)+Vector3.up*cameraHeight) + (-cam.transform.forward)*cameraDistance;
	        }
	    }
    }
}
