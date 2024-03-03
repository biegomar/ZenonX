using UnityEngine;

namespace Camera.Controller
{
    /// <summary>
    /// The camera controller.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
