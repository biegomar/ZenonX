using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * GameManager.ShipLaserSpeed;    
    }

    public void Update()
    {
        if (transform.position.y > 7)
        {            
            Destroy(gameObject);
        }        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {        
        var collisionObject = collision.gameObject;
        if (collisionObject.tag == "Enemy")
        {
            Destroy(collisionObject);
        }
    }
}
