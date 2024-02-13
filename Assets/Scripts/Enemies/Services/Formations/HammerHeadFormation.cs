using System;
using System.Collections.Generic;
using Enemies.Model;
using UnityEngine;

namespace Enemies.Services.Formations
{
    public class HammerHeadFormation : EnemyFormation
    {
        public override KeyValuePair<Guid, IList<EnemyFlightFormationItem>> SpawnFormation()
        {
            var formationId = Guid.NewGuid();
            var gameObjects = new List<EnemyFlightFormationItem>();
            var initialDistance = new Vector3(this.enemyFormationData.Distance.x,
                this.enemyFormationData.Distance.y, this.enemyFormationData.Distance.z);

            for (int i = 0; i < 7; i++)
            {
                this.enemyFormationData.Distance = initialDistance * i;
                EnemyFlightFormationItem enemyItem = this.CreateNewEnemyItem(formationId);
                gameObjects.Add(enemyItem);
            }
            
            this.enemyFormationData.Distance = initialDistance;
            
            return new KeyValuePair<Guid, IList<EnemyFlightFormationItem>>(formationId, gameObjects);
        }
    }
}