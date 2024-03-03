using System.Collections.Generic;
using System.Linq;
using Enemies.Controller.Waves;
using Enemies.Services.Formations;
using JetBrains.Annotations;
using UnityEngine;

namespace Enemies.Controller
{
    /// <summary>
    /// The main enemy controller.
    /// </summary>
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private List<WaveSpawnController> waveControllers;

        private WaveSpawnController actualWave;
        private EnemyFormation actualFormation;

        private int waveIndex;
        private int formationIndex;
        
       private float timer;

        private void Start()
        {
            GetFirstWave();
        }

        private void GetFirstWave()
        {
            waveIndex = 1;
            formationIndex = 1;
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
            
            if (this.actualWave != null)
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
                    this.actualWave.waveSpawnCounter++;
                    
                    if (this.waveIndex >= this.waveControllers.Count)
                    {
                        this.waveIndex = 0;
                        GameManager.Instance.IsEnemyWaveGettingStronger = true;
                    }
                    else
                    {
                        this.actualWave = waveControllers[waveIndex];
                        if (this.actualWave != null)
                        {
                            this.actualWave.ResetWave();
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
