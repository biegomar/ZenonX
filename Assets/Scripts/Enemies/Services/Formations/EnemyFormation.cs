using System;
using System.Collections.Generic;
using Enemies.Model;
using UnityEngine;

namespace Enemies.Services.Formations
{
    public abstract class EnemyFormation : MonoBehaviour
    {
        [SerializeField] 
        protected EnemyFormationData enemyFormationData;
        
        public abstract KeyValuePair<Guid, IList<EnemyFlightFormationItem>> SpawnFormation();
        
        public EnemyFlightFormationItem CreateNewEnemyItem(Guid formationId)
        {
            var vector = this.enemyFormationData.StartPoint + this.enemyFormationData.Distance;

            return new EnemyFlightFormationItem
            {
                FormationId = formationId,
                Health = this.enemyFormationData.EnemyHealthPoints,
                Enemy = Instantiate(this.enemyFormationData.EnemyTemplate, vector, Quaternion.identity),
                StartPosition = vector
            };
        }
    }
}
