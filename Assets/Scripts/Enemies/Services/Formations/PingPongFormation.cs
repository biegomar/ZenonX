using System;
using System.Collections.Generic;
using Enemies.Model;
using UnityEngine;

namespace Enemies.Services.Formations
{
    /// <summary>
    /// The ping pong formation.
    /// </summary>
    public class PingPongFormation : EnemyFormation
    {
        public override KeyValuePair<Guid, IList<EnemyFlightFormationItem>> SpawnFormation()
        {
            var formationId = Guid.NewGuid();
            var gameObjects = new List<EnemyFlightFormationItem>();
            var initialDistance = new Vector3(this.enemyFormationData.Distance.x,
                this.enemyFormationData.Distance.y, this.enemyFormationData.Distance.z);

            for (int i = 0; i < 3; i++)
            {
                this.enemyFormationData.Distance = new Vector3(i % 2 == 0 ? initialDistance.x : -initialDistance.x,
                    initialDistance.y * (i + 1), initialDistance.z);
                EnemyFlightFormationItem enemyItem = this.CreateNewEnemyItem(formationId);
                gameObjects.Add(enemyItem);
            }

            this.enemyFormationData.Distance = initialDistance;

            return new KeyValuePair<Guid, IList<EnemyFlightFormationItem>>(formationId, gameObjects);
        }

        protected override EnemyFlightFormationItem CreateNewEnemyItem(Guid formationId, uint positionInFormation = 0,
            bool isNegative = false)
        {
            var startVector = this.enemyFormationData.StartPoint + this.enemyFormationData.Distance;
            var vector = startVector.x < 0 ? startVector + 2 * Vector3.left : startVector + 2 * Vector3.right;

            return new EnemyFlightFormationItem
            {
                FormationId = formationId,
                Formation = this,
                Health = this.enemyFormationData.EnemyHealthPoints + this.healthAddOn,
                Enemy = Instantiate(this.enemyFormationData.EnemyTemplate, vector, Quaternion.identity, this.transform),
                StartPosition = startVector,
                PositionInFormation = positionInFormation,
                Flag = isNegative
            };
        }
    }
}