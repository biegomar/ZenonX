using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireControl : MonoBehaviour
{
    [SerializeField]
    private GameObject Laser;

    private uint ActualLaserPower;
    private float ActualLaserFrequence;
    private float LaserInterval;

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
        this.ActualLaserPower = GameManager.Instance.MaxShipLaserPower;
        this.LaserInterval = 0;

        InvokeRepeating(nameof(GainFirePower), 1f, GameManager.Instance.LaserPowerRegainInterval);
    }

    public void Update()
    {
        this.ActualLaserFrequence = GameManager.Instance.Level1LaserFrequence;
        this.LaserInterval += Time.deltaTime;

        if (this.LaserInterval >= this.ActualLaserFrequence && this.ActualLaserPower > 0 && this.IsFirePressed())
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

    private bool IsFirePressed()
    {
        return this.fire.triggered;
    }
}
