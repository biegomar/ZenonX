using System;
using System.Collections.Generic;
using Enemies.Model;
using UnityEngine;

namespace Enemies.Services.Formations
{
    public class SnakeFormation : BaseEnemyFormation
    {
        public override KeyValuePair<Guid, IList<EnemyFlightFormationItem>> SpawnFormation(Vector3 startPoint, Vector3 transitionRangeFrom, Vector3 transitionRangeTo, Vector3 distance, bool flag = true)
        {
            var posX = UnityEngine.Random.Range(transitionRangeFrom.x, transitionRangeTo.x);

            

            var formationId = Guid.NewGuid();
            var gameObjects = new List<EnemyFlightFormationItem>();

            for (int i = 0; i < 5; i++)
            {
                var distanceVector = new Vector3(posX, distance.y * i, 0);
                EnemyFlightFormationItem enemyItem = CreateNewEnemyItem(formationId, startPoint, distanceVector);
                gameObjects.Add(enemyItem);
            }
            
            return new KeyValuePair<Guid, IList<EnemyFlightFormationItem>>(formationId, gameObjects);
        }
    }
}