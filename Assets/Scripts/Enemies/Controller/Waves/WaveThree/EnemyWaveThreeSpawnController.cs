using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Controller.Waves;
using UnityEngine;

public class EnemyWaveThreeSpawnController : BaseWaveSpawnController
{
    [SerializeField]
    private GameObject EnemyTemplate;

    [SerializeField]
    private GameObject LootTemplate;
    
    public IDictionary<Guid, IList<EnemyFlightFormationItem>> EnemyFlightFormation;
    public IDictionary<int, EnemyFlightFormationItem> Enemies;

    private Vector2 startPositionLeft;
    private Vector2 startPositionRight;
    
    private float WaveTimer;
    private bool IsFirstFormationReleased;
    private bool IsSecondFormationReleased;
    private bool IsThirdFormationReleased;

    private void Start()
    {
        this.startPositionLeft = new Vector2(-9.5f, 5.5f);
        this.startPositionRight = new Vector2(9.5f, 4.5f);
        
        InitializeWave();
    } 
    
    public override void SpawnWave()
    {
        WaveTimer += Time.deltaTime;
        
        if (!this.IsFirstFormationReleased && WaveTimer > 1f && WaveTimer < 1.1f)
        {
            this.SpawnWaveInternal();
            IsFirstFormationReleased = true;
        }
        
        if (!this.IsSecondFormationReleased && WaveTimer > 2f && WaveTimer < 2.1f)
        {
            this.SpawnWaveInternal();
            IsSecondFormationReleased = true;
        }
        
        if (!this.IsThirdFormationReleased && WaveTimer > 3f && WaveTimer < 3.1f)
        {
            this.SpawnWaveInternal();
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

        this.EnemyFlightFormation = new Dictionary<Guid, IList<EnemyFlightFormationItem>>();
        this.Enemies = new Dictionary<int, EnemyFlightFormationItem>();
    }

    private void SpawnWaveInternal()
    {
        var waveId = Guid.NewGuid();
        var gameObjects = new List<EnemyFlightFormationItem>();
        var distance = 0;
        
        AddLeftFormationItem(waveId, distance, gameObjects);
        AddRightFormationItem(waveId, distance, gameObjects);
        distance += 2;
        
        AddLeftFormationItem(waveId, distance, gameObjects);
        AddRightFormationItem(waveId, distance, gameObjects);
        distance += 2;
        
        AddLeftFormationItem(waveId, distance, gameObjects);
        AddRightFormationItem(waveId, distance, gameObjects);

        this.EnemyFlightFormation.Add(waveId, gameObjects);
    }

    private void AddLeftFormationItem(Guid waveId, int distance, List<EnemyFlightFormationItem> gameObjects)
    {
        EnemyFlightFormationItem enemyItem = CreateNewEnemyItem(waveId, startPositionLeft, distance);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);
    }

    private void AddRightFormationItem(Guid waveId, int distance, List<EnemyFlightFormationItem> gameObjects)
    {
        EnemyFlightFormationItem enemyItem;
        enemyItem = CreateNewEnemyItem(waveId, startPositionRight, distance);
        gameObjects.Add(enemyItem);
        this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);
    }

    private EnemyFlightFormationItem CreateNewEnemyItem(Guid waveId, Vector2 startPosition, float distance)
    {
        var vector = new Vector3(
            startPosition.x,
            startPosition.y - distance,
            0);

        return new EnemyFlightFormationItem
        {
            WaveId = waveId,
            Health = GameManager.Instance.EnemyWaveThreeHealth,
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
