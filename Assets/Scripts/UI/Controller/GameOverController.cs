using TMPro;
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

        //new input system
        private GameInput gameInput;
        private InputAction quit;
        private InputAction pause;

        private bool IsGamePaused;

        private void OnEnable()
        {
            this.GameOverText.text = string.Empty;

            this.gameInput = new GameInput();
            this.quit = this.gameInput.Game.Quit;       
            this.pause = this.gameInput.Game.Pause;

            this.quit.Enable();        
            this.pause.Enable();

            this.IsGamePaused = false;
        }

        private void OnDisable()
        {
            this.quit.Disable();  
            this.pause.Disable();
        }

        private void Update()
        {
            this.CheckForGamePause();

            if (this.IsGamePaused)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }

            if (!GameManager.Instance.IsGameRunning)
            {                        
                this.GameOverText.text = "Game Over!";
            }   
            
            if (this.IsQuitPressed())
            {
                SceneManager.LoadScene(0);
            }
        }

        private bool IsQuitPressed()
        {
            return this.quit.triggered;
        }

        private void CheckForGamePause()
        {
            if (this.pause.triggered) 
            {
                this.IsGamePaused = !this.IsGamePaused;
            }
        }
    }
}
