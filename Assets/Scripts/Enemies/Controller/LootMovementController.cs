using System.Collections;
using Player.Services;
using UnityEngine;

namespace Enemies.Controller
{
    /// <summary>
    /// The controller for the loot movement.
    /// </summary>
    public class LootMovementController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D Rigidbody;

        private IncentiveService _incentiveService;
        private bool isInCollisionHandling = false;

        void Start()
        {
            this._incentiveService = new IncentiveService();
            Rigidbody.AddForce(new Vector2(transform.position.x > 0.0f ? -0.3f : 0.3f, 1f) * GameManager.Instance.ShipBoosterVelocity, ForceMode2D.Impulse);
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
            else
            {
                this.Rigidbody.velocity = Vector2.zero;
                this.Rigidbody.gravityScale = 0;
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isInCollisionHandling)
            {
                var collisionObject = collision.gameObject;
                if (collisionObject.CompareTag("Player"))
                {
                    AudioManager.Instance.GetSound("Loot").Play();
                    isInCollisionHandling = true;
                    _incentiveService.GiveIncentive();

                    Destroy(gameObject);
                }
            }
        }
    }
}
