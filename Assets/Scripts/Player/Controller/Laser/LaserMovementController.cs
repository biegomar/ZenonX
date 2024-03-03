using UnityEngine;

namespace Player.Controller.Laser
{
    /// <summary>
    /// The laser movement controller for the space ship.
    /// </summary>
    public class LaserMovementController : MonoBehaviour
    {
        [SerializeField] 
        private float laserDisappearYPosition;
        [SerializeField]
        private float laserSpeed; 
        
        private Rigidbody2D rigidBody;

        public void Start()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            rigidBody.velocity = Vector2.up * laserSpeed;
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
}
