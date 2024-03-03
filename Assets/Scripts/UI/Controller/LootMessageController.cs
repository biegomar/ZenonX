using UnityEngine;
using UnityEngine.UI;

namespace UI.Controller
{
    /// <summary>
    /// The loot message controller.
    /// </summary>
    public class LootMessageController : MonoBehaviour
    {
        [SerializeField]
        private Text LootText;

        private float messageTimer;
        private void OnEnable()
        {
            this.LootText.text = string.Empty;
        }

        private void Start()
        {
            this.messageTimer = 0f;
        }

        private void Update()
        {
            this.messageTimer += Time.deltaTime;
        
            if (GameManager.Instance.IsLootSpawned)
            {
                this.messageTimer = 0f;
                this.LootText.text = GameManager.Instance.LootMessage;
                GameManager.Instance.IsLootSpawned = false;
            }

            if (!string.IsNullOrEmpty(this.LootText.text) && this.messageTimer >= 2f)
            {
                this.LootText.text = string.Empty;
            }
        }
    }
}
