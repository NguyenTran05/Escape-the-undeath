using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingZone : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float movingspeed;
    [SerializeField] private float distanceBetween;
    


    private float distance;



    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 diretion = player.transform.position - transform.position;
        diretion.Normalize();
        float angle = Mathf.Atan2(diretion.y, diretion.x) * Mathf.Rad2Deg;
       

        if (distance < distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, movingspeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            Rotation();
            
        }

    }
    private void Rotation()
    {

        Quaternion localRotation = transform.localRotation;
        localRotation.z *= 0;
        transform.localRotation = localRotation;
    }
}
