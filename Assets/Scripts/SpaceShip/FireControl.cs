using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;

    private uint ActualLaserPower;

    public void Start()
    {
        this.ActualLaserPower = GameManager.MaxShipLaserPower;
        //InvokeRepeating(nameof(GainFirePower), 1f, GameManager.LaserPowerRegainFactor);
    }

    public void Update()
    {
        if (this.ActualLaserPower > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Laser, new Vector3(
               transform.position.x,
               transform.position.y + 0.3f,
               transform.position.z), Quaternion.identity);

            this.ActualLaserPower--;
        }        
    }

    private void GainFirePower()
    {
        if (this.ActualLaserPower < 100)
        {
            this.ActualLaserPower++;
        }               
    }
}
