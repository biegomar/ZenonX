using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyTemplate;

    private float WaveTimer;
    private bool IsFirstWaveReleased;
    private bool IsSecondWaveReleased;
    private bool IsThirdWaveReleased;

    public void Start()
    {
        WaveTimer = 0;
        IsFirstWaveReleased = false;
        IsSecondWaveReleased = false;
        IsThirdWaveReleased = false;
    }

    public void Update()
    {
        WaveTimer += Time.deltaTime;

        if (!IsFirstWaveReleased && WaveTimer > 1f && WaveTimer < 1.1f) 
        {
            this.SpawnWave();
            IsFirstWaveReleased=true;
        }

        if (!IsSecondWaveReleased && WaveTimer > 2f && WaveTimer < 2.1f)
        {
            this.SpawnWave();
            IsSecondWaveReleased=true;
        }

        if (!IsThirdWaveReleased && WaveTimer > 3f && WaveTimer < 3.1f)
        {
            this.SpawnWave();
            IsThirdWaveReleased=true;
        }
    }

    private void SpawnWave()
    {
        var posX = Random.Range(-7f, 7f);

        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y,
               transform.position.z), Quaternion.identity);

        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y + GameManager.EnemyDistance,
               transform.position.z), Quaternion.identity);

        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y + GameManager.EnemyDistance * 2,
               transform.position.z), Quaternion.identity);

        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y + GameManager.EnemyDistance * 3,
               transform.position.z), Quaternion.identity);

        Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y + GameManager.EnemyDistance * 4,
               transform.position.z), Quaternion.identity);        
    }
}
