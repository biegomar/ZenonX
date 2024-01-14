using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal interface IMovementStrategy
{
    float CalculateNewXPosition(GameObject gameObject);
    float CalculateNewYPosition(GameObject gameObject);
}
