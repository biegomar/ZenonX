using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// The scroll text controller.
/// </summary>
public class ScrollTextManager : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private bool looping;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private RectTransform scrollText;

    private RectTransform textTransform;

    private void Start()
    {
        this.scrollText.localPosition = this.startPosition;
    }

    private void Update()
    {
        if (this.scrollText.localPosition.x > this.endPosition.x)
        {
            
            this.scrollText.Translate(Vector3.left * (this.scrollSpeed * Time.deltaTime));
        }
        else
        {
            this.scrollText.localPosition = this.startPosition;
        }
    }
}
