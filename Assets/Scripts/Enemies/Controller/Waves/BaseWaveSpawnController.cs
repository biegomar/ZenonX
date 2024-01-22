using UnityEngine;

namespace Enemies.Controller.Waves
{
    public abstract class BaseWaveSpawnController : MonoBehaviour
    {
        public bool IsWaveSpawned { get; set; }
        public bool IsWaveCompleted { get; set; }

        public abstract void SpawnWave();

        public abstract void SpawnLoot(Vector3 lastPosition);

        public abstract void ResetWave();
    }
}
