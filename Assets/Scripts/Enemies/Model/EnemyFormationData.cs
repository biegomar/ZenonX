using UnityEngine;

namespace Enemies.Model
{
    /// <summary>
    /// Base object for static data on enemy formations.
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyFormation")]
    public class EnemyFormationData : ScriptableObject
    {
        public GameObject EnemyTemplate;
        public GameObject LootTemplate;
        public uint EnemyHealthPoints;
        public Vector3 StartPoint;
        public Vector3 TransitionRangeFrom;
        public Vector3 TransitionRangeTo;
        public Vector3 Distance;
        public bool Flag;
    }
}
