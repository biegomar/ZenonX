using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.MovementStrategies;
using UnityEngine;

public class EnemyWaveOneFireController : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;

    private EnemyWaveOneSpawnController enemyController;
    private EnemyItem enemyItem;
    private float fireInterval;
    private float elapsedTimeSinceLastShot;

    void Start()
    {
        this.fireInterval = Random.Range(2f, 6f);

        this.elapsedTimeSinceLastShot = this.fireInterval - (this.fireInterval * .5f);

        GameObject go = GameObject.Find("EnemyWaveOne");
        if (go != null)
        {
            this.enemyController = go.GetComponent<EnemyWaveOneSpawnController>();
            if (this.enemyController != null)
            {
                this.enemyItem = this.enemyController.Enemies[gameObject.GetInstanceID()];
            }
            else
            {
                Debug.Log("EnemyWaveOneFireController.GetComponent<EnemyController>() is null");
            }
        }
        else
        {
            Debug.Log("EnemyWaveOneFireController.Find(Enemies) is null");
        }
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
            Instantiate(Laser, new Vector3(
               transform.position.x,
               transform.position.y - 0.3f,
               Laser.transform.position.z), Quaternion.identity);
            
            this.elapsedTimeSinceLastShot = 0;
        }
    }

}
