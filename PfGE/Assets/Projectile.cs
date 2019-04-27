using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public GameObject ex;

	private Transform tf;

	public bool explodes;
	public float speed;
	public float damage;

	public GameObject sender;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (GameManager.instance.paused) return;
        tf.Translate((Vector3.forward*speed)*Time.deltaTime);
    }

    void OnTriggerEnter(Collider c) {
    	if (c.gameObject.tag == "Powerup" || c.gameObject == sender) {
    		return;
    	}
    	if (c.gameObject.tag == "Pawn") {
    		c.gameObject.GetComponent<Pawn>().TakeDamage(damage);
    	}
    	if (explodes) {
    		Instantiate(ex, tf.position, Quaternion.identity);
    	}
    	Destroy(this.gameObject);
    }
}
