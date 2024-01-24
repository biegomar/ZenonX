using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Model;
using UnityEngine;

namespace Enemies.Controller.Waves.WaveFour
{
    public class EnemyWaveFourSpawnController : BaseWaveSpawnController
    {
        private const float releaseTimespan = 0.2f;
        private float lastReleaseTimeStampForFirstFormation;
        private float lastReleaseTimeStampForSecondFormation;
        private byte numberOfReleasedItemsInFirstFormation;
        private byte numberOfReleasedItemsInSecondFormation;

        public IDictionary<Guid, IList<EnemyFlightFormationItem>> EnemyFlightFormation;
        private IList<EnemyFlightFormationItem> gameObjectsOfFirstFormation;
        private IList<EnemyFlightFormationItem> gameObjectsOfSecondFormation;
    
        private Vector3 startPositionLeft;
        private Vector3 startPositionRight;
    
        private float WaveTimer;
        private bool IsFirstFormationReleased;
        private bool IsSecondFormationReleased;
        private bool IsThirdFormationReleased;
        private bool IsFourthFormationReleased;
        private Guid FormationOneWaveId;
        private Guid FormationTwoWaveId;
    
        private void Start()
        {
            this.startPositionLeft = new Vector3(-7.5f, .5f, 0);
            this.startPositionRight = new Vector3(7.5f, .5f, 0);
        
            this.FormationOneWaveId = Guid.NewGuid();
            this.FormationTwoWaveId = Guid.NewGuid();
        
            InitializeWave();
        } 
    
        public override void SpawnWave()
        {
            WaveTimer += Time.deltaTime;

            if (!IsFirstFormationReleased)
            {
                ReleaseFirstFormation();

                if (this.numberOfReleasedItemsInFirstFormation >= 5)
                {
                    this.EnemyFlightFormation.Add(this.FormationOneWaveId, this.gameObjectsOfFirstFormation);
                    IsFirstFormationReleased = true;
                }
            }
        
            if (!IsSecondFormationReleased)
            {
                ReleaseSecondFormation();

                if (this.numberOfReleasedItemsInSecondFormation >= 5)
                {
                    this.EnemyFlightFormation.Add(this.FormationTwoWaveId, this.gameObjectsOfSecondFormation);
                    IsSecondFormationReleased = true;
                }
            }

            if (IsFirstFormationReleased && IsSecondFormationReleased)
            {
                this.IsWaveSpawned = true;
            }

            // if (!IsSecondFormationReleased && WaveTimer > 2f && WaveTimer < 2.1f)
            // {
            //     this.SpawnWaveInternalLeft();
            //     IsSecondFormationReleased = true;
            // }
            //
            // if (!IsThirdFormationReleased && WaveTimer > 3f && WaveTimer < 3.1f)
            // {
            //     this.SpawnWaveInternalLeft();
            //     IsThirdFormationReleased = true;            
            // }
            //
            // if (!IsFourthFormationReleased && WaveTimer > 4f && WaveTimer < 4.1f)
            // {
            //     this.SpawnWaveInternalLeft();
            //     IsFourthFormationReleased = true;
            //
            //     this.IsWaveSpawned = true;
            // }
        }

        private void ReleaseFirstFormation()
        {
            if (this.numberOfReleasedItemsInFirstFormation < 5 && this.lastReleaseTimeStampForFirstFormation + releaseTimespan < this.WaveTimer)
            {
                this.SpawnWaveInternalLeft(this.FormationOneWaveId, this.gameObjectsOfFirstFormation);
                this.lastReleaseTimeStampForFirstFormation = this.WaveTimer;
                this.numberOfReleasedItemsInFirstFormation++;
            }
        }
    
        private void ReleaseSecondFormation()
        {
            if (this.numberOfReleasedItemsInSecondFormation < 5 && this.lastReleaseTimeStampForSecondFormation + releaseTimespan < this.WaveTimer)
            {
                this.SpawnWaveInternalRight(this.FormationTwoWaveId, this.gameObjectsOfSecondFormation);
                this.lastReleaseTimeStampForSecondFormation = this.WaveTimer;
                this.numberOfReleasedItemsInSecondFormation++;
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
                    if (lastPosition.z > -10f)
                    {
                        Instantiate(LootTemplate, lastPosition, Quaternion.identity);    
                    }
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
            this.lastReleaseTimeStampForFirstFormation = 0f;
            this.lastReleaseTimeStampForSecondFormation = 0f;
            this.numberOfReleasedItemsInFirstFormation = 0;
            this.numberOfReleasedItemsInSecondFormation = 0;

            this.IsFirstFormationReleased = false;
            this.IsSecondFormationReleased = false;
            this.IsThirdFormationReleased = false;
            this.IsFourthFormationReleased = false;

            this.EnemyFlightFormation = new Dictionary<Guid, IList<EnemyFlightFormationItem>>();
            this.Enemies = new Dictionary<int, EnemyFlightFormationItem>();
            this.gameObjectsOfFirstFormation = new List<EnemyFlightFormationItem>();
            this.gameObjectsOfSecondFormation = new List<EnemyFlightFormationItem>();
        }
    
        private void SpawnWaveInternalLeft(Guid waveId, IList<EnemyFlightFormationItem> gameObjects)
        {
            AddFormationItem(waveId, startPositionLeft, gameObjects);
        }
    
        private void SpawnWaveInternalRight(Guid waveId, IList<EnemyFlightFormationItem> gameObjects)
        {
            AddFormationItem(waveId, startPositionRight, gameObjects, true);
        }
    
        private void AddFormationItem(Guid waveId, Vector3 startPosition, ICollection<EnemyFlightFormationItem> gameObjects, bool isNegative=false)
        {
            EnemyFlightFormationItem enemyItem = CreateNewEnemyItem(waveId, startPosition, isNegative);
            gameObjects.Add(enemyItem);
            this.Enemies.Add(enemyItem.Enemy.GetInstanceID(), enemyItem);
        }
    
        private EnemyFlightFormationItem CreateNewEnemyItem(Guid waveId, Vector3 startPosition, bool isNegative)
        {
            return new EnemyFlightFormationItem
            {
                FormationId = waveId,
                Flag = isNegative,
                Health = GameManager.Instance.EnemyWaveOneHealth,
                Enemy = Instantiate(EnemyTemplate, startPosition, Quaternion.identity),
                StartPosition = startPosition
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
}
