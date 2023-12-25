using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
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
            //LaserPower.text = $"Laserpower: {GameManager.Instance.ActualLaserPower}/{GameManager.Instance.MaxShipLaserPower}";    
        }
    }
}
