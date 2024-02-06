using System;
using System.Collections.Generic;
using Enemies.Model;

namespace Enemies.Services.Formations
{
    public class CycloidFormation : EnemyFormation
    {
        public override KeyValuePair<Guid, IList<EnemyFlightFormationItem>> SpawnFormation()
        {
            var formationId = Guid.NewGuid();
            var gameObjects = new List<EnemyFlightFormationItem>();

            for (int i = 0; i < 5; i++)
            {
                EnemyFlightFormationItem enemyItem = this.CreateNewEnemyItem(formationId);
                gameObjects.Add(enemyItem);
            }
            
            return new KeyValuePair<Guid, IList<EnemyFlightFormationItem>>(formationId, gameObjects);
        }
    }
}