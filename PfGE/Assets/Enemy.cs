using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

	public float maxSpeed;
	public float slowSpeed;

	private NavMeshAgent nma;
	private Animator anim;
	private WeaponAgent weaponAgent;
	public Transform target;
	public float maxRange;
	private Vector3 desiredVelocity;
	private AlignRotation align;
	private bool firing = false;

    // Start is called before the first frame update
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        weaponAgent = GetComponent<WeaponAgent>();
        weaponAgent.Equip(GameManager.WeaponType.M4A1);
       	align = weaponAgent.equippedWeaponObject.GetComponent<AlignRotation>();
       	if (GameManager.instance.playerPawn)
	        target = GameManager.instance.playerPawn.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (GameManager.instance.paused) {
    		nma.enabled = false;
    		return; 
    	} else {
    		nma.enabled = true;
    	}
    	if (!target) {
    		if (GameManager.instance.playerPawn)
    			target = GameManager.instance.playerPawn.GetComponent<Transform>();
    		return;
    	}
    	if (nma.isOnNavMesh)
	    	nma.SetDestination(target.position);

        align.Align(Quaternion.LookRotation((target.position+Vector3.up*1.5f)-weaponAgent.equippedWeaponObject.transform.position, Vector3.up));

    	desiredVelocity = Vector3.MoveTowards(desiredVelocity, nma.desiredVelocity, nma.acceleration * Time.deltaTime);
		Vector3 input = transform.InverseTransformDirection (desiredVelocity);
		if (Vector3.Distance(transform.position, target.position) < maxRange) {
			input = input*slowSpeed; 
			weaponAgent.equippedWeaponObject.GetComponent<AlignRotation>().Align();
			if (!firing && Mathf.Floor(Random.Range(0, 120)) == 0) {
				weaponAgent.equippedWeapon.PullTrigger();
				firing = true;
			}
		} else {
			input = input*maxSpeed; 
			weaponAgent.equippedWeapon.ReleaseTrigger();
			firing = false;
		} 

		if (firing && Mathf.Floor(Random.Range(0, 120)) == 0) {
			weaponAgent.equippedWeapon.ReleaseTrigger();
			firing = false;
		}

		anim.SetFloat ("Horizontal", input.x);
		anim.SetFloat ("Vertical", input.z); 
    }

    private void OnAnimatorMove() {
    	if (GameManager.instance.paused) return;
	    nma.velocity = anim.velocity;
	}
}
