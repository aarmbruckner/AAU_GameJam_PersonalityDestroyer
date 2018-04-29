using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHateSpeech : Enemy {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        handleUpdate();

    }
 

    override protected void changeFixedUpdateVelocity(float currentSpeed)
    {
      
        Vector3 playerPos = GameObject.Find("hero").transform.position;

        float horSpeed = 1.5f;
        float vertSpeed = 1.5f;

        if ((playerPos.x - transform.position.x) < 0)
        {
            horSpeed = horSpeed*-1;
        }

        if ((playerPos.y - transform.position.y) < 0)
        {
            vertSpeed = vertSpeed * -1;
        }

        if( (GetComponent<Rigidbody2D>().velocity.x  >0 && horSpeed<0) || (horSpeed >0 && GetComponent<Rigidbody2D>().velocity.x <0))
        {
            Flip();
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(horSpeed, vertSpeed);
    }

    private void reactToPlayer()
    {
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D col in cols)
        {
            col.isTrigger = true;
        }
        Destroy(this.gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            reactToPlayer();
        }
    }

    override protected void handleCollision(Collider2D c)
    {
       
        if (c.tag == "Player")
        {
            reactToPlayer();
 
        }
    }

}
