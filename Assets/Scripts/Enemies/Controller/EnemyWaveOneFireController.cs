using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.MovementStrategies;
using UnityEngine;

public class EnemyWaveOneFireController : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;

    private EnemyController enemyController;
    private EnemyItem enemyItem;
    private float fireInterval;
    private float elapsedTimeSinceLastShot;

    void Start()
    {
        fireInterval = Random.Range(2f, 6f);

        this.elapsedTimeSinceLastShot = fireInterval - 0.1f;

        GameObject go = GameObject.Find("EnemyWaveOne");
        if (go != null)
        {
            this.enemyController = go.GetComponent<EnemyController>();
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
        this.elapsedTimeSinceLastShot += Time.deltaTime;
        this.FeuerFrei();
    }

    private void FeuerFrei()
    {
        if (this.elapsedTimeSinceLastShot >= this.fireInterval)
        {
            Instantiate(Laser, new Vector3(
               transform.position.x,
               transform.position.y - 0.3f,
               transform.position.z), Quaternion.identity);
            
            this.elapsedTimeSinceLastShot = 0;
        }
    }

}
