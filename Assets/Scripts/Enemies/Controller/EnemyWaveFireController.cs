using UnityEngine;

namespace Enemies.Controller.Waves
{
    /// <summary>
    /// The central fire controller for the enemy waves.
    /// </summary>
    public class EnemyWaveFireController : MonoBehaviour
    {
        [SerializeField]
        private GameObject Laser;
        
        private AudioSource laserSound;
    
        private float fireInterval;
        private float elapsedTimeSinceLastShot;

        void Start()
        {
            this.fireInterval = Random.Range(2f, 6f);
            this.elapsedTimeSinceLastShot = this.fireInterval - (this.fireInterval * .5f);
            this.laserSound = AudioManager.Instance.GetSound("EnemyLaser");
            this.laserSound.enabled = true;
        }

        private void Update()
        {
            if (GameManager.Instance.IsGameRunning)
            {
                this.elapsedTimeSinceLastShot += Time.deltaTime;
                this.FeuerFrei();
            }            
        }

        private void FeuerFrei()
        {
            if (this.elapsedTimeSinceLastShot >= this.fireInterval)
            {
                this.laserSound.Play();

                Instantiate(Laser, new Vector3(
                    transform.position.x,
                    transform.position.y - 0.3f,
                    Laser.transform.position.z), Quaternion.identity);
            
                this.elapsedTimeSinceLastShot = 0;
            }
        }

    }
}
