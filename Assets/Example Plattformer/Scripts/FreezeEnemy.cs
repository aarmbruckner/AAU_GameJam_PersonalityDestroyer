using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEnemy : Enemy
{
    float currentRotationAngle = 0;

    private void Awake()
    {
        initalizeObject();


    }

    private void FixedUpdate()
    {
        handleUpdate();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            freezePlayer();
        }
    }

    void freezePlayer()
    {
        GameObject player = GameObject.Find("hero");
        PlayerControl playerControl = (PlayerControl)player.GetComponent(typeof(PlayerControl));
        playerControl.Freeze(300);
        //col.gameObject.GetComponent<PlayerControl>().Freeze(300);

        //Death();
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D col in cols)
        {
            col.isTrigger = true;
        }
        Destroy(this.gameObject);
    }

    override protected void handleCollision(Collider2D c)
    {
        // If any of the colliders is an Obstacle...
        if (c.tag == "Obstacle")
        {
            // if hits obstacle, it disappears
            OutOfBounce();

        }
        if (c.tag == "Player")
        {
            freezePlayer();


        }
    }

    override  protected void changeFixedUpdateVelocity(float currentSpeed)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
    }
}
