using UnityEngine;

namespace Enemies.Model
{
    /// <summary>
    /// Some meta data for a single enemy.
    /// </summary>
    public record EnemyItem
    {        
        public GameObject Enemy { get; set; }
        public Vector3 StartPosition { get; set; }
        public uint Health { get; set; }
    }
}
