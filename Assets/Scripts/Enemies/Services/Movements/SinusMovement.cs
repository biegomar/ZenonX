using System;
using UnityEngine;

namespace Enemies.Services.Movements
{
    internal class SinusMovement : IMovementStrategy
    {
        private float initialX;
        private const int sinusMultiplier = 2;
        private double activeSinus;

        public SinusMovement(Vector2 initialPosition)
        {
            this.initialX = initialPosition.x;
        }

        public float CalculateNewXPosition(GameObject gameObject)
        {
            if (gameObject.transform.position.y < 5)
            {
                var result = this.initialX + (float)Math.Sin(activeSinus) * GameManager.Instance.EnemyWaveOneSinusAmplitude;

                if (activeSinus < Math.PI * sinusMultiplier)
                {
                    activeSinus += GameManager.Instance.EnemyWaveOneSinusStep;
                }
                else
                {
                    activeSinus = 0d;
                }

                return result;
            }

            return gameObject.transform.position.x;
        }

        public float CalculateNewYPosition(GameObject gameObject)
        {
            return (gameObject.transform.position.y - GameManager.Instance.EnemyWaveOneYStep * GameManager.Instance.EnemyWaveOneYSpeed);
        }
    }
}
