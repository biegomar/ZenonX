using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies.MovementStrategies;
using Assets.Scripts.Enemies;
using UnityEngine;

public class EnemyWaveOneLaserMovementController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.rigidBody.velocity = Vector2.down * GameManager.Instance.EnemyWaveOneLaserSpeed;
    }

    // Update is called once per frame
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
            this.rigidBody.velocity = Vector2.zero;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionObject = collision.gameObject;
        switch (collisionObject.tag)
        {
            case "SpaceShip":
                {
                    GameManager.Instance.ActualShipHealth -= 1;

                    Destroy(gameObject);
                    break;
                }
            case "SpaceShipShield":
            {
                GameManager.Instance.ActualShieldHealth -= 1;

                Destroy(gameObject);
                break;
            }
        }
    }
}
