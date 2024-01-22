using UnityEngine;

namespace Sky
{
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
            if (GameManager.Instance.IsGameRunning)
            {
                var offset = material.mainTextureOffset;
                offset.y += Time.deltaTime / 20f;
                material.mainTextureOffset = offset;
            }        
        }
    }
}
