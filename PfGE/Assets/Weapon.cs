using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public GameManager.WeaponType type;

	public bool automatic;
	public float fireRate;

	public string gunName;
	public Sprite gunImage;
	public int ammo;
	public int maxAmmo;
	public int accuracy;

	public int reloadTime;
	private int reloadTimer;

	public bool reloading;
	private bool firing;

	private float fireTimer;

	public GameObject bullet;

	public Transform bulletPoint;
	public Transform leftHandIK;
	public Transform rightHandIK;

	public GameObject gunObject;

	public Renderer r;

	public GameObject pawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    	if (GameManager.instance.paused) return;
        if (reloading) {
        	reloadTimer--;
        	if (reloadTimer < 0) {
        		reloading = false;
        		ammo = maxAmmo;
        	}
        }
    	if (type == GameManager.WeaponType.RPG) {
			if (ammo <= 0) {
				r.enabled = false;
			} else {
				r.enabled = true;
			}
    	}
    	if (firing) {
    		fireTimer -= Time.deltaTime;
    		if (fireTimer < 0) {
    			Fire();
    			fireTimer = fireRate/60;
    		}
    	}
    }

    public void Reload() {
    	if (reloading) return;
    	reloading = true;
    	reloadTimer = reloadTime;
    }

    public void Fire() {
    	if (ammo > 0 && !reloading) {
    		ammo--;
    		GameObject b;
    		b = Instantiate(bullet, bulletPoint.position, gunObject.transform.rotation);
    		b.GetComponent<Projectile>().sender = pawn;
    	} else {
    		Reload();
    	}
    }

    public void PullTrigger() {
    	if (automatic) {
    		firing = true;
    		fireTimer = fireRate/60;
		} else {
			Fire();
		}
    }

    public void ReleaseTrigger() {
    	firing = false;
    }
}
