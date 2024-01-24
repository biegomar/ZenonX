using System;
using System.Collections.Generic;
using Enemies.Model;
using UnityEngine;

namespace Enemies.Services.Formations
{
    public abstract class BaseEnemyFormation : MonoBehaviour
    {
        [SerializeField]
        protected GameObject enemyTemplate;
        
        [SerializeField]
        protected uint enemyHealthPoints;
        
        public abstract KeyValuePair<Guid, IList<EnemyFlightFormationItem>> SpawnFormation(Vector3 startPoint, Vector3 transitionRangeFrom, Vector3 transitionRangeTo, Vector3 distance, bool flag = true);
        
        protected EnemyFlightFormationItem CreateNewEnemyItem(Guid formationId, Vector3 startPoint, Vector3 distance)
        {
            var vector = startPoint + distance;

            return new EnemyFlightFormationItem
            {
                FormationId = formationId,
                Health = this.enemyHealthPoints,
                Enemy = Instantiate(this.enemyTemplate, vector, Quaternion.identity),
                StartPosition = vector
            };
        }
    }
}
