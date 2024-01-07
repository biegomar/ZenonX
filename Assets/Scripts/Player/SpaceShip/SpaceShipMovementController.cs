using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShipMovementController : MonoBehaviour
{
    //Sprites
    private GameObject ship;
    private GameObject shipRight;
    private GameObject shipLeft;

    //new input system
    private GameInput gameInput;
    private InputAction move;
    private InputAction boost;

    private Rigidbody2D Rigidbody;

    private bool isShipBoosted;


    private void OnEnable()
    {
        this.gameInput = new GameInput();
        this.move = this.gameInput.Player.Move;
        this.boost = this.gameInput.Player.Boost;

        this.move.Enable();
        this.boost.Enable();
    }

    private void OnDisable()
    {
        this.move.Disable();
        this.boost.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        isShipBoosted = false;
        this.Rigidbody = GetComponent<Rigidbody2D>();

        shipLeft = GameObject.FindGameObjectsWithTag("Left").First();
        shipRight = GameObject.FindGameObjectsWithTag("Right").First();
        ship = GameObject.FindGameObjectsWithTag("Unmoved").First();
    }

    // Update is called once per frame
    void Update()
    {
        // use delta time for game pause here.        
        if (GameManager.Instance.IsGameRunning && Time.deltaTime > 0f)
        {
            ShowRelevantShipSprite();
            MoveHorizontal();
            BoostShip();
        }      
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {        
        var collisionObject = collision.gameObject;
        if (collisionObject.tag == "Border")
        {            
            this.Rigidbody.velocity = Vector2.zero;
            this.Rigidbody.isKinematic = true;
        }
    }

    private void ShowRelevantShipSprite()
    {
        
        {
            if (this.move.ReadValue<Vector2>().x > 0)
            {
                this.shipRight.SetActive(true);
                this.shipLeft.SetActive(false);
                this.ship.SetActive(false);
            }
            else if (this.move.ReadValue<Vector2>().x < 0)
            {
                this.shipLeft.SetActive(true);
                this.shipRight.SetActive(false);
                this.ship.SetActive(false);
            }
            else if (this.move.ReadValue<Vector2>().x == 0)
            {
                this.ship.SetActive(true);
                this.shipLeft.SetActive(false);
                this.shipRight.SetActive(false);
            }
        }        
    }

    private void BoostShip()
    {
        if (this.Rigidbody.velocity.y == 0)
        {
            this.isShipBoosted = false;
        }        

        if (!this.isShipBoosted & this.boost.triggered)
        {
            this.Rigidbody.isKinematic = false;
            this.Rigidbody.AddForce(Vector2.up * GameManager.Instance.ShipBoosterVelocity, ForceMode2D.Impulse);
            this.isShipBoosted = true;
        }            
    }

    private void MoveHorizontal()
    {
        transform.position = new Vector3(
            CalculateNewXPosition(),
            transform.position.y,
            transform.position.z);
    }

    private float CalculateNewXPosition()
    {        
        return Math.Min(9f, Math.Max(-9f, transform.position.x + this.move.ReadValue<Vector2>().x * GameManager.Instance.ShipHorizontalSpeed * Time.deltaTime));
    }
}
