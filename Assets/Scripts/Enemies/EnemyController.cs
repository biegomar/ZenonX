using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyTemplate;

    private float WaveTimer;
    private bool IsFirstWaveReleased;

    public void Start()
    {
        WaveTimer = 0;
        IsFirstWaveReleased = false;
    }

    public void Update()
    {
        WaveTimer += Time.deltaTime;

        if (!IsFirstWaveReleased && WaveTimer > 1f && WaveTimer < 1.1f) 
        {
            this.SpawnFirstWave();
        }        
    }

    private void SpawnFirstWave()
    {
        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x,
               transform.position.y,
               transform.position.z), Quaternion.identity);

        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x,
               transform.position.y + 0.6f,
               transform.position.z), Quaternion.identity);

        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x,
               transform.position.y + 1.2f,
               transform.position.z), Quaternion.identity);

        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x,
               transform.position.y + 1.8f,
               transform.position.z), Quaternion.identity);

        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x,
               transform.position.y + 2.4f,
               transform.position.z), Quaternion.identity);

        IsFirstWaveReleased = true;
    }
}
