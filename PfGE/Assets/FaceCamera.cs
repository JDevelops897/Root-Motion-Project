using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform target;

    private void Start() 
    {
    	if (GameManager.instance.mainCamera)
    		target = GameManager.instance.mainCamera.transform;
    }

    private void Update()
    {
    	if (target) 
			transform.eulerAngles = target.eulerAngles;
		else if (GameManager.instance.mainCamera)
			target = GameManager.instance.mainCamera.transform;

    }

}
