using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Controller.Waves;
using UnityEngine;

public class EnemyWaveTwoSpawnController : BaseWaveSpawnController
{
    public override void SpawnWave()
    {
        throw new System.NotImplementedException();
    }

    public void Start()
    {
        this.IsWaveSpawned = false;
        this.IsWaveCompleted = false;        
    }
}
