using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveTwoMovementController : MonoBehaviour
{    
    private IMovementStrategy activeMovementStrategy;
    private bool isTargetPositionReached = false;

    private void Start()
    {        
        this.activeMovementStrategy = new StraightLerpMovement(transform.position);    
    }

    void Update()
    {
        // use delta time for game pause here.
        if (GameManager.Instance.IsGameRunning && Time.deltaTime > 0f)
        {
            if (!isTargetPositionReached)
            {
                transform.position = new Vector3(
                  CalculateNewXPosition(),
                  CalculateNewYPosition(),
                  transform.position.z);

                isTargetPositionReached = transform.position.y <= 5.6f;
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

    private float CalculateNewXPosition()
    {
        return this.activeMovementStrategy.CalculateNewXPosition(gameObject);
    }

    private float CalculateNewYPosition()
    {
        return this.activeMovementStrategy.CalculateNewYPosition(gameObject);
    }
}
