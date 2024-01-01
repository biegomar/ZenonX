using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
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

    public IDictionary<Guid, IList<EnemyFlightFormationItem>> EnemyWaves;
    public IDictionary<int, EnemyItem> Enemies;

    public void Start()
    {
        EnemyWaves = new Dictionary<Guid, IList<EnemyFlightFormationItem>>();
        Enemies = new Dictionary<int, EnemyItem>();
        WaveTimer = 0;
        IsFirstWaveReleased = false;
        IsSecondWaveReleased = false;
        IsThirdWaveReleased = false;
    }

    public void Update()
    {
        this.SpawnWaveOne();
    } 
    
    private void SpawnWaveOne()
    {
        WaveTimer += Time.deltaTime;

        if (!IsFirstWaveReleased && WaveTimer > 1f && WaveTimer < 1.1f)
        {
            this.SpawnWave(-7f, -3f);
            IsFirstWaveReleased = true;
        }

        if (!IsSecondWaveReleased && WaveTimer > 2f && WaveTimer < 2.1f)
        {
            this.SpawnWave(-2f, 2f);
            IsSecondWaveReleased = true;
        }

        if (!IsThirdWaveReleased && WaveTimer > 3f && WaveTimer < 3.1f)
        {
            this.SpawnWave(3f, 7f);
            IsThirdWaveReleased = true;
        }
    }

    private void SpawnWave(float appearFrom, float appearTo)
    {
        var posX = UnityEngine.Random.Range(appearFrom, appearTo);

        var waveId = Guid.NewGuid();
        var gameObjects = new List<EnemyFlightFormationItem>();

        EnemyFlightFormationItem enemyItem = CreateNewEnemyItem(waveId, posX, 0);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, posX, 1);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, posX, 2);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, posX, 3);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, posX, 4);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        this.EnemyWaves.Add(waveId, gameObjects);
    }

    private EnemyFlightFormationItem CreateNewEnemyItem(Guid waveId, float posX, float distance)
    {
        var vector = new Vector3(
                       transform.position.x + posX,
                       transform.position.y + GameManager.Instance.EnemyWaveOneDistance * distance,
                       transform.position.z);

        return new EnemyFlightFormationItem
        {
            WaveId = waveId,
            Health = 2,
            Enemy = Instantiate(EnemyTemplate, vector, Quaternion.identity),
            StartPosition = vector
        };
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
