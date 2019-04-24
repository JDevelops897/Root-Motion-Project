using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Pawn pawn;
    public GameObject cam;

    private float xMove = 0.0f;
    private float zMove = 0.0f;

    private float slowRate = 30f;

    private float walkSpeed = 6.5f;
    private float sprintSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Mathf.MoveTowards(xMove, 0, slowRate * Time.deltaTime);
        zMove = Mathf.MoveTowards(zMove, 0, slowRate * Time.deltaTime);

        xMove += Input.GetAxis("Horizontal");
        zMove += Input.GetAxis("Vertical");


        float spd = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) {
            spd = sprintSpeed;
        }
        xMove = Mathf.Clamp(xMove, -spd, spd);
        zMove = Mathf.Clamp(zMove, -spd, spd);

        Vector3 movement = new Vector3(xMove/10f, 0.0f, zMove/10f);
        
        pawn.Move(movement);

        pawn.transform.eulerAngles = new Vector3(pawn.transform.eulerAngles.x, cam.transform.eulerAngles.y, pawn.transform.eulerAngles.z);
    }
}
