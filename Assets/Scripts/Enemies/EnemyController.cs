using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyTemplate;

    [SerializeField]
    private GameObject LootTemplate;

    private float WaveTimer;
    private bool IsFirstWaveReleased;
    private bool IsSecondWaveReleased;
    private bool IsThirdWaveReleased;

    public IDictionary<Guid, IList<GameObject>> EnemyWaves;

    public void Start()
    {
        EnemyWaves = new Dictionary<Guid, IList<GameObject>>();
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
            this.SpawnWave(-7f, -2f);
            IsFirstWaveReleased=true;
        }

        if (!IsSecondWaveReleased && WaveTimer > 2f && WaveTimer < 2.1f)
        {
            this.SpawnWave(-2f, 3f);
            IsSecondWaveReleased=true;
        }

        if (!IsThirdWaveReleased && WaveTimer > 3f && WaveTimer < 3.1f)
        {
            this.SpawnWave(3f, 7f);
            IsThirdWaveReleased=true;
        }        
    }    

    private void SpawnWave(float appearFrom, float appearTo)
    {
        var posX = UnityEngine.Random.Range(appearFrom, appearTo);

        var id = Guid.NewGuid();
        var gameObjects = new List<GameObject>
        {
            Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y,
               transform.position.z), Quaternion.identity),


            Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y + GameManager.EnemyDistance,
               transform.position.z), Quaternion.identity),

            Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y + GameManager.EnemyDistance * 2,
               transform.position.z), Quaternion.identity),

            Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y + GameManager.EnemyDistance * 3,
               transform.position.z), Quaternion.identity),

            Instantiate(EnemyTemplate, new Vector3(
               transform.position.x + posX,
               transform.position.y + GameManager.EnemyDistance * 4,
               transform.position.z), Quaternion.identity)
        };

        this.EnemyWaves.Add(id, gameObjects);
    }

    public void SpawnLoot(Vector3 lastPosition)
    {
        var deadWaves = new List<Guid>();
        foreach (var wave in EnemyWaves)
        {                        
            if (!wave.Value.Any())
            {
                deadWaves.Add(wave.Key);
                Instantiate(LootTemplate, lastPosition, Quaternion.identity);
            }
        }

        this.RemoveDeadWaveFromDictionary(deadWaves);
    }

    private void RemoveDeadWaveFromDictionary(IEnumerable<Guid> deadWaves)
    {
        foreach (var wave in deadWaves)
        {
            this.EnemyWaves.Remove(wave);
        }
    }    
}
