using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5f;                //max Health
    public float health = 5f;                   // The player's health.
    public float repeatDamagePeriod = 2f;       // How frequently the player can be damaged.
    public AudioClip[] ouchClips;               // Array of clips to play when the player is damaged.
    public float hurtForce = 10f;               // The force with which the player is pushed when hurt.
    public float damageAmount = 1f;         // The amount of damage to take when enemies touch the player

    private SpriteRenderer healthBar;           // Reference to the sprite renderer of the health bar.
    private float lastHitTime;                  // The time at which the player was last hit.
    private Vector3 healthScale;                // The local scale of the health bar initially (with full health).
    private PlayerControl playerControl;        // Reference to the PlayerControl script.
    private Animator anim;						// Reference to the Animator on the player
    private string [] animatorControllerNames  = {"Assets/Example Plattformer/Animation/Controllers/CharacterL4.controller","Assets/Example Plattformer/Animation/Controllers/CharacterL3.controller"};
    private int activeAnimatorControllerIndex = 0;

	void Awake ()
	{
		// Setting up references.
		playerControl = GetComponent<PlayerControl>();
		healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		// Getting the intial scale of the healthbar (whilst the player has full health).
		healthScale = healthBar.transform.localScale;
	}


	void OnCollisionEnter2D (Collision2D col)
	{
     
        // If the colliding gameobject is an Enemy...
        if (col.gameObject.tag == "Enemy")
		{
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				// ... and if the player still has health...
				if(health > 0f)
				{
					// ... take damage and reset the lastHitTime.
					TakeDamage(col.transform); 
					lastHitTime = Time.time; 
				}
				// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
				else
				{
					// Find all of the colliders on the gameobject and set them all to be triggers.
					Collider2D[] cols = GetComponents<Collider2D>();
					foreach(Collider2D c in cols)
					{
						c.isTrigger = true;
					}

					// Move all sprite parts of the player to the front
					SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
					foreach(SpriteRenderer s in spr)
					{
						s.sortingLayerName = "UI";
					}

					// ... disable user Player Control script
					GetComponent<PlayerControl>().enabled = false;

					// ... disable the Gun script to stop a dead guy shooting a nonexistant bazooka
					GetComponentInChildren<Gun>().enabled = false;

					// ... Trigger the 'Die' animation state
					anim.SetTrigger("Die");

                    //UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
                    Application.LoadLevel("Lose");
                }
			}
		}
	}


	void TakeDamage (Transform enemy)
	{
       
        //Debug.Log("Leben " + health);
        // Make sure the player can't jump.
        playerControl.jump = false;

        //Hebe freeze auf jeden fall auf
        playerControl.Freeze(0);

		// Create a vector that's from the enemy to the player with an upwards boost.
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;

		// Add a force to the player in the direction of the vector and multiply by the hurtForce.
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);

		// Reduce the player's health by 10.
		health -= damageAmount;
        anim.SetFloat("Health", health);
        // Update what the health bar looks like.
        UpdateHealthBar();

		// Play a random clip of the player getting hurt.
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
	}

    private void FixedUpdate()
    {
        if (PlayerControl.freezed >= 0)
        {
            healthBar.material.color = Color.blue;
            // Set the scale of the health bar to be proportional to the player's freeze time.
            healthBar.transform.localScale = new Vector3(healthScale.x * PlayerControl.freezed / 300.0f, 1, 1);
            // show
            //gameObject.GetComponent<Renderer>().enabled = true;
        }
        //else
        // hide
        //gameObject.GetComponent<Renderer>().enabled = false;


    }

    public void UpdateHealthBar ()
	{

        if (animatorControllerNames.Length > activeAnimatorControllerIndex + 1)
        {
            //activeAnimatorControllerIndex++;

             
            //RuntimeAnimatorController controller = UnityEditor.Animations.AnimatorController.CreateAnimatorControllerAtPath("Assets/Example Plattformer/Animation/Controllers/CharacterL4.controller");

            //anim.runtimeAnimatorController = Resources.Load("Assets/Example Plattformer/Animation/Controllers/CharacterL4.controller") as RuntimeAnimatorController;
            //anim.runtimeAnimatorController = controller;
            //Debug.Log(UnityEditor.AssetDatabase.GetAssetPath(anim.runtimeAnimatorController));
            //animator.runtimeAnimatorController = Resources.Load(animatorControllerNames[activeAnimatorControllerIndex]) as RuntimeAnimatorController;
        }

        //if (PlayerControl.freezed > 0)
        //{
        //    // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        //    healthBar.material.color = Color.blue;

        //    // Set the scale of the health bar to be proportional to the player's freeze time.
        //    healthBar.transform.localScale = new Vector3(healthScale.x * PlayerControl.freezed / 300.0f, 1, 1);
        //} else
        //{
        //    // Set the health bar's colour to proportion of the way between green and red based on the player's health.
        //    healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        //    healthBar.material.color = Color.blue;
        //    // Set the scale of the health bar to be proportional to the player's health.
        //    healthBar.transform.localScale = new Vector3(healthScale.x * health / maxHealth, 1, 1);
        //}
    }
}
