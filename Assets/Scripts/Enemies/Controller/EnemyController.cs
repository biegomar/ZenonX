using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemies;
using Assets.Scripts.Enemies.Controller.Waves;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<BaseWaveSpawnController> waveControllers;

    private BaseWaveSpawnController actualWave;

    private void Start()
    {
        this.actualWave = this.waveControllers.FirstOrDefault();
    }

    private void Update()
    {
        if (this.actualWave != null && !this.actualWave.IsWaveSpawned) 
        { 
            this.actualWave.SpawnWave();
        }
    }
}
