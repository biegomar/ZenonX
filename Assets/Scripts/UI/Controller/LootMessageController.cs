using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootMessageController : MonoBehaviour
{
    [SerializeField]
    private Text LootText;

    private float lootTimer;
    private void OnEnable()
    {
        this.LootText.text = string.Empty;
    }

    private void Start()
    {
        this.lootTimer = 0f;
    }

    private void Update()
    {
        this.lootTimer += Time.deltaTime;
        
        if (GameManager.Instance.IsLootSpawned)
        {
            this.lootTimer = 0f;
            this.LootText.text = GameManager.Instance.LootMessage;
            GameManager.Instance.IsLootSpawned = false;
        }

        if (!string.IsNullOrEmpty(this.LootText.text) && this.lootTimer >= 2f)
        {
            this.LootText.text = string.Empty;
        }
    }
}
