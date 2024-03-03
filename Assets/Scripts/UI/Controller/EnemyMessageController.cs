using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Controller
{
    /// <summary>
    /// The enemy message controller.
    /// </summary>
    public class EnemyMessageController : MonoBehaviour
    {
        [SerializeField] private List<string> messages;
        [SerializeField] private Text enemyText;

        private float messageTimer;

        private const string messageFallback = "Your Enemies are getting stronger!";
        private void OnEnable()
        {
            this.enemyText.text = messageFallback;
        }

        private void Start()
        {
            this.messageTimer = 2f;
        }

        private void Update()
        {
            this.messageTimer += Time.deltaTime;
        
            if (GameManager.Instance.IsEnemyWaveGettingStronger)
            {
                this.messageTimer = 0f;
            
                if (this.messages.Any())
                {
                    var index = UnityEngine.Random.Range(0, this.messages.Count);
                    this.enemyText.text = this.messages[index];
                }
            
                GameManager.Instance.IsEnemyWaveGettingStronger = false;
            }

            if (!string.IsNullOrEmpty(this.enemyText.text) && this.messageTimer >= 2f)
            {
                this.enemyText.text = string.Empty;
            }
        }
    }
}
