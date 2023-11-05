using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;

    private uint ActualLaserPower;
    private float ActualLaserFrequence;
    private float LaserInterval;

    public void Start()
    {        
        this.ActualLaserPower = GameManager.MaxShipLaserPower;
        this.LaserInterval = 0;

        InvokeRepeating(nameof(GainFirePower), 1f, GameManager.LaserPowerRegainInterval);
    }

    public void Update()
    {
        this.ActualLaserFrequence = GameManager.Level1LaserFrequence;
        this.LaserInterval += Time.deltaTime;

        if (this.LaserInterval >= this.ActualLaserFrequence && this.ActualLaserPower > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Laser, new Vector3(
               transform.position.x,
               transform.position.y + 0.3f,
               transform.position.z), Quaternion.identity);

            this.ActualLaserPower--;
            this.LaserInterval = 0;
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
