using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyItem
    {        
        public GameObject Enemy { get; set; }
        public Vector3 StartPosition { get; set; }
        public int Health { get; set; }
    }
}
