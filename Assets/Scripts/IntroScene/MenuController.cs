using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
