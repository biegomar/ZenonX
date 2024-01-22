using UnityEngine;

namespace Camera.Controller
{
    public class CameraController : MonoBehaviour
    {
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
