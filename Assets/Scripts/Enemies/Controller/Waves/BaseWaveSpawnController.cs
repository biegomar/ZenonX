using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies.Controller.Waves
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
