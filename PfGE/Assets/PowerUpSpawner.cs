using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
	public int spawnTimer = 1;
	public bool powerUpSpawned = false;

	public PowerUp.powerUpType type;

	private Transform tf;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer > 0 && !powerUpSpawned) {
			spawnTimer--;
		} else if (spawnTimer <= 0 && !powerUpSpawned) {
			SpawnNewPowerup ();
		}
	}

	void SpawnNewPowerup () {
		GameObject p = Instantiate (GameManager.instance.powerUps[(int) type], tf);
		p.GetComponent<PowerUp>().spawner = gameObject;
		p.GetComponent<PowerUp>().type = type;
		powerUpSpawned = true;
	}
}
