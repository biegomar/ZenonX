using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enemies.Model;
using Enemies.Services.Formations;
using UnityEngine;

namespace Enemies.Controller.Waves
{
    public class WaveSpawnController : MonoBehaviour
    {
        public List<EnemyFormation> enemyFormations;
        public float formationSpawnDistanceTime = 3f;
        
        public IDictionary<int, EnemyFlightFormationItem> Enemies;
        public IDictionary<Guid, IList<EnemyFlightFormationItem>> EnemyFlightFormations;
        
        private IDictionary<Guid, bool> EnemyFlightFormationNegativeDirection;
        
        public bool IsWaveSpawned { get; set; }
        public bool IsWaveCompleted { get; set; }
        
        private float WaveTimer;
        
        private void Start()
        {
            InitializeWave();
        } 
        
        public void SpawnFormation(EnemyFormation enemyFormation)
        {
            var formation = enemyFormation.SpawnFormation();
            EnemyFlightFormationNegativeDirection.Add(formation.Key, false);
            formation.Value.ToList().ForEach(x => this.Enemies.Add(x.Enemy.GetInstanceID(), x));
            this.EnemyFlightFormations.Add(formation);
        }

        public void SpawnLoot(Guid enemyFlightFormationId, GameObject lootTemplate, Vector3 lastPosition)
        {
            var enemyFlightFormation = this.EnemyFlightFormations.FirstOrDefault(f => f.Key == enemyFlightFormationId);
            if (!enemyFlightFormation.Value.Any())
            {
                this.RemoveDeadWaveFromDictionary(enemyFlightFormation.Key);
                Instantiate(lootTemplate, lastPosition, Quaternion.identity);
            }
        }

        public void ResetWave()
        {
            InitializeWave();
        }
        
        private void InitializeWave()
        {
            this.IsWaveSpawned = false;
            this.IsWaveCompleted = false;
            
            this.EnemyFlightFormations = new Dictionary<Guid, IList<EnemyFlightFormationItem>>();
            this.EnemyFlightFormationNegativeDirection = new Dictionary<Guid, bool>();
            this.Enemies = new Dictionary<int, EnemyFlightFormationItem>();
        }
        
        private void RemoveDeadWaveFromDictionary(Guid deadWave)
        {
            this.EnemyFlightFormations.Remove(deadWave);

            if (!this.EnemyFlightFormations.Any())
            {
                this.IsWaveCompleted = true;
            }
        }
    }
}
