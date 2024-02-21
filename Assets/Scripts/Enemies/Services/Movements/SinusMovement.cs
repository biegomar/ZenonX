using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Enemies.Services.Movements
{
    internal class SinusMovement : IMovementStrategy
    {
        private float initialX;
        private float activeSinus;
        private const int sinusMultiplier = 2;

        public SinusMovement(Vector2 initialPosition)
        {
            this.initialX = initialPosition.x;
        }

        public float CalculateNewXPosition(GameObject gameObject, bool isRightMovement)
        {
            if (gameObject.transform.position.y < 5)
            {
                var time = Time.time;
                var result = this.initialX + Mathf.Sin(activeSinus + time) * GameManager.Instance.EnemyWaveOneSinusAmplitude;
            
                if (activeSinus < Math.PI * sinusMultiplier)
                {
                    activeSinus += GameManager.Instance.EnemyWaveOneSinusStep * Time.deltaTime;
                }
                else
                {
                    activeSinus = 0f;
                }
            
                return result;
            }
            
            return gameObject.transform.position.x;
        }

        public float CalculateNewYPosition(GameObject gameObject, bool isUpMovement)
        {
            if (isUpMovement)
            {
                return gameObject.transform.position.y + GameManager.Instance.EnemyWaveOneYSpeed * Time.deltaTime;
            }
            
            return gameObject.transform.position.y - GameManager.Instance.EnemyWaveOneYSpeed * Time.deltaTime;
        }
    }
}
