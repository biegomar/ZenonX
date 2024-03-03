using System;
using Enemies.Services.Formations;

namespace Enemies.Model
{
    /// <summary>
    /// Some meta data for enemies that are part of a formation.
    /// </summary>
    public record EnemyFlightFormationItem : EnemyItem
    {
        public Guid FormationId { get; set; }

        public EnemyFormation Formation { get; set; }

        public bool Flag { get; set; }
        
        public uint PositionInFormation { get; set; }
    }
}
