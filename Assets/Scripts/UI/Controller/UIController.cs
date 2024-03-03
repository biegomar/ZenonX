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

        void Update()
        {
            if (GameManager.Instance.IsGameRunning)
            {
                ScoreText.text = $"Score: {GameManager.Instance.Score}";
                //LaserPower.text = $"Laserpower: {GameManager.Instance.ActualShipLaserPower}/{GameManager.Instance.MaxShipLaserPower}";    
            }
        }
    }
}
