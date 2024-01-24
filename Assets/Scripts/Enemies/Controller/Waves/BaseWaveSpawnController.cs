using System.Collections.Generic;
using Enemies.Model;
using Enemies.Services.Formations;
using UnityEngine;

namespace Enemies.Controller.Waves
{
    public abstract class BaseWaveSpawnController : MonoBehaviour
    {
        [SerializeField]
        protected GameObject EnemyTemplate;

        [SerializeField]
        protected GameObject LootTemplate;
        
        [SerializeField] 
        protected List<BaseEnemyFormation> enemyFormations;
        
        public IDictionary<int, EnemyFlightFormationItem> Enemies;
        
        public bool IsWaveSpawned { get; set; }
        public bool IsWaveCompleted { get; set; }

        public abstract void SpawnWave();

        public abstract void SpawnLoot(Vector3 lastPosition);

        public abstract void ResetWave();
    }
}
