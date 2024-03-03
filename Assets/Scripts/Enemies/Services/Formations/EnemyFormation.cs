using System;
using System.Collections.Generic;
using Enemies.Model;
using UnityEngine;

namespace Enemies.Services.Formations
{
    /// <summary>
    /// The base class for all enemy formations.
    /// </summary>
    public abstract class EnemyFormation : MonoBehaviour
    {
        [SerializeField] public EnemyFormationData enemyFormationData;
        [SerializeField] public uint healthAddOn;

        public abstract KeyValuePair<Guid, IList<EnemyFlightFormationItem>> SpawnFormation();

        public virtual void SpawnLoot(Vector3 lastPosition)
        {
            Instantiate(this.enemyFormationData.LootTemplate, lastPosition, Quaternion.identity, this.transform);
        }

        protected virtual EnemyFlightFormationItem CreateNewEnemyItem(Guid formationId, uint positionInFormation = 0,
            bool isNegative = false)
        {
            var vector = this.enemyFormationData.StartPoint + this.enemyFormationData.Distance;

            return new EnemyFlightFormationItem
            {
                FormationId = formationId,
                Formation = this,
                Health = this.enemyFormationData.EnemyHealthPoints + this.healthAddOn,
                Enemy = Instantiate(this.enemyFormationData.EnemyTemplate, vector, Quaternion.identity, this.transform),
                StartPosition = vector,
                PositionInFormation = positionInFormation,
                Flag = isNegative
            };
        }
    }
}
