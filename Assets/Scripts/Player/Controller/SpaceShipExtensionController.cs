using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipExtensionController : MonoBehaviour
{
    [SerializeField] 
    private GameObject Shield;

    void Update()
    {
        if (GameManager.Instance.IsShipShieldActive && !this.Shield.activeSelf)
        {
            Shield.SetActive(true);
        }

        if (!GameManager.Instance.IsShipShieldActive && this.Shield.activeSelf)
        {
            Shield.SetActive(false);
        }
    }
}
