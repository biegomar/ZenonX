using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using UnityEngine;

public class EnemyWaveOneFireController : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;

    [SerializeField]
    private AudioSource laserSound;
    
    private float fireInterval;
    private float elapsedTimeSinceLastShot;

    void Start()
    {
        this.fireInterval = Random.Range(2f, 6f);
        this.elapsedTimeSinceLastShot = this.fireInterval - (this.fireInterval * .5f);
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
