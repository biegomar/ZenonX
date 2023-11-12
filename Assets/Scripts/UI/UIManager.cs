using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ScoreText;
    
    void Update()
    {
        ScoreText.text = $"Score: {GameManager.Instance.Score}";
    }
}
