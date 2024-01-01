using UnityEngine;

public class LootMovementController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Rigidbody;

    private bool isInCollisionHandling = false;

    void Start()
    {    
        Rigidbody.AddForce(new Vector2(0.3f, 1f) * GameManager.Instance.ShipBoosterVelocity, ForceMode2D.Impulse);
    }

    public void Update()
    {
        if (GameManager.Instance.IsGameRunning)
        {
            if (transform.position.y > 30)
            {
                Destroy(gameObject);
            }
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