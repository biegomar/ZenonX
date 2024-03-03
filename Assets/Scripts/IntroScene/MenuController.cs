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
    }
}
