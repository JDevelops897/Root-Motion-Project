using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Pawn pawn;
    public GameObject cam;

    public Slider healthBarUI;
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
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        pawn.healthBarUI = healthBarUI;
    }

    // Update is called once per frame
    void Update()
    {
    	isGrounded = Physics.Raycast(pawn.transform.position+pawn.transform.up*.1f, -pawn.transform.up, .35f);

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


        if (Input.GetKey(KeyCode.Delete)) {
        	pawn.TakeDamage(0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
        	pawn.GetComponent<WeaponAgent>().Equip(GameManager.WeaponType.M4A1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
        	pawn.GetComponent<WeaponAgent>().Equip(GameManager.WeaponType.RPG);
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
        	pawn.GetComponent<WeaponAgent>().UnEquip();
        }

        if (pawn.GetComponent<WeaponAgent>().equippedWeaponObject != null && spd != sprintSpeed) {
           	pawn.GetComponent<WeaponAgent>().equippedWeaponObject.GetComponent<AlignRotation>().Align();
        }

        //move the pawn by the movespeed/10
        pawn.Move(new Vector3(xMove/10f, 0.0f, zMove/10f));
        //rotate the pawn to face the direction the camera is facing
        pawn.transform.eulerAngles = new Vector3(pawn.transform.eulerAngles.x, cam.transform.eulerAngles.y, pawn.transform.eulerAngles.z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
        	pawn.GetComponent<Rigidbody>().velocity = Vector3.up*jumpPower;
        }
    }
}
