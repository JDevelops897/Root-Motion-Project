using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform target;

    private void Start() 
    {
    	target = GameManager.instance.mainCamera.transform;
    }

    private void Update()
    {
		transform.eulerAngles = target.eulerAngles;
    }

}
