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
    public IDictionary<Guid, bool> EnemyFlightFormationNegativeDirection;
    public IDictionary<int, EnemyFlightFormationItem> Enemies;

    private float WaveTimer;
    private bool IsFirstFormationReleased;
    private bool IsSecondFormationReleased;
    private bool IsThirdFormationReleased;
    private bool IsFourthFormationReleased;

    private void Start()
    {
        InitializeWave();
    }    

    public override void SpawnWave()
    {
        WaveTimer += Time.deltaTime;

        if (!IsFirstFormationReleased && WaveTimer > 1f && WaveTimer < 1.1f)
        {
            this.SpawnWave(-9.5f, -6.5f, false);
            IsFirstFormationReleased = true;
        }

        if (!IsSecondFormationReleased && WaveTimer > 2f && WaveTimer < 2.1f)
        {
            this.SpawnWave(-4.5f, -1.5f, UnityEngine.Random.Range(0, 2) == 0);
            IsSecondFormationReleased = true;
        }

        if (!IsThirdFormationReleased && WaveTimer > 3f && WaveTimer < 3.1f)
        {
            this.SpawnWave(0.5f, 3.5f, UnityEngine.Random.Range(0, 2) == 0);
            IsThirdFormationReleased = true;            
        }

        if (!IsFourthFormationReleased && WaveTimer > 4f && WaveTimer < 4.1f)
        {
            this.SpawnWave(6f, 9f, true);
            IsFourthFormationReleased = true;

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

    public override void ResetWave()
    {
        InitializeWave();
    }

    private void InitializeWave()
    {
        this.IsWaveSpawned = false;
        this.IsWaveCompleted = false;

        this.WaveTimer = 0f;

        this.IsFirstFormationReleased = false;
        this.IsSecondFormationReleased = false;
        this.IsThirdFormationReleased = false;
        this.IsFourthFormationReleased = false;

        this.EnemyFlightFormation = new Dictionary<Guid, IList<EnemyFlightFormationItem>>();
        this.EnemyFlightFormationNegativeDirection = new Dictionary<Guid, bool>();
        this.Enemies = new Dictionary<int, EnemyFlightFormationItem>();
    }

    private Guid SpawnWave(float appearFrom, float appearTo, bool isNegativeXDirection)
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
        this.EnemyFlightFormationNegativeDirection.Add(waveId, isNegativeXDirection);

        return waveId;
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
