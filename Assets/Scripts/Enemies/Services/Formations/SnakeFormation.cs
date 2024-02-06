using System;
using System.Collections.Generic;
using Enemies.Model;
using UnityEngine;

namespace Enemies.Services.Formations
{
    public class SnakeFormation : EnemyFormation
    {
        public override KeyValuePair<Guid, IList<EnemyFlightFormationItem>> SpawnFormation()
        {
            var posX = UnityEngine.Random.Range(this.enemyFormationData.TransitionRangeFrom.x, this.enemyFormationData.TransitionRangeTo.x);
            
            var formationId = Guid.NewGuid();
            var gameObjects = new List<EnemyFlightFormationItem>();
            this.enemyFormationData.StartPoint.x = posX;
            var initialDistance = new Vector3(this.enemyFormationData.Distance.x,
                this.enemyFormationData.Distance.y, this.enemyFormationData.Distance.z);

            for (int i = 0; i < 5; i++)
            {
                this.enemyFormationData.Distance = initialDistance * (i+1);
                EnemyFlightFormationItem enemyItem = this.CreateNewEnemyItem(formationId);
                gameObjects.Add(enemyItem);
            }
            
            return new KeyValuePair<Guid, IList<EnemyFlightFormationItem>>(formationId, gameObjects);
        }
    }
}