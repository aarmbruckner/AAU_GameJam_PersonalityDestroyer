using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public int direction = 0; 			// direction 0 is left, direction 1 is right
	public GameObject[] enemies;		// Array of enemy prefabs.


	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
       
        GameObject obj = (GameObject) Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
        Debug.Log(obj.GetComponent<Rigidbody2D>().velocity);
        obj.GetComponent<Rigidbody2D>().velocity  =  - obj.GetComponent<Rigidbody2D>().velocity;

        // Play the spawning effect from all of the particle systems.
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		{
			p.Play();
		}
	}
}
