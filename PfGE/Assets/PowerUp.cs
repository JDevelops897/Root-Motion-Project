using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	public enum powerUpType {
		heal,
		ammo, 
		coin
	}
		
	public GameObject spawner;
	public powerUpType type;

	private float yPos;
	private Transform tf;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform> ();
		//randomise the rotation/bobbing
		yPos += Random.Range (0, 90);
		//set color depending on type of powerup
		switch (type) {
		case powerUpType.heal:
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.paused) return;
		//constantly rotate and bob up and down
		tf.position = new Vector3 (tf.position.x, tf.position.y + (Mathf.Sin(yPos)/100), tf.position.z);
		tf.eulerAngles = new Vector3 (tf.eulerAngles.x, Time.frameCount+yPos, tf.eulerAngles.z);
		yPos += 0.025f;
	}

	void OnTriggerEnter(Collider c) {
		if (c.gameObject.tag == "Pawn") {
			//activate the powerup of this type
			switch (type) {
			case powerUpType.heal:
				c.gameObject.GetComponent<Pawn>().TakeDamage(-25);
				break;
			case powerUpType.ammo:
				Weapon w = c.gameObject.GetComponent<WeaponAgent>().equippedWeapon;
				if (w != null) {
					w.ammo = w.ammo*5;
				}
				break;
			case powerUpType.coin:
				if (c.gameObject.GetComponent<Pawn>().isPlayer) {
					GameManager.instance.playerController.coins++;
					if (GameManager.instance.playerController.coins >= 5) {
						GameManager.instance.Win();
					}
				}				
				break;
			}
			//set the spawner timer
			if (spawner) {
				spawner.GetComponent<PowerUpSpawner>().spawnTimer = 60*10;
				spawner.GetComponent<PowerUpSpawner>().powerUpSpawned = false;
			}
			if (type == powerUpType.coin) {
				if (c.gameObject.GetComponent<Pawn>().isPlayer) {
					Destroy (this.gameObject);
				}
			} else {
				Destroy (this.gameObject);
			}
		}
	}
}