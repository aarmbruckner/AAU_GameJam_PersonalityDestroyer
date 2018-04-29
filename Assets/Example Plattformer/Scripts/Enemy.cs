using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP = 2;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
	public float deathSpinMax = 100f;           // A value to give the maximum amount of Torque when dying


    public int direction = 1;           // direction 0 is left, direction 1 is right

    private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform frontCheck;       // Reference to the position of the gameobject used for checking if something is in front.
    private Transform backCheck;
    private bool dead = false;			// Whether or not the enemy is dead.
	private Score score;				// Reference to the Score script.

	
	void Awake()
	{
        initalizeObject();
    }

    protected void initalizeObject()
    {
        // Setting up the references.
        if (transform.Find("body") != null)
            ren = transform.Find("body").GetComponent<SpriteRenderer>();
        else
            ren =  GetComponent<SpriteRenderer>();
        frontCheck = transform.Find("frontCheck").transform;
        backCheck = transform.Find("backCheck").transform;
        //score = GameObject.Find("Score").GetComponent<Score>();
    }

    private void checkCollisionToColliders(Collider2D[] colliders)
    {
        foreach (Collider2D c in colliders)
        {
            handleCollision(c);
        }
    }

    private void handleCollisions()
    {
        // Create an array of all the colliders in front of the enemy.
        Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);
        Collider2D[] backHits = Physics2D.OverlapPointAll(backCheck.position, 1);
        checkCollisionToColliders(frontHits);
        checkCollisionToColliders(backHits);

    }

    protected virtual void handleCollision(Collider2D c)
    {
        // If any of the colliders is an Obstacle...
        if (c.tag == "Obstacle" || c.tag == "Cage") 
        {
            // if hits obstacle, it disappears
            OutOfBounce();

        }
        else if (c.tag == "Player")
        {
            runFromPlayer();
        }
    }

    protected void handleUpdate()
    {
        //GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));
        // Check each of the colliders.
        handleCollisions();

        // Set the enemy's velocity to moveSpeed in the x direction.
        float x;
        if (direction == 1)
        {

            x = transform.localScale.x * -moveSpeed;
        }
        else
        {
            x = transform.localScale.x * moveSpeed;
        }

        changeFixedUpdateVelocity(x);

        // If the enemy has one hit point left and has a damagedEnemy sprite...
        if (HP == 1 && damagedEnemy != null)
            // ... set the sprite renderer's sprite to be the damagedEnemy sprite.
            ren.sprite = damagedEnemy;

        // If the enemy has zero or fewer hit points and isn't dead yet...
        if (HP <= 0 && !dead)
            // ... call the death function.
            Death();
    }

    virtual protected void changeFixedUpdateVelocity(float currentSpeed)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(currentSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

	void FixedUpdate ()
	{
        handleUpdate();
    }
	
	public void Hurt()
	{
		// Reduce the number of hit points by one.
		HP--;
	}

    protected void runFromPlayer()
    {
        Vector3 playerPos =  GameObject.Find("hero").transform.position;
        if((playerPos.x - transform.position.x) * (1-2*direction) >0)
        {
            Flip();
            //direction = 1 - direction;
        }
    
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if(col.gameObject.tag == "Player")
        {

            runFromPlayer();
        }
    }

    protected void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		foreach(SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		ren.enabled = true;
		ren.sprite = deadEnemy; //change enemy sprite to colorful

		// Increase the score by 100 points
		//score.score += 100;

		// Set dead to true.
		dead = true;


        // Allow the enemy to rotate and spin it by adding a torque.
        //GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));
        //if (direction == 0)
        //{
        //    direction = 1;
        //}
        //else
        //{
        //    direction = 0;

        //}

        runFromPlayer();
        //float x;
        //if (direction ==0)
        //{
        //    x = transform.localScale.x * -moveSpeed;

        //}
        //else
        //{
        //    x = transform.localScale.x * moveSpeed;

        //}
        //Debug.Log("enemy dead changing direction");
        //this.GetComponent<Rigidbody2D>().velocity = new Vector2(x, GetComponent<Rigidbody2D>().velocity.y);

        // Find all of the colliders on the gameobject and set them all to be triggers.
        //      Collider2D[] cols = GetComponents<Collider2D>();
        //foreach(Collider2D c in cols)
        //{
        //          //c.isTrigger = true;
        //      }

        // Play a random audioclip from the deathClips array.
        int i = Random.Range(0, deathClips.Length);
        if(deathClips.Length>i)
		    AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

		// Create a vector that is just above the enemy.
		//Vector3 scorePos;
		//scorePos = transform.position;
		//scorePos.y += 1.5f;

		//// Instantiate the 100 points prefab at this point.
		//Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
        Destroy(gameObject, 5); 
    }

	
	protected void OutOfBounce()
	{
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Disable all of them sprite renderers.
        foreach (SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		ren.enabled = true;
		ren.sprite = deadEnemy;

		// Set dead to true.
		dead = true;

		// Allow the enemy to rotate and spin it by adding a torque.
		//GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
        }
        Destroy(gameObject, 5);
    }
	

	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}
}
