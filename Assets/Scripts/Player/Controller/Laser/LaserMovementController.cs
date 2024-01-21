using UnityEngine;

public class LaserMovementController : MonoBehaviour
{
    [SerializeField] 
    private float laserDisappearYPosition;
    
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
            if (transform.position.y > laserDisappearYPosition)
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
