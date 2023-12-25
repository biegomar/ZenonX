using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

namespace Assets.Scripts.Enemies.MovementStrategies
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
                var result = this.initialX + (float)Math.Sin(activeSinus) * GameManager.Instance.EnemySinusAmplitude;

                if (activeSinus < Math.PI * sinusMultiplier)
                {
                    activeSinus += GameManager.Instance.EnemySinusStep;
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
            return (gameObject.transform.position.y - GameManager.Instance.EnemyYStep * GameManager.Instance.EnemyYSpeed);
        }
    }
}
