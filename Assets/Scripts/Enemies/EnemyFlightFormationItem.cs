using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyFlightFormationItem
    {
        public Guid WaveId { get; set; }
        public GameObject Enemy { get; set; }
        public Vector3 StartPosition { get; set; }
    }
}
