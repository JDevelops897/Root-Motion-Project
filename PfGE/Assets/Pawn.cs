using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Pawn : MonoBehaviour
{
	private Animator anim;
	private WeaponAgent wep;
	private Rigidbody rb;
	public Slider healthBarUI;

	public float maxHealth = 100;
	public float health = 100;

	public bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
		wep = GetComponent<WeaponAgent>();
		rb = GetComponent<Rigidbody>();
    }

    void Update() {
    	isGrounded = (Physics.Raycast(transform.position+transform.up*.1f, -transform.up, .35f));
    	if (isGrounded) rb.velocity = Vector3.zero;
    	anim.applyRootMotion = isGrounded;
    }

    public void Move(Vector3 movement) {
		anim.SetFloat("Horizontal", movement.x);
		anim.SetFloat("Vertical", movement.z);
    }

    public void TakeDamage(float damage) {
    	health = Mathf.Clamp(health-damage, 0, maxHealth);
    	healthBarUI.value = health;
    	if (health == 0) {
    		Die();
    	}
    }

    public void Die() {
    	Destroy(this.gameObject);
    }

    protected virtual void OnAnimatorIK() {
		if (wep.equippedWeapon == null)
	        return;

	    if (wep.equippedWeapon.rightHandIK) {
	        anim.SetIKPosition (AvatarIKGoal.RightHand, wep.equippedWeapon.rightHandIK.position);
	        anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 1f);
	        anim.SetIKRotation (AvatarIKGoal.RightHand, wep.equippedWeapon.rightHandIK.rotation);
	        anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 1f);
	    } else {
	        anim.SetIKPositionWeight (AvatarIKGoal.RightHand, 0f);
	        anim.SetIKRotationWeight (AvatarIKGoal.RightHand, 0f);
	    } 

	   	if (wep.equippedWeapon.leftHandIK) {
	        anim.SetIKPosition (AvatarIKGoal.LeftHand, wep.equippedWeapon.leftHandIK.position);
	        anim.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1f);
	        anim.SetIKRotation (AvatarIKGoal.LeftHand, wep.equippedWeapon.leftHandIK.rotation);
	        anim.SetIKRotationWeight (AvatarIKGoal.LeftHand, 1f);
	    } else {
	        anim.SetIKPositionWeight (AvatarIKGoal.LeftHand, 0f);
	        anim.SetIKRotationWeight (AvatarIKGoal.LeftHand, 0f);
	    } 
	} 

}
