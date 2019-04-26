using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Pawn pawn;
    public GameObject cam;

    public Slider healthBarUI;
    public GameObject ammoUI;
    public Text gunNameText;
    public Text ammoCountText;
    public Image gunImage;
    public List<RectTransform> crosshairUI;

    public float crosshairThickness = 2f;
    public float crosshairDistance = 10f;

    public float jumpPower = 5f;

    private float xMove = 0.0f;
    private float zMove = 0.0f;

    private float slowRate = 30f;

    private const float walkSpeed = 6.5f;
    private const float sprintSpeed = 10f;
    private float spd;
    public bool isGrounded = true;

    private WeaponAgent weaponAgent;

    public GameObject ex;

    // Start is called before the first frame update
    void Start()
    {
        pawn.healthBarUI = healthBarUI;
        weaponAgent = pawn.GetComponent<WeaponAgent>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (!pawn) return;
    	isGrounded = pawn.isGrounded;
    	//slow down the movement at a specific rate
        xMove = Mathf.MoveTowards(xMove, 0, slowRate * Time.deltaTime);
        zMove = Mathf.MoveTowards(zMove, 0, slowRate * Time.deltaTime);

        //add the horizontal and vertical axis movement to the movement variables
        xMove += Input.GetAxis("Horizontal");
        zMove += Input.GetAxis("Vertical");

        //set the speed to either walk or sprint speed
        spd = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) {
            spd = sprintSpeed;
        }

        //clamp the speed 
        xMove = Mathf.Clamp(xMove, -spd, spd);
        zMove = Mathf.Clamp(zMove, -spd, spd);

        if (xMove == 0 && zMove == 0 && isGrounded) {
        	crosshairUI[0].anchoredPosition = new Vector3(-1*crosshairDistance, 0, 0);
        	crosshairUI[1].anchoredPosition = new Vector3(crosshairDistance, 0, 0); 
        	crosshairUI[2].anchoredPosition = new Vector3(0, crosshairDistance, 0); 
        	crosshairUI[3].anchoredPosition = new Vector3(0, -1*crosshairDistance, 0); 
        } else {
        	float spread = Mathf.Abs(Vector2.Distance(new Vector2(0f, 0f), new Vector2(xMove, zMove)));
        	crosshairUI[0].anchoredPosition = new Vector3(-1*(crosshairDistance+spread), 0, 0);
        	crosshairUI[1].anchoredPosition = new Vector3((crosshairDistance+spread), 0, 0); 
        	crosshairUI[2].anchoredPosition = new Vector3(0, (crosshairDistance+spread), 0); 
        	crosshairUI[3].anchoredPosition = new Vector3(0, -1*(crosshairDistance+spread), 0); 
        }

        ammoUI.SetActive((weaponAgent.equippedWeapon != null));
        if (weaponAgent.equippedWeapon) {
        	if (weaponAgent.equippedWeapon.reloading) {
        			ammoCountText.text = "Reloading...";
        		} else {
        			ammoCountText.text = weaponAgent.equippedWeapon.ammo.ToString() + "/" + weaponAgent.equippedWeapon.maxAmmo.ToString();
        		}
        	gunImage.sprite = weaponAgent.equippedWeapon.gunImage;
	        gunNameText.text = weaponAgent.equippedWeapon.gunName;
	    }

        if (Input.GetKey(KeyCode.Delete)) {
        	pawn.TakeDamage(0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
        	weaponAgent.Equip(GameManager.WeaponType.M4A1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
        	weaponAgent.Equip(GameManager.WeaponType.RPG);
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
        	weaponAgent.UnEquip();
        }

        if (weaponAgent.equippedWeaponObject != null && spd != sprintSpeed) {
           	weaponAgent.equippedWeaponObject.GetComponent<AlignRotation>().Align();
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) {
        	if (weaponAgent.equippedWeapon != null) {
	        	weaponAgent.equippedWeapon.PullTrigger();		
	        }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
        	if (weaponAgent.equippedWeapon != null) {
        		if (weaponAgent.equippedWeapon.ammo != weaponAgent.equippedWeapon.maxAmmo)
		        	weaponAgent.equippedWeapon.Reload();		
	        }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.E)) {
        	if (weaponAgent.equippedWeapon != null) {
	        	weaponAgent.equippedWeapon.ReleaseTrigger();
        	}
        }

        //move the pawn by the movespeed/10
        if (pawn) {
	        pawn.Move(new Vector3(xMove/10f, 0.0f, zMove/10f));
	        //rotate the pawn to face the direction the camera is facing
	        pawn.transform.eulerAngles = new Vector3(pawn.transform.eulerAngles.x, cam.transform.eulerAngles.y, pawn.transform.eulerAngles.z);
	    }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
        	if (pawn) {
	        	pawn.transform.position += Vector3.up*.45f;
	        	pawn.GetComponent<Rigidbody>().velocity += (new Vector3(xMove, jumpPower, zMove));
	        }
	    }
    }
}
