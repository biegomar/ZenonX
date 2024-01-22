using System;

namespace Enemies.Model
{
    public class EnemyFlightFormationItem : EnemyItem
    {
        public Guid WaveId { get; set; }

        public bool Flag { get; set; }
    }
}
