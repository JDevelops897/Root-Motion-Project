using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public GameObject botPrefab;
	public GameObject bot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bot == null) {
        	bot = Instantiate(botPrefab, transform);
        }
    }
}
