using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HateSpeechSpawner : Spawner {

    void Start()
    {
        // Start calling the Spawn function repeatedly after a delay .
        InvokeRepeating("handleSpawning", spawnDelay, spawnTime);
    }

    void handleSpawning()
    {
        EnemyHateSpeech[] existintHateSpeeches = FindObjectsOfType<EnemyHateSpeech>();

        if(existintHateSpeeches == null || existintHateSpeeches.Length<=0)
         Spawn();
    }

    protected override void handleGameObjectAfterCreation(GameObject gameObject)
    {

        GameObject[] ignoreObjects;
        ignoreObjects = GameObject.FindGameObjectsWithTag("ground");

        //int groundLayerID = LayerMask.NameToLayer("Ground");
        //int flyingLayerID = LayerMask.NameToLayer("Flying");
        //Physics.IgnoreLayerCollision(groundLayerID, flyingLayerID);
    
        foreach (GameObject ignoreObject in ignoreObjects)
        {

            Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), ignoreObject.GetComponent<BoxCollider2D>());
            //   Physics.IgnoreLayerCollision(Layer)
            // Physics.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), ignoreObject.GetComponent<BoxCollider2D>());
        }

    }


}
