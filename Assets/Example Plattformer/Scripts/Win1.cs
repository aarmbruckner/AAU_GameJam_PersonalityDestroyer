using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win1 : MonoBehaviour {
    
    float timer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
      //  int loadedLevel = Application.loadedLevel;
      //  Debug.Log("Current Level: "+ Application.loadedLevel +" Loading Level: " + (loadedLevel+1));
        if (timer > 5)
        {
            Application.LoadLevel("Level32");

        }
    }
}
