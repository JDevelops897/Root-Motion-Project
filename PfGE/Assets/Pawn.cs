using System.Collections;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine;

public class Pawn : MonoBehaviour
{
	private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
    }

    public void Move(Vector3 movement) {
		anim.SetFloat("Horizontal", movement.x);
		anim.SetFloat("Vertical", movement.z);
    }
}
