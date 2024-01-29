using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Model;
using Enemies.Services.Formations;
using UnityEngine;

namespace Enemies.Controller.Waves.WaveOne
{
    public class EnemyWaveOneSpawnController : BaseWaveSpawnController
    {
        public IDictionary<Guid, IList<EnemyFlightFormationItem>> EnemyFlightFormation;
        public IDictionary<Guid, bool> EnemyFlightFormationNegativeDirection;

        private float WaveTimer;
        private bool IsFirstFormationReleased;
        private bool IsSecondFormationReleased;
        private bool IsThirdFormationReleased;
        private bool IsFourthFormationReleased;

        private BaseEnemyFormation actualFormation;

        private void Start()
        {
            InitializeWave();
        }    

        public override void SpawnWave()
        {
            WaveTimer += Time.deltaTime;

            if (this.actualFormation != default)
            {
                var distanceVector = new Vector3(0, GameManager.Instance.EnemyWaveOneDistance, 0);
                if (!IsFirstFormationReleased && WaveTimer > 1f && WaveTimer < 1.1f)
                {
                    var formation = this.actualFormation.SpawnFormation(transform.position,
                        new Vector3(-7f, 0, 0),
                        new Vector3(-5f, 0, 0),
                        distanceVector,
                        false);
                    EnemyFlightFormationNegativeDirection.Add(formation.Key, false);
                    formation.Value.ToList().ForEach(x => this.Enemies.Add(x.Enemy.GetInstanceID(), x));
                    this.EnemyFlightFormation.Add(formation);
                    IsFirstFormationReleased = true;
                }

                if (!IsSecondFormationReleased && WaveTimer > 2f && WaveTimer < 2.1f)
                {
                    bool flag = UnityEngine.Random.Range(0, 2) == 0;
                    var formation = this.actualFormation.SpawnFormation(transform.position,
                        new Vector3(-4f, 0, 0),
                        new Vector3(-2f, 0, 0), 
                        distanceVector,
                        flag);
                    EnemyFlightFormationNegativeDirection.Add(formation.Key, flag);
                    formation.Value.ToList().ForEach(x => this.Enemies.Add(x.Enemy.GetInstanceID(), x));
                    this.EnemyFlightFormation.Add(formation);
                    IsSecondFormationReleased = true;
                }

                if (!IsThirdFormationReleased && WaveTimer > 3f && WaveTimer < 3.1f)
                {
                    bool flag = UnityEngine.Random.Range(0, 2) == 0;
                    var formation = this.actualFormation.SpawnFormation(transform.position,
                        new Vector3(-1f, 0, 0),
                        new Vector3(1f, 0, 0), 
                        distanceVector,
                        flag);
                    EnemyFlightFormationNegativeDirection.Add(formation.Key, flag);
                    formation.Value.ToList().ForEach(x => this.Enemies.Add(x.Enemy.GetInstanceID(), x));
                    this.EnemyFlightFormation.Add(formation);
                    IsThirdFormationReleased = true;            
                }

                if (!IsFourthFormationReleased && WaveTimer > 4f && WaveTimer < 4.1f)
                {
                    var formation = this.actualFormation.SpawnFormation(transform.position,
                        new Vector3(2f, 0, 0),
                        new Vector3(5f, 0, 0), 
                        distanceVector,
                        true);
                    EnemyFlightFormationNegativeDirection.Add(formation.Key, true);
                    formation.Value.ToList().ForEach(x => this.Enemies.Add(x.Enemy.GetInstanceID(), x));
                    this.EnemyFlightFormation.Add(formation);

                    this.IsWaveSpawned = true;
                }
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

            this.actualFormation = this.enemyFormations.FirstOrDefault();
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
