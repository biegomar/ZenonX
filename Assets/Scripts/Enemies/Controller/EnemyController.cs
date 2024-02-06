using System.Collections.Generic;
using System.Linq;
using Enemies.Controller.Waves;
using Enemies.Services.Formations;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemies.Controller
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private List<WaveSpawnController> waveControllers;

        private WaveSpawnController actualWave;
        private EnemyFormation actualFormation;

        private int waveIndex = 1;
        private int formationIndex = 1;
        
        private bool allWavesSpawned = false;
        private float timer;

        private void Start()
        {
            this.actualWave = waveControllers.FirstOrDefault();
            if (this.actualWave != null)
            {
                this.timer = this.actualWave.formationSpawnDistanceTime;
                this.actualFormation = this.actualWave.enemyFormations.FirstOrDefault();
            }
        }

        private void Update()
        {
            this.timer += Time.deltaTime;
            
            if (this.actualWave != null && !this.allWavesSpawned)
            {
                if (!this.actualWave.IsWaveSpawned)
                {
                    if (this.timer >= this.actualWave.formationSpawnDistanceTime)
                    {
                        if (this.actualFormation != null)
                        {
                            this.actualWave.SpawnFormation(this.actualFormation);
                            this.timer = 0;
                        }

                        if (this.formationIndex >= this.actualWave.enemyFormations.Count)
                        {
                            this.actualWave.IsWaveSpawned = true;
                            this.formationIndex = 1;
                        }
                        else
                        {
                            this.actualFormation = this.actualWave.enemyFormations[this.formationIndex];
                            this.formationIndex++;
                        }       
                    }
                }
                else if (this.actualWave.IsWaveCompleted)
                {
                    if (this.waveIndex >= this.waveControllers.Count)
                    {
                        this.allWavesSpawned = true;
                        this.waveIndex = 1;
                    }
                    else
                    {
                        this.actualWave = waveControllers[waveIndex];
                        if (this.actualWave != null)
                        {
                            this.timer = this.actualWave.formationSpawnDistanceTime;
                            this.actualFormation = this.actualWave.enemyFormations.FirstOrDefault();
                        }
                        waveIndex++;
                    }   
                }
            }
        }    
    }
}
