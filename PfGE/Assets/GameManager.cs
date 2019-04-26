using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public GameObject mainCamera;

	public static GameManager instance;
	
	public enum WeaponType { 
		None,
		M4A1,
		RPG
	};

	public List<GameObject> powerUps;

	public List<GameObject> weaponPrefabs;

	public List<Vector3> attachmentPointOffsets;


	//on awake make sure the manager cannot be destroyed on scene loads
	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
