using UnityEngine;

namespace Enemies.Model
{
    public class EnemyItem
    {        
        public GameObject Enemy { get; set; }
        public Vector3 StartPosition { get; set; }
        public uint Health { get; set; }
    }
}
