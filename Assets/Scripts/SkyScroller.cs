using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyScroller : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material material;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    void Update()
    {                
        var offset = material.mainTextureOffset;
        offset.y += Time.deltaTime / 20f;
        material.mainTextureOffset = offset;
    }
}
