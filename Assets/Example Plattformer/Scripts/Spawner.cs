using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public int direction = 0; 			// direction 0 is left, direction 1 is right
	public GameObject[] enemies;        // Array of enemy prefabs.
    public float enemySpeed = 20f;

    void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}

	protected void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
        GameObject enemyInstance = null;
        if (enemies.Length >0)
        {
            if (direction == 1)
            {
                // enemy that goes from right to left
                enemyInstance = Instantiate(enemies[enemyIndex], transform.position, Quaternion.Euler(new Vector3(0, 180f, 0)));
                enemyInstance.GetComponent<Enemy>().direction = 1;
                enemyInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(-enemySpeed, 0);
            }
            else
            {
                // enemy that goes from left to right
                enemyInstance = Instantiate(enemies[enemyIndex], transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                enemyInstance.GetComponent<Enemy>().direction = 0;
                enemyInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(-enemySpeed, 0);
            }
        }
        handleGameObjectAfterCreation(enemyInstance);
        //GameObject obj = (GameObject) Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
        //Debug.Log(obj.GetComponent<Rigidbody2D>().velocity);
        //if (direction == 0)
        //    enemySpeed = enemySpeed * -1;
        //obj.GetComponent<Rigidbody2D>().velocity = new Vector2(enemySpeed, 0);

        //obj.GetComponent<Rigidbody2D>().velocity  =  -2* obj.GetComponent<Rigidbody2D>().velocity;

        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}

    protected virtual void handleGameObjectAfterCreation(GameObject gameObject)
    {
        //Debug.Log("handleGameObjectAfterCreation SPAWNER");
    }
}
