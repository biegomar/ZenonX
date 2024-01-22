using UnityEngine;

namespace Enemies.Controller.Waves
{
    public class EnemyWaveOneLaserMovementController : MonoBehaviour
    {
        private Rigidbody2D rigidBody;
    
        void Start()
        {
            this.rigidBody = GetComponent<Rigidbody2D>();
            this.rigidBody.velocity = Vector2.down * GameManager.Instance.EnemyWaveLaserSpeed;
        }

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
                case "Player":
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
}
