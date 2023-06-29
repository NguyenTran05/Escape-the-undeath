using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChase : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float movingspeed;
    [SerializeField] private float distanceBetween;
    [SerializeField] private bool flip;

    
    private float distance;
    

    
    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 diretion = player.transform.position - transform.position;
        diretion.Normalize();
        float angle = Mathf.Atan2(diretion.y, diretion.x) * Mathf.Rad2Deg;
        Vector3 scale = transform.localScale;

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, movingspeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            Rotation();
            if(player.transform.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;
        }
                
    }   
    private void Rotation()
    {
        
        Quaternion localRotation = transform.localRotation;
        localRotation.z *= 0;
        transform.localRotation = localRotation;
    }
    
}
    


