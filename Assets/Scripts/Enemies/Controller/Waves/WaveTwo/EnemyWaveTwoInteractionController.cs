using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyWaveTwoInteractionController : MonoBehaviour
{
    private const float rayLength = 9.7f;

    private RaycastHit2D hit;
    private Rigidbody2D rigidBody;

    private bool isInCollisionHanding = false;

    private void Start()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameRunning)
        {
            hit = Physics2D.Raycast(transform.position + Vector3.down * .3f, Vector3.down, rayLength);

            LetTheHammerFall();
            RayDebugOutput();
        }            
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInCollisionHanding)
        {
            var collisionObject = collision.gameObject;
            switch (collisionObject.tag)
            {
                case "SpaceShip":
                    {
                        isInCollisionHanding = true;
                        GameManager.Instance.ActualShipHealth -= 5;

                        Destroy(gameObject);
                        break;
                    }
            }
        }        
    }

    private void LetTheHammerFall()
    {
        if (hit.collider != null && hit.collider.gameObject.tag == "SpaceShip")
        {
            this.rigidBody.gravityScale = 2;
        }
    }

    private void RayDebugOutput()
    {
        Color debugColor = hit.collider != null ? Color.green : Color.red;
        Debug.DrawRay(transform.position + Vector3.down * .3f, Vector2.down * rayLength, debugColor, 0.01f, true);
    }
}
