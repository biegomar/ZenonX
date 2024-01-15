using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using UnityEngine;

public class EnemyWaveTwoSpawnController : BaseWaveSpawnController
{
    [SerializeField]
    private GameObject EnemyTemplate;

    public IDictionary<Guid, IList<EnemyFlightFormationItem>> EnemyFlightFormation;
    public IDictionary<int, EnemyItem> Enemies;

    private void Start()
    {
        InitializeWave();
    }
  

    public override void SpawnWave()
    {
        var startPosition = new Vector3(-9.0f, 7.0f, 0f);        

        var waveId = Guid.NewGuid();
        var gameObjects = new List<EnemyFlightFormationItem>();

        EnemyFlightFormationItem enemyItem = CreateNewEnemyItem(waveId, startPosition, 0);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, startPosition, 1);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, startPosition, 2);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, startPosition, 3);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, startPosition, 4);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, startPosition, 5);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        enemyItem = CreateNewEnemyItem(waveId, startPosition, 6);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);

        this.EnemyFlightFormation.Add(waveId, gameObjects);

        this.IsWaveSpawned = true;
    }

    public override void SpawnLoot(Vector3 lastPosition)
    {
        var deadWaves = new List<Guid>();
        foreach (var wave in EnemyFlightFormation)
        {
            if (!wave.Value.Any())
            {
                deadWaves.Add(wave.Key);                
            }
        }

        this.RemoveDeadWaveFromDictionary(deadWaves);
    }

    public override void ResetWave()
    {
        InitializeWave();
    }

    private void InitializeWave()
    {
        this.IsWaveSpawned = false;
        this.IsWaveCompleted = false;

        EnemyFlightFormation = new Dictionary<Guid, IList<EnemyFlightFormationItem>>();
        Enemies = new Dictionary<int, EnemyItem>();
    }

    private EnemyFlightFormationItem CreateNewEnemyItem(Guid waveId, Vector3 startPosition, byte distance)
    {
        var vector = new Vector3(
                       startPosition.x + GameManager.Instance.EnemyWaveTwoDistance * distance,
                       startPosition.y,
                       startPosition.z);

        return new EnemyFlightFormationItem
        {
            WaveId = waveId,
            Health = GameManager.Instance.EnemyWaveTwoHealth,
            Enemy = Instantiate(EnemyTemplate, vector, Quaternion.identity),
            StartPosition = vector
        };
    }

    private void RemoveDeadWaveFromDictionary(IEnumerable<Guid> deadWaves)
    {
        foreach (var wave in deadWaves)
        {
            this.EnemyFlightFormation.Remove(wave);

            if (!this.EnemyFlightFormation.Any())
            {
                this.IsWaveCompleted = true;
            }
        }
    }
}
