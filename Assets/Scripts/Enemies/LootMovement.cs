using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootMovement : MonoBehaviour
{
    private bool isInCollisionHandling = false;

    void Start()
    {
        var Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.AddForce(new Vector2(0.3f, 1f) * GameManager.Instance.ShipBoosterVelocity, ForceMode2D.Impulse);
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
        if (!isInCollisionHandling)
        {            
            var collisionObject = collision.gameObject;
            if (collisionObject.tag == "SpaceShip")
            {
                isInCollisionHandling = true;
                Destroy(gameObject);
                IncentiveController.GiveIncentive();
            }
        }        
    }
}
