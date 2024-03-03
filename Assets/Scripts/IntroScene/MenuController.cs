using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IntroScene
{
    /// <summary>
    /// The menu controller.
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        public void StartGame()
        {
            GameManager.Instance.Initialize();
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Intro")
            {
                Cursor.lockState = CursorLockMode.None;
            }
        
            if (scene.name == "Level")
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
