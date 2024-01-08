using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovementController : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    public void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.up * GameManager.Instance.ShipLaserSpeed;
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
        else
        {
            rigidBody.velocity = Vector2.zero;
        }
    }
}
