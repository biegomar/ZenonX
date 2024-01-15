using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private List<BaseWaveSpawnController> waveControllers;

    private BaseWaveSpawnController actualWave;

    private int waveIndex = 1;
    private bool allWavesSpawned = false;

    private void Start()
    {
        this.actualWave = waveControllers.FirstOrDefault();        
    }

    private void Update()
    {
        if (this.actualWave != null && !allWavesSpawned)
        {
            if (!this.actualWave.IsWaveSpawned)
            {
                this.actualWave.SpawnWave();
            }

            if (this.actualWave.IsWaveCompleted)
            {
                if (waveIndex == waveControllers.Count())
                {
                    this.allWavesSpawned = true;
                }
                else
                {
                    this.actualWave = waveControllers[waveIndex];
                    this.actualWave.ResetWave();
                    waveIndex++;
                }                
            }
        }
    }    
}
