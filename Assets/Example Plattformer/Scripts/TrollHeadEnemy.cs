using UnityEngine;
 

public class TrollHeadEnemy : Enemy
{
    float currentRotationAngle = 0;

    private void Awake()
    {
        initalizeObject();
       // GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin, deathSpinMax));
       
       
    }

    private void FixedUpdate()  
    {
        handleUpdate();
        if (transform.localScale.x > 0)
         transform.Rotate(0, 0, 10-20*direction) ;
        else
         transform.Rotate(0, 0, -10 + 20 * direction);
    }
}