using UnityEngine;

namespace Enemies.Services
{
    internal interface IMovementStrategy
    {
        float CalculateNewXPosition(GameObject gameObject);
        float CalculateNewYPosition(GameObject gameObject);
    }
}
