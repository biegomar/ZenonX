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
        //if (transform.position.y < 3)
        //{
        //    transform.position = new Vector3(
        //       transform.position.x,
        //       transform.position.y + 0.01f,
        //       transform.position.z);

        //    return;
        //}

        //Destroy(this);
    }
}
