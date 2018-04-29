using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalReached : MonoBehaviour {

    Animator anim;
    public String NextLevelName = "Win";

    private void OnTriggerEnter2D(Collider2D collider)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log(Application.loadedLevel);
            EndLevel();
        }
    }

    void Awake()
    {
        // Set up the reference.
        anim = GetComponent<Animator>();
    }

    private void EndLevel()
    {
        //Application.LoadLevel(Application.loadedLevel+1);
        //Debug.Log("wuiej");
        //GUI.Window;
        
        //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "test");


        //Application.LoadLevel(Application.loadedLevel);
        //gameObject.SendMessage("ApplyDamage", 5.0F);
        //anim.SetTrigger("Very Nice!! Great Success!!");
        Application.LoadLevel(NextLevelName);
        
    }
}
