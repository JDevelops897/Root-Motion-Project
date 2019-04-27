using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignRotation : MonoBehaviour 
{
    
    public Transform target;
    private float speed = 20f;

    private void Start() 
    {
    	target = GameManager.instance.mainCamera.transform;
    }

    private void Update()
    {

    }

    public void Align(Quaternion r = default(Quaternion)) {
    	if (r == default(Quaternion)) {
    		if (target != null)
    			transform.rotation = Quaternion.Slerp (transform.rotation, target.rotation, speed * Time.deltaTime);
    	} else {
	    	transform.rotation = Quaternion.Slerp (transform.rotation, r, speed * Time.deltaTime);
	    }
    }

}