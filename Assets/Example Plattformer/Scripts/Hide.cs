using UnityEngine;
using System.Collections;

public class Hide : MonoBehaviour
{
    public int HP = 1;                  // How many times the enemy can be hit before it dies.
    public Sprite activateHide;            // A sprite of the enemy when it's dead.
    public AudioClip[] ativate;      // An array of audioclips that can play when the enemy dies.
    public Sprite hideableSprite;			

    private bool hideable = false;			// Whether or not the enemy is dead.
    public Collider2D versteckColliderL;
    public Collider2D versteckColliderU;
    public Collider2D versteckColliderR;
    public Collider2D versteckColliderO;
    private bool isHideAlreadyUsed = false;
    private float timeHidden;

    public float smallTimeMargin = 1f;
    public string keyToHide = "h";
 
    private void OnTriggerEnter2D(Collider2D collider)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (collider.gameObject.tag == "Bullet")
        {
            HP = HP - 1;
            if (HP == 0)
            {
                hideable = true;
                Debug.Log(isHideAlreadyUsed);
                Debug.Log(true);
                renderer.enabled = true;
                renderer.sprite = hideableSprite;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(keyToHide))
        {
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            double offset = 0.6;
            Debug.Log(0.5f);
            Debug.Log(hideable + " " + isHideAlreadyUsed);

            if (hideable & !versteckColliderU.enabled & !isHideAlreadyUsed)
            {
                Debug.Log(hideable);

                Vector3 PlayerPosition = GameObject.Find("hero").transform.position;

                float ppx = PlayerPosition.x, ppy = PlayerPosition.y;
                float vx = transform.position.x, vy = transform.position.y;
                float sx = transform.localScale.x, sy = transform.localScale.y;
                Debug.Log(ppx + " " + vx + " "+ sx);
                if (ppx <= vx + sx+ offset & ppx >= vx-offset)
                {
                    if (ppy >= vy - sy - offset & ppy <= vy+ offset)
                    {
                        Debug.Log(versteckColliderU.enabled);
                        versteckColliderU.enabled = true;
                        versteckColliderO.enabled = true;
                        versteckColliderR.enabled = true;
                        versteckColliderL.enabled = true;
                        isHideAlreadyUsed = true;
                        Debug.Log("versteckt");
                        Debug.Log(versteckColliderU.enabled);
                        timeHidden = Time.time;


                        EnemyHateSpeech[] existintHateSpeeches = FindObjectsOfType<EnemyHateSpeech>();
                        foreach(EnemyHateSpeech hateSpeech in existintHateSpeeches)
                        {
                            Destroy(hateSpeech.gameObject);
                            Destroy(hateSpeech);
                            
                        }
                       
                    }
                }
               
            }
            else if (hideable & versteckColliderU.enabled & Time.time - timeHidden> smallTimeMargin)
            {
                versteckColliderU.enabled = false;
                versteckColliderO.enabled = false;
                versteckColliderR.enabled = false;
                versteckColliderL.enabled = false;
                Debug.Log("nicht mehr versteckt");
            }
        }
    }

}