 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public KeyCode upKey;
    public KeyCode downKey;
    public float speed = 15;
    private Rigidbody2D rigidbody2d;
	void Start () {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
	}
	
	void Update () {
	    if(Input.GetKey(upKey))
        {
            rigidbody2d.velocity = new Vector2(0, speed);
        }
        else if (Input.GetKey(downKey))
        {
            rigidbody2d.velocity = new Vector2(0, -speed);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(0, 0);
        }
	}

}
