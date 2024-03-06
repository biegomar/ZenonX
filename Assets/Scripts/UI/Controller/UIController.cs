using System;
using TMPro;
using UnityEngine;

namespace UI.Controller
{
    /// <summary>
    /// The UI controller.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI ScoreText;

        [SerializeField]
        private TextMeshProUGUI LaserPower;

        private GameObject shieldHealthProgressBar;

        private void Start()
        {
            shieldHealthProgressBar = GameObject.FindGameObjectWithTag("ShieldHealthProgressBar");
        }

        void Update()
        {
            if (GameManager.Instance.IsGameRunning)
            {
                ScoreText.text = $"Score: {GameManager.Instance.Score}";
                if (this.shieldHealthProgressBar != null)
                {
                    this.shieldHealthProgressBar.SetActive(GameManager.Instance.IsShipShieldActive);
                }
            }
        }
    }
}
