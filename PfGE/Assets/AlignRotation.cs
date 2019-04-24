using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignRotation : MonoBehaviour 
{
    
    private Transform target;
    private float speed = 10f;

    private void Start() 
    {
    	target = GameManager.instance.mainCamera.transform;
    }

    private void Update()
    {

    }

    public void Align() {
    	if (target != null)
	    	transform.rotation = Quaternion.Slerp (transform.rotation, target.rotation, speed * Time.deltaTime);
    }

}