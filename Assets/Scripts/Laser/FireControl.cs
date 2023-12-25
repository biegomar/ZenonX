using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireControl : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;

    [SerializeField]
    private AmmoBarManager healthBarManager;

    private float ActualLaserFrequence;
    private float LaserInterval;
    private float ActualLaserPowerRegainInterval;        

    //new input system
    private GameInput gameInput;
    private InputAction fire;  

    private void OnEnable()
    {
        this.gameInput = new GameInput();
        this.fire = this.gameInput.Player.Fire;        

        this.fire.Enable();        
    }

    private void OnDisable()
    {
        this.fire.Disable();        
    }

    public void Start()
    {
        this.LaserInterval = 0;
        this.ActualLaserPowerRegainInterval = 0;            
    }

    public void Update()
    {
        this.ActualLaserFrequence = GameManager.Instance.Level1LaserFrequence;
        this.LaserInterval += Time.deltaTime;
        this.ActualLaserPowerRegainInterval += Time.deltaTime;

        this.FeuerFrei();
        this.GainFirePower();
        this.healthBarManager.SetHealthValue();
    }

    private void FeuerFrei()
    {
        if (this.LaserInterval >= this.ActualLaserFrequence && GameManager.Instance.ActualLaserPower > 0 && this.IsFirePressed())
        {
            Instantiate(Laser, new Vector3(
               transform.position.x,
               transform.position.y + 0.3f,
               transform.position.z), Quaternion.identity);

            GameManager.Instance.ActualLaserPower--;
            this.LaserInterval = 0;                
        }
    }

    private void GainFirePower()
    {
        if (this.ActualLaserPowerRegainInterval >= GetLaserPowerRegainIntervalHalfLife())
        {
            if (GameManager.Instance.ActualLaserPower < GameManager.Instance.MaxShipLaserPower)
            {
                GameManager.Instance.ActualLaserPower++;
                this.ActualLaserPowerRegainInterval = 0;
            }
        }                  
    }

    private float GetLaserPowerRegainIntervalHalfLife()
    {
        var percentage = ((float)GameManager.Instance.ActualLaserPower / GameManager.Instance.MaxShipLaserPower);
        return percentage switch
        {
            >= .5f => GameManager.Instance.LaserPowerRegainInterval,
            >= .25f and < .5f => GameManager.Instance.LaserPowerRegainInterval / 2,
            >= .1f and < .25f => GameManager.Instance.LaserPowerRegainInterval / 3,
            < .1f => GameManager.Instance.LaserPowerRegainInterval / 4
        };
    }

    private bool IsFirePressed()
    {
        return this.fire.triggered;
    }
}
