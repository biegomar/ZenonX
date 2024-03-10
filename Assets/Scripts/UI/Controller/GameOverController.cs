using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Controller
{
    /// <summary>
    /// The game over controller.
    /// </summary>
    public class GameOverManagerController : MonoBehaviour
    {
        [SerializeField]
        private Text GameOverText;

        private bool isGamePaused;

        private void Awake()
        {
            this.GameOverText.text = string.Empty;
            this.isGamePaused = false;
        }

        private void Update()
        {
            Time.timeScale = this.isGamePaused ? 0f : 1f;

            if (!GameManager.Instance.IsGameRunning)
            {                        
                this.GameOverText.text = "Game Over!";
            }
        }

        public void OnQuit(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                SceneManager.LoadScene(0);
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed) 
            {
                this.isGamePaused = !this.isGamePaused;
            }
        }
    }
}
