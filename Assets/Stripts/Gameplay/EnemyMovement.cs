using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 2f;
    [SerializeField] private GameObject[] wayPoints;

    private Animator anim;
    private int curWayPointIndex = 0;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(wayPoints[curWayPointIndex].transform.position, transform.position) < 0.1f)
        {          
            flip();
            curWayPointIndex++;
            if (curWayPointIndex >= wayPoints.Length)
            {
                curWayPointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[curWayPointIndex].transform.position, movingSpeed * Time.deltaTime);
        anim.SetBool("Moving", true);
        
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

    }
    
}
