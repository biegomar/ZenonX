using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Controller.Waves;
using UnityEngine;

public class EnemyWaveOneSpawnController : BaseWaveSpawnController
{
    [SerializeField]
    private GameObject EnemyTemplate;

    [SerializeField]
    private GameObject LootTemplate;

    public IDictionary<Guid, IList<EnemyFlightFormationItem>> EnemyFlightFormation;
    public IDictionary<int, EnemyItem> Enemies;

    private float WaveTimer;
    private bool IsFirstFormationReleased;
    private bool IsSecondFormationReleased;
    private bool IsThirdFormationReleased;

    private void Start()
    {
        this.IsWaveSpawned = false;
        this.IsWaveCompleted = false;

        EnemyFlightFormation = new Dictionary<Guid, IList<EnemyFlightFormationItem>>();
        Enemies = new Dictionary<int, EnemyItem>();
    }    

    public override void SpawnWave()
    {
        WaveTimer += Time.deltaTime;

        if (!IsFirstFormationReleased && WaveTimer > 1f && WaveTimer < 1.1f)
        {
            this.SpawnWave(-7f, -3f);
            IsFirstFormationReleased = true;
        }

        if (!IsSecondFormationReleased && WaveTimer > 2f && WaveTimer < 2.1f)
        {
            this.SpawnWave(-2f, 2f);
            IsSecondFormationReleased = true;
        }

        if (!IsThirdFormationReleased && WaveTimer > 3f && WaveTimer < 3.1f)
        {
            this.SpawnWave(3f, 7f);
            IsThirdFormationReleased = true;

            this.IsWaveSpawned = true;
        }
    }

    public override void SpawnLoot(Vector3 lastPosition)
    {
        var deadWaves = new List<Guid>();
        foreach (var wave in EnemyFlightFormation)
        {
            if (!wave.Value.Any())
            {
                deadWaves.Add(wave.Key);
                Instantiate(LootTemplate, lastPosition, Quaternion.identity);
            }
        }

        this.RemoveDeadWaveFromDictionary(deadWaves);
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

        this.EnemyFlightFormation.Add(waveId, gameObjects);
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
            Health = GameManager.Instance.EnemyWaveOneHealth,
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
