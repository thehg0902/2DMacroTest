using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb2d;
    public float maxDist;
    public float minDist;
    public float speed;
    
    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        follow();
    }
    void follow()
    {
        float dist = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (dist>minDist && dist<maxDist)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, player.transform.position, speed);
        }
        else
        {
            return;
        }
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, maxDist);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, minDist);
    }

}
