using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

	public int time = 15;
	public float growSpeed = 0.1f;

	private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        Collider[] hitColliders = Physics.OverlapSphere(tf.position, 10);
        foreach(Collider c in hitColliders) {
        	if (c.gameObject.tag == "Pawn") {
        		c.gameObject.GetComponent<Pawn>().TakeDamage(10);
				c.gameObject.GetComponent<Animator>().applyRootMotion = false;
        		c.gameObject.GetComponent<Rigidbody>().velocity += ((c.gameObject.transform.position+Vector3.up*1.5f)-tf.position).normalized*10;
	        	c.gameObject.transform.position += Vector3.up*.45f;
        	}
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0) {
        	Destroy(this.gameObject);
        } else {
        	time--;
        	tf.localScale += (new Vector3(1, 1, 1))*growSpeed;
        }
    }
}
