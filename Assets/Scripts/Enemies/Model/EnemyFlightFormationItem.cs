using System;

namespace Enemies.Model
{
    public class EnemyFlightFormationItem : EnemyItem
    {
        public Guid FormationId { get; set; }

        public bool Flag { get; set; }
        
        public uint PositionInFormation { get; set; }
    }
}
