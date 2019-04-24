using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAgent : MonoBehaviour
{
	private Animator anim;

	public Transform attachmentPoint;

	public GameObject equippedWeaponObject;
	public Weapon equippedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip(GameManager.WeaponType weaponID) {
    	if (equippedWeaponObject != null) {
    		Destroy(equippedWeaponObject);
    	}
    	GameObject weapon = GameManager.instance.weaponPrefabs[(int) weaponID];
    	equippedWeaponObject = Instantiate(weapon);
    	equippedWeapon = equippedWeaponObject.GetComponent<Weapon>();
    	equippedWeaponObject.transform.SetParent (attachmentPoint);

    	attachmentPoint.transform.localPosition = GameManager.instance.attachmentPointOffsets[(int) weaponID];

		equippedWeaponObject.transform.localPosition = weapon.transform.localPosition;
		equippedWeaponObject.transform.localRotation = weapon.transform.localRotation; 
		anim.SetInteger("Weapon Animation Type", (int) weaponID);
    }

    public void UnEquip() {
    	if (equippedWeaponObject != null) {
    		Destroy(equippedWeaponObject);
    	}
    	equippedWeaponObject = null;
    	equippedWeapon = null;
    	anim.SetInteger("Weapon Animation Type", 0);
    }

}
