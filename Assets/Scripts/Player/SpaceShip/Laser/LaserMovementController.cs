using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.up * GameManager.Instance.ShipLaserSpeed;
    }

    public void Update()
    {
        if (GameManager.Instance.IsGameRunning)
        {
            if (transform.position.y > 6)
            {
                Destroy(gameObject);
            }
        }        
    }
}
