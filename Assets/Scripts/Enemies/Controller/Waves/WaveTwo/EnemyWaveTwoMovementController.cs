using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTwoMovementController : MonoBehaviour
{   
    void Update()
    {
        if (GameManager.Instance.IsGameRunning)
        {
            if (transform.position.y < -7)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            var rigidBody = GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = Vector2.zero;
            }
            
        }
    }
}
