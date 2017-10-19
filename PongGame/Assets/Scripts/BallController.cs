using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    private Rigidbody2D rigidbody2d;
   
	void Start () {
        int number = Random.Range(0, 2);
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>(); 
        if (number == 1)
        {
            rigidbody2d.AddForce(new Vector2(100, 0));
        }
        else
        {
            rigidbody2d.AddForce(new Vector2(-100, 0));
        }
	}

    void Update()
    {
        //检测小球速度，如果小球x轴方向速度降低到9以下，则恢复至初速度10
        Vector2 velocity = rigidbody2d.velocity;
        if (velocity.x < 9 && velocity.x > -9 && velocity.x!=0)
        {
            if(velocity.x>0)
            {
                velocity.x = 10;
            }
            else
            {
                velocity.x = -10;
            }
        }
        rigidbody2d.velocity = velocity; 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Player")
        {
            Vector2 velocity = rigidbody2d.velocity;
            velocity =velocity/2+ collision.rigidbody.velocity/2;
            rigidbody2d.velocity = velocity;
        }
        if(collision.gameObject.name=="rightWall"|| collision.gameObject.name == "leftWall")
        {
            GameManager.instance.ChangeScore(collision.gameObject.name);
        }
        if (collision.transform.tag == "Player")
        {
            MusicControlle.instance.PlayClick();
        }
        else if (collision.transform.tag == "Wall")
        {
            MusicControlle.instance.PlayHit();
        }
    }

    public void Reset()
    {
        //将小球重置到世界原点
        transform.position = Vector3.zero;
        //随机将小球射向左右两个方向
        int number = Random.Range(0, 2);
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        if (number == 1)
        {
            rigidbody2d.AddForce(new Vector2(100, 0));
        }
        else
        {
            rigidbody2d.AddForce(new Vector2(-100, 0));
        }
    }

}
